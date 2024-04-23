using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// CRUD-Operations class for tool categories
    /// </summary>
    public class ToolCategoryManagement : CRUDManager<ToolCategory>
    {
        private List<int> _ids = new List<int>();
        public ToolCategoryManagement() { }

        public ToolCategoryManagement(IStorageHandler storageAccess)
        {
            _storageAccess = storageAccess;
        }

        public ToolCategoryManagement(IStorageHandler storageAccess, string source) : this(storageAccess)
        {
            if (source == null) return;
            _source = source;
        }

        public ToolCategoryManagement(IStorageHandler storageAccess, string source, List<ToolCategory> categories) : this(storageAccess, source)
        {
            _categories = categories;
        }

        private List<ToolCategory> _categories { get; }
        public IStorageHandler _storageAccess { get; }
        private string _source { get; }

        /// <summary>
        /// Get dezerialized tool category objects from xml file
        /// </summary>
        /// <returns>tool category objects</returns>
        public override List<ToolCategory> GetItem()
        {
            return _storageAccess.DeserializeXmlObjects<List<ToolCategory>>(_source);
        }

        /// <summary>
        /// Create a new tool category
        /// </summary>
        public override void CreateItem()
        {
            // check Ids for category
            if (File.Exists(_source))
            {
                if (checkCategoryIds(_source))
                {
                    // Get tool category objects from XML-File
                    var dezerializedCatObj = _storageAccess.DeserializeXmlObjects<List<ToolCategory>>(_source);

                    if (dezerializedCatObj != null && dezerializedCatObj.Count > 0)
                    {
                        // Add dezerialized tool category objects from XML to new categories from List
                        MergeDezerializedItems(dezerializedCatObj);
                    }
                }
            }

            // Serialize ToolCategory
            _storageAccess.SerializeXmlListOfObjects(_source, _categories);

            // Write Message to console
            Console.WriteLine();
            Console.WriteLine($"Tool category successfully created.");
            Console.WriteLine();
        }

        /// <summary>
        /// Edit an existing tool category
        /// </summary>
        /// <param name="selectedId">selected tool category Id</param>
        /// <param name="element">which xml element</param>
        /// <param name="newContent">new content for xml element</param>
        public override void EditItem(int selectedId, string element, string newContent)
        {
            try
            {
                if (element != null && newContent != null)
                {
                    // Get Content (objects) from XML
                    var customersXdoc = _storageAccess.XmlDoc(source: _source);

                    // Linq-Query for tool category
                    var customerXDoc = (from category in customersXdoc.Descendants("ToolCategory")
                                        where (int)category.Element("CategoryId") == selectedId
                                        select category).FirstOrDefault();

                    if (customerXDoc != null)
                    {
                        // Change element in XML-File with newContent (new value)
                        customerXDoc.Element(element).Value = newContent;
                        customersXdoc.Save(_source);
                        Console.WriteLine($"Tool category {element} successfully changed to {newContent}.");
                    }
                    else
                    {
                        Console.WriteLine($"Tool category with ID {selectedId} not found.");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please insert element and new content. Both, sould not be null.");
            }
        }

        /// <summary>
        /// Delete existing tool category
        /// </summary>
        /// <param name="categoryIdToRemove"></param>
        public override void DeleteItem(int categoryIdToRemove)
        {
            // Get Content (objects) from XML
            var categoryXdoc = _storageAccess.XmlDoc(source: _source);

            // Find tool category element with specified ID
            var categoryToRemove = (from category in categoryXdoc.Descendants("ToolCategory")
                                    where (int)category.Element("CategoryId") == categoryIdToRemove
                                    select category).FirstOrDefault();

            if (categoryToRemove != null)
            {
                // Remove a category element from XML
                categoryToRemove.Remove();
                Console.WriteLine("Tool category successfully removed...");
            }
            else
            {
                Console.WriteLine($"Tool category with ID {categoryIdToRemove} not found...");
            }

            // Save back the changes to XML-File
            categoryXdoc.Save(_source);
            Console.WriteLine("Tool category XML saved with changes...");
        }

        /// <summary>
        /// Show existing tool category
        /// </summary>
        public override void ShowItem()
        {
            // Get dezerialized content from XML
            var toolCategoryObjects = _storageAccess.DeserializeXmlObjects<List<ToolCategory>>(_source);

            // check, if object is not null
            if (toolCategoryObjects != null)
            {
                int count = 0;
                // Display tool category data on screen 
                foreach (var toolCategory in toolCategoryObjects)
                {
                    Console.WriteLine($"******************************** Tool Category Data {++count} **********************************");
                    Console.WriteLine($"Tool category ID:   {toolCategory.CategoryId}\n" +
                                      $"Name of Category:   {toolCategory.Name}\n" +
                                      $"Description:        {toolCategory.Description}\n" +
                                      $"Price per day:      {toolCategory.PricePerDay}\n" +
                                      "****************************************************************************************\n");
                }
            }
            else
            {
                // Error Message
                Console.WriteLine("Error: Result of deserialization is null.");
            }
        }

        /// <summary>
        /// Check for tool category Ids, if category Id currently exists in xml file.
        /// Its not allowed that there are double Ids.
        /// </summary>
        /// <param name="source">source file path</param>
        /// <returns>Check if Id currently exists in xml file</returns>
        private bool checkCategoryIds(string source)
        {
            // flag checked
            bool isNotDouble = true;

            // Get Content (objects) from XML
            var categoryXdoc = _storageAccess.XmlDoc(source: source);

            // Linq-Query: Give back all tool category Ids from XML-File
            var categoryIdsToAdd = from category in categoryXdoc.Descendants("ToolCategory")
                                   where category.Element("CategoryId") != null
                                   select (int)category.Element("CategoryId");

            // Add all existing tool category Ids to List
            _ids.AddRange(categoryIdsToAdd);
            _ids.Sort();

            foreach (var item in _categories as IEnumerable<ToolCategory>)
            {
                foreach (var id in _ids)
                {
                    if (item.CategoryId.Equals(id))
                    {
                        isNotDouble = false;
                    }
                }
            }

            return isNotDouble;
        }

        /// <summary>
        /// Combine new tool categories from list with existing tool categories in XML-File
        /// </summary>
        /// <param name="Items">tool category objects</param>
        private void MergeDezerializedItems(List<ToolCategory> Items)
        {
            foreach (var item in Items)
            {
                _categories.Add(item);
            }
        }

    }
}

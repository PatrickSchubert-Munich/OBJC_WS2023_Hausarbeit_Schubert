using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// CRUD-Operations class for tools
    /// </summary>
    public class ToolManagement : CRUDManager<Tool>
    {
        public ToolManagement() { }

        public ToolManagement(IStorageHandler storageAccess)
        {
            _storageAccess = storageAccess;
        }
        public ToolManagement(IStorageHandler storageAccess, string source) : this(storageAccess)
        {
            if (source == null) return;
            _source = source;
        }

        public ToolManagement(IStorageHandler storageAccess, string source, List<Tool> tools) : this(storageAccess, source)
        {
            _tools = tools;
        }

        private IStorageHandler _storageAccess { get; }
        private List<Tool> _tools { get; }
        private string _source { get; }

        /// <summary>
        /// Get dezerialized tool objects from xml file
        /// </summary>
        /// <returns>tool objects</returns>
        public override List<Tool> GetItem()
        {
            return _storageAccess.DeserializeXmlObjects<List<Tool>>(_source);
        }

        /// <summary>
        /// Create a new tool
        /// </summary>
        public override void CreateItem()
        {
            if (File.Exists(_source))
            {
                // Get Content (objects) from XML-File
                var dezerializedToolObj = _storageAccess.DeserializeXmlObjects<List<Tool>>(_source);

                if (dezerializedToolObj != null)
                {
                    // Add dezerialized tool objects from XML to new tools from List
                    MergeDezerializedItems(dezerializedToolObj);
                }
            }

            // Serialize tools (consists new customers and existing customers)
            _storageAccess.SerializeXmlListOfObjects(_source, _tools);

            // Write Message to console if Object is successful created
            Console.WriteLine();
            Console.WriteLine($"Tool successfully created...");
            Console.WriteLine();
        }

        /// <summary>
        /// Edit an existing tool
        /// </summary>
        /// <param name="selectedId">selected tool Id</param>
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

                    // Linq-Query for tools
                    var customerXDoc = (from tool in customersXdoc.Descendants("Tool")
                                        where (int)tool.Element("ToolId") == selectedId
                                        select tool).FirstOrDefault();

                    if (customerXDoc != null)
                    {
                        // Change element in XML-File with newContent (new value)
                        customerXDoc.Element(element).Value = newContent;
                        customersXdoc.Save(_source);
                        Console.WriteLine($"Tool {element} successfully changed to {newContent}.");
                    }
                    else
                    {
                        Console.WriteLine($"Tool with ID {selectedId} not found.");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please insert element and new content. Both, sould not be null.");
            }
        }

        /// <summary>
        /// Delete an existing tool
        /// </summary>
        /// <param name="toolIdToRemove"></param>
        public override void DeleteItem(int toolIdToRemove)
        {
            // Get Content (objects) from XML
            var toolXdoc = _storageAccess.XmlDoc(source: _source);

            // Find the tools element with the specified ID
            var toolToRemove = (from tool in toolXdoc.Descendants("Tool")
                                where (int)tool.Element("ToolId") == toolIdToRemove
                                select tool).FirstOrDefault();

            if (toolToRemove != null)
            {
                // Remove the tools element from the XML
                toolToRemove.Remove();
                Console.WriteLine("Tool successfully removed...");
            }
            else
            {
                Console.WriteLine($"Tool with ID {toolIdToRemove} not found...");
            }

            // Save back the changes to XML-File
            toolXdoc.Save(_source);
            Console.WriteLine("Tool XML saved with changes...");
        }

        /// <summary>
        /// Show existing tool data
        /// </summary>
        public override void ShowItem()
        {
            // Get dezerialized content from XML
            var toolObjects = _storageAccess.DeserializeXmlObjects<List<Tool>>(_source);

            // check, if object is not null
            if (toolObjects != null)
            {
                int count = 0;
                // Show tools data on screen 
                foreach (var tool in toolObjects)
                {
                    Console.WriteLine($"*************************** Tool Data {++count} *****************************");
                    Console.WriteLine($"Tool ID:            {tool.ToolId}\n" +
                                      $"Category ID:        {tool.CategoryId}\n" +
                                      $"Manufactorer:       {tool.Manufactorer}\n" +
                                      $"Name of Tool:       {tool.Description}\n" +
                                      $"Power supply:       {tool.PowerSupply}\n" +
                                      "*********************************************************************\n");
                }
            }
            else
            {
                // Error Message
                Console.WriteLine("Error: Result of deserialization is null.");
            }
        }

        /// <summary>
        /// Combine new tools from list with existing tools in XML-File
        /// </summary>
        /// <param name="Items">tool object</param>
        private void MergeDezerializedItems(List<Tool> Items)
        {
            foreach (var item in Items)
            {
                _tools.Add(item);
            }
        }

        /// <summary>
        /// Read highest tool Id from xml file
        /// </summary>
        /// <returns>highest tool Id</returns>
        public int ReadToolIDs()
        {
            if (!File.Exists(_source))
            {
                return 0;
            }

            // Get Content (objects) from XML
            var toolXdoc = _storageAccess.XmlDoc(source: _source);

            // Linq-Query: Give back all tool Ids from XML-File
            var toolIdsToAdd = from tool in toolXdoc.Descendants("Tool")
                               where tool.Element("ToolId") != null
                               select (int)tool.Element("ToolId");

            // Add all existing tool Ids to List
            return toolIdsToAdd.Max();
        }
    }
}

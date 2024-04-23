using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// CRUD-Operations class for lendings
    /// </summary>
    public class LendingManagement : CRUDManager<Lending>
    {
        public LendingManagement(IStorageHandler storageAccess)
        {
            _storageAccess = storageAccess;
        }

        public LendingManagement(IStorageHandler storageAccess, string source) : this(storageAccess)
        {
            if (source == null) return;
            _source = source;
        }

        public LendingManagement(IStorageHandler storageAccess, string source, List<Lending> lendings) : this(storageAccess, source)
        {
            _lendings = lendings;
        }
        private List<Lending> _lendings { get; }
        public IStorageHandler _storageAccess { get; }
        private string _source { get; }

        /// <summary>
        /// Get dezerialized lending objects from xml file
        /// </summary>
        /// <returns>lending objects</returns>
        public override List<Lending> GetItem()
        {
            return _storageAccess.DeserializeXmlObjects<List<Lending>>(_source);
        }

        /// <summary>
        /// Create a new lending
        /// </summary>
        public override void CreateItem()
        {
            if (File.Exists(_source))
            {
                // Get Content (objects) from XML-File
                var dezerializedToolObj = _storageAccess.DeserializeXmlObjects<List<Lending>>(_source);

                // Add kundenObjects from XML to ExistingCustomers List
                if (dezerializedToolObj != null)
                {
                    MergeDezerializedItems(dezerializedToolObj);
                }
            }

            // Serialize customers (consists new customers and existing customers)
            _storageAccess.SerializeXmlListOfObjects(_source, _lendings);

            // Write Message to console if Object is successful created
            Console.WriteLine();
            Console.WriteLine($"Lending successfully created...");
            Console.WriteLine();
        }

        /// <summary>
        /// Delete an existing lending
        /// </summary>
        /// <param name="lendingIdToRemove"></param>
        public override void DeleteItem(int lendingIdToRemove)
        {
            // Get Content (objects) from XML
            var lendingXdoc = _storageAccess.XmlDoc(source: _source);

            // Find the customer element with the specified ID
            var lendingToRemove = (from lending in lendingXdoc.Descendants("Lending")
                                   where (int)lending.Element("LendingId") == lendingIdToRemove
                                   select lending).FirstOrDefault();

            if (lendingToRemove != null)
            {
                // Remove the customer element from the XML
                lendingToRemove.Remove();
                Console.WriteLine("Lending successfully removed...");
            }
            else
            {
                Console.WriteLine($"Lending with ID {lendingIdToRemove} not found...");
            }

            // Save back changes to XML-File
            lendingXdoc.Save(_source);
            Console.WriteLine("Lending XML saved with changes...");
        }

        /// <summary>
        /// Edit an existing lending
        /// </summary>
        /// <param name="selectedId">selected lending Id</param>
        /// <param name="element">which xml element</param>
        /// <param name="newContent">new content for xml element</param>
        public override void EditItem(int selectedId, string element, string newContent)
        {
            try
            {
                if (element != null && newContent != null)
                {
                    // Get Content (objects) from XML
                    var lendingXdoc = _storageAccess.XmlDoc(source: _source);

                    // Linq-Query for customer
                    var lendingXDoc = (from lending in lendingXdoc.Descendants("Lending")
                                       where (int)lending.Element("LendingId") == selectedId
                                       select lending).FirstOrDefault();

                    if (lendingXDoc != null)
                    {
                        // Change element in XML-File with newContent (new value)
                        lendingXDoc.Element(element).Value = newContent;
                        lendingXdoc.Save(_source);
                        Console.WriteLine($"Lending {element} successfully changed to {newContent}.");
                    }
                    else
                    {
                        Console.WriteLine($"Lending with ID {selectedId} not found.");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please insert element and new content. Both, sould not be null.");
            }
        }

        /// <summary>
        /// Show existing Customers
        /// </summary>
        public override void ShowItem()
        {
            // Get dezerialized content from XML
            var lendingObjects = _storageAccess.DeserializeXmlObjects<List<Lending>>(_source);

            // check, if object is not null
            if (lendingObjects != null)
            {
                int count = 0;
                // Show customer data on screen 
                foreach (var lending in lendingObjects)
                {
                    if (lending != null)
                    {
                        Console.WriteLine($"*************************** Lending Data {++count} ************************");
                        Console.WriteLine($"Lemding - Id:                 {lending.LendingId}\n" +
                                          $"Customer - Id:                {lending.CustomerId}\n" +
                                          $"Customers Name:               {lending.CustomerFullName}\n" +
                                          $"Address:                      {lending.CustomerAdress}\n" +
                                          $"Tool Category:                {lending.CategoryId}\n" +
                                          $"Tool Id:                      {lending.ToolId}\n" +
                                          $"Tool manufactorer:            {lending.ToolManufactorer}\n" +
                                          $"Tool name:                    {lending.ToolName}\n" +
                                          $"Lending begins:               {lending.LendingBegin}\n" +
                                          $"Duration of lending:          {lending.LendingDuration} Tage\n" +
                                          $"Lending ends:                 {lending.LendingEnd}\n" +
                                          $"Whole costs of lending:       {lending.LendingCosts} €\n" +
                                          "********************************************************************\n");
                    }
                }
            }
            else
            {
                // Error Message
                Console.WriteLine("No Lendings available.");
            }
        }

        /// <summary>
        /// Combine new lendings from list with existing lendings in XML-File
        /// </summary>
        /// <param name="Items">lending object</param>
        private void MergeDezerializedItems(List<Lending> Items)
        {
            foreach (var item in Items)
            {
                _lendings.Add(item);
            }
        }

        /// <summary>
        /// Read highest lending Id from xml file
        /// </summary>
        /// <returns>highest lending Id</returns>
        public int ReadLendingIDs()
        {
            // Check, if XML-File exists
            if (!File.Exists(_source))
            {
                return 0;
            }

            // Get Content (objects) from XML
            var lendingXdoc = _storageAccess.XmlDoc(source: _source);

            // Linq-Query: Give back all customer Ids from XML-File
            var lendingIdsToAdd = from lending in lendingXdoc.Descendants("Lending")
                                  where lending.Element("LendingId") != null
                                  select (int)lending.Element("LendingId");

            // Add all existing customer Ids to List
            return lendingIdsToAdd.Max();
        }
    }
}

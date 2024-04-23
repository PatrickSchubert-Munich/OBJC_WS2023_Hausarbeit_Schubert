using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// CRUD-Operations class for customers
    /// </summary>
    public class CustomerManagement : CRUDManager<Customer>
    {
        public CustomerManagement() { }

        public CustomerManagement(IStorageHandler storageAccess, string source)
        {
            if (source == null) return;
            _source = source;
            _storageAccess = storageAccess;
        }
        public CustomerManagement(IStorageHandler storageAccess, string source, List<Customer> customers) : this(storageAccess, source)
        {
            _customers = customers;
        }
        private IStorageHandler _storageAccess { get; }
        private List<Customer> _customers { get; }
        private string _source { get; }

        /// <summary>
        /// Get dezerialized customer object from xml file
        /// </summary>
        /// <returns>customer objects</returns>
        public override List<Customer> GetItem()
        {
            return _storageAccess.DeserializeXmlObjects<List<Customer>>(_source);
        }

        /// <summary>
        /// Create a new customers
        /// </summary>
        public override void CreateItem()
        {
            if (File.Exists(_source))
            {
                // Get Content from XML-File
                var dezerializedCustObjects = _storageAccess.DeserializeXmlObjects<List<Customer>>(_source);

                if (dezerializedCustObjects != null && dezerializedCustObjects.Count > 0)
                {
                    // Add dezerialized customer objects from XML to new Customers from List
                    MergeCustomers(dezerializedCustObjects);
                }
            }

            // Serialize all customer objects in List format
            _storageAccess.SerializeXmlListOfObjects(_source, _customers);

            // Write Message to console
            Console.WriteLine();
            Console.WriteLine($"Customers successfully created.");
            Console.WriteLine();
        }

        /// <summary>
        /// Edit an existing customers
        /// </summary>
        /// <param name="selectedId">selected customer Id</param>
        /// <param name="element">which xml element</param>
        /// <param name="newContent">new content for xml element</param>
        public override void EditItem(int selectedId, string element, string newContent)
        {
            try
            {
                if (element != null && newContent != null)
                {
                    // Get Content (objects) from XML document
                    var customersXdoc = _storageAccess.XmlDoc(source: _source);

                    // Linq-Query for customer
                    var customerXDoc = (from customer in customersXdoc.Descendants("Customer")
                                        where (int)customer.Element("Id") == selectedId
                                        select customer).FirstOrDefault();

                    if (customerXDoc != null)
                    {
                        // Change element in XML-File with newContent (new value)
                        customerXDoc.Element(element).Value = newContent;
                        customersXdoc.Save(_source);
                        Console.WriteLine($"Customer {element} successfully changed to {newContent}.");
                    }
                    else
                    {
                        Console.WriteLine($"Customer with ID {selectedId} not found.");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please insert element and new content. Both, sould not be null.");
            }
        }

        /// <summary>
        /// Delete an existing customers
        /// </summary>
        /// <param name="lendingIdToRemove">customer Id which should remove</param>
        public override void DeleteItem(int lendingIdToRemove)
        {
            try
            {
                // Get Content (objects) from XML
                var customersXdoc = _storageAccess.XmlDoc(source: _source);

                // Find the customer element with the specified ID
                var customerToRemove = (
                    from customer in customersXdoc.Descendants("Customer")
                    where (int)customer.Element("Id") == lendingIdToRemove
                    select customer
                ).FirstOrDefault();

                if (customerToRemove != null)
                {
                    // Remove the customer element from the XML
                    customerToRemove.Remove();
                    Console.WriteLine("Customer removed successfully.");
                }
                else
                {
                    Console.WriteLine($"Customer with ID {lendingIdToRemove} not found.");
                }

                // Save back changes to XML-File
                customersXdoc.Save(_source);
                Console.WriteLine("XML saved with changes.");
            }
            catch (NullReferenceException ex)
            {
                // Error Message
                Console.WriteLine($"Error: {ex.Message}. NullReferenceException during deserialization. Object: {ex.Source}");
                throw;
            }
            catch (FileNotFoundException ex)
            {
                // Error Message
                Console.WriteLine($"Error: {ex.Message}. File does not exist or File is not on the correct Path. Object: {ex.Source}");
                throw;
            }

        }

        /// <summary>
        /// Show existing Customers
        /// </summary>
        public override void ShowItem()
        {
            // Get dezerialized content from XML
            var customerObjects = _storageAccess.DeserializeXmlObjects<List<Customer>>(_source);

            // check, if object is not null
            if (customerObjects != null)
            {
                int count = 0;
                // Show customer data on screen 
                foreach (var kunde in customerObjects)
                {
                    // Check also, if Customer is not null
                    if (kunde != null)
                    {
                        Console.WriteLine($"********************** Customer Data {++count} *******************");
                        Console.WriteLine($"Surname:        {kunde.Surname}\n" +
                                          $"Name:           {kunde.Name}\n" +
                                          $"Gender:         {kunde.Gender}\n" +
                                          $"Birthday:       {kunde.Birthday.Day}.{kunde.Birthday.Month}.{kunde.Birthday.Year}\n" +
                                          $"Address:        {kunde.Address}\n" +
                                          $"Customer Type:  {kunde.CustomerType}\n" +
                                          $"Phone number:   {kunde.TelephoneNumber}\n" +
                                          $"Bank account:   {kunde.BankAccount}\n" +
                                          "**********************************************************\n");
                    }
                }
            }
            else
            {
                // Error Message
                Console.WriteLine("Error: Result of deserialization is null.");
            }
        }

        /// <summary>
        /// Combine new customers from list with existing customers in XML-File
        /// </summary>
        /// <param name="ExistingCustomers">customer objects</param>
        private void MergeCustomers(List<Customer> ExistingCustomers)
        {
            foreach (var existingCustomer in ExistingCustomers)
            {
                _customers.Add(existingCustomer);
            }
        }

        /// <summary>
        /// Read highest customer Id from xml file
        /// </summary>
        /// <returns>highest customer Id</returns>
        public int ReadCustomerIDs()
        {
            // Check, if XML-File exists
            if (!File.Exists(_source))
            {
                return 0;
            }

            // Get Content (objects) from XML
            var customersXdoc = _storageAccess.XmlDoc(source: _source);

            // Linq-Query: Give back all customer Ids from XML-File
            var customerIdsToAdd = from customer in customersXdoc.Descendants("Customer")
                                   where customer.Element("Id") != null
                                   select (int)customer.Element("Id");

            // Add all existing customer Ids to List
            return customerIdsToAdd.Max();
        }
    }
}

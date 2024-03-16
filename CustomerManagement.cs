using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Werkzeugverleih
{
    /// <summary>
    /// Generic Class because it´s possible instantiating
    /// special customers like Gescheaftskunden, Privatkunden, ...
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomerManagement<T>
    {
        // Filepath and Filename to save Xml-File
        private string FilePath = @"C:\Users\Patri\OneDrive\Dokumente\VaWi\C_Sharp\Pruefungsleistung\OBJC_WS2023_Hausarbeit_Schubert";
        private string FileName = @"Kundendatei.xml";
        // customer-object
        private T _customer;
        // List of new customer Objects -> advantage: initialization does not depend on any constructor
        private List<T> NewCustomers = new List<T>();


        /// <summary>
        /// Constructor which use dependency Injection (DI) --> instantiate a special Customer
        /// </summary>
        /// <param name="customer">Object from special Customer</param>
        public CustomerManagement(T customer)
        {
            // new Customer
            _customer = customer;

            // Add new Customer to list
            AddCustomerToList();
        }

        public CustomerManagement() { }

        public void CreateCustomers()
        {
            // Joined FilePath and FileName
            var source = Path.Combine(FilePath, FileName);

            // Check, if XML-File exists
            if (XmlHandler.CheckXmlFileExists(source))
            {
                // Get Content (objects) from XML-File
                var kundenObjects = XmlHandler.DeserializeXmlObjects<List<T>>(source);

                // Add kundenObjects from XML to ExistingCustomers List
                MergeCustomers(kundenObjects);

                // Serialize customers (consists new customers and existing customers)
                XmlHandler.SerializeXmlListOfObjects(source, NewCustomers);
                Console.WriteLine($"Customers successfully created.");
            }

        }


        public void EditCustomers(int selectedId, string element, string newContent)
        {
            try
            {
                if (element != null && newContent != null)
                {
                    // Get Content (objects) from XML
                    var customersXdoc = XmlHandler.XmlDoc(Path.Combine(FilePath, FileName));

                    // Linq-Query for customer
                    var customerXDoc = (from customer in customersXdoc.Descendants("Customer")
                                        where (int)customer.Element("Id") == selectedId
                                        select customer).FirstOrDefault();

                    if (customerXDoc != null)
                    {
                        // Change element in XML-File with newContent (new value)
                        customerXDoc.Element(element).Value = newContent;
                        customersXdoc.Save(Path.Combine(FilePath, FileName));
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

        public void DeleteCustomers(int customerIdToRemove)
        {
            try
            {
                // Get Content (objects) from XML
                var customersXdoc = XmlHandler.XmlDoc(Path.Combine(FilePath, FileName));

                // Find the customer element with the specified ID
                var customerToRemove = (
                    from customer in customersXdoc.Descendants("Customer")
                    where (int)customer.Element("Id") == customerIdToRemove
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
                    Console.WriteLine($"Customer with ID {customerIdToRemove} not found.");
                }

                // Save back changes to XML-File
                customersXdoc.Save(Path.Combine(FilePath, FileName));
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
        /// Show Customer data.
        /// </summary>
        public void ShowCustomerData()
        {
            try
            {
                // Get dezerialized content from XML
                var customerObjects = XmlHandler.DeserializeXmlObjects<List<Customer>>(Path.Combine(FilePath, FileName));

                // check, if object is not null
                if (customerObjects != null)
                {
                    // Show customer data on screen 
                    foreach (var kunde in customerObjects)
                    {
                        // Check also, if Customer is not null
                        if (kunde != null)
                        {
                            Console.WriteLine("**************** KUNDENDATEN **************");
                            Console.WriteLine($"Surname   : {kunde.Surname},\n" +
                                              $"Name  : {kunde.Name},\n" +
                                              $"Gender: {kunde.Gender},\n" +
                                              $"Geb.-Datum: {kunde.Birthday},\n" +
                                              $"Address   : {kunde.Address},\n" +
                                              $"CustomerType : {kunde.CustomerType},\n" +
                                              $"Tel.-Nr.  : {kunde.TelephoneNumber},\n" +
                                              $"Bankverb. : {kunde.BankAccount},\n" +
                                              "*******************************************\n");
                        }
                    }
                }
                else
                {
                    // Error Message
                    Console.WriteLine("Error: Result of deserialization is null.");
                }
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
                Console.WriteLine($"Error: {ex.Message}. File does not exist or File is not on correct Path. Object: {ex.Source}");
                throw;
            }

        }

        /// <summary>
        /// Add KundenObject to List and set up IdCounter.
        /// </summary>
        private void AddCustomerToList()
        {
            NewCustomers.Add(this._customer);
        }

        /// <summary>
        /// Combine new Customers from instantiating and existing customers from XML-File.
        /// </summary>
        private void MergeCustomers(List<T> ExistingCustomers)
        {
            foreach (var existingCustomer in ExistingCustomers)
            {
                NewCustomers.Add(existingCustomer);
            }
        }

    }

}

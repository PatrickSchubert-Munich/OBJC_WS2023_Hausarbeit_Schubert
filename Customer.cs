using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Werkzeugverleih
{
    public class Customer
    {
        // Filepath and Filename to save Xml-File
        [XmlIgnore]
        private string FilePath = @"C:\Users\Patri\OneDrive\Dokumente\VaWi\C_Sharp\Pruefungsleistung\OBJC_WS2023_Hausarbeit_Schubert";
        [XmlIgnore]
        private string FileName = @"Kundendatei.xml";

        // List for all existing customer Ids from XML-File
        [XmlIgnore]
        private List<int> AllCustomerIds = new List<int>();

        // Properties
        [XmlElement("Id")]
        public int CustomerId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Birthday { get; set; }

        [XmlElement("Age")]
        public int Age
        {
            get
            {
                // save todays date.
                var today = DateTime.Today;
                var birthday = DateTime.Parse(Birthday);
                // calculate age 
                int age = today.Year - birthday.Year;
                return age;
            }
            set { }

        }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string BankAccount { get; set; }

        [XmlAttribute("type")]
        public string CustomerType { get; set; }

        public string TelephoneNumber { get; set; }

        [XmlAttribute("rating")]
        public int Rating { get; set; }


        // parameterless constructor
        public Customer() { }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="customerId">unique ID</param>
        /// <param name="surname">customers surname</param>
        /// <param name="name">customers name</param>
        /// <param name="gender">customers gender</param>
        /// <param name="birthday">customers birthday</param>
        /// <param name="address">customers address</param>
        /// <param name="bankAccount">customers account of bank (IBAN)</param>
        /// <param name="customerType">customer type like privat or bussiness</param>
        /// <param name="telephoneNumber">customers telephone number</param>
        /// <param name="rating">customers rating</param>
        public Customer(string surname, string name, string gender, string birthday,
                        string address, string bankAccount, string customerType,
                        string telephoneNumber, int rating)
        {
            this.CustomerId = ReadCustomerIDs();
            this.Surname = surname;
            this.Name = name;
            this.Gender = gender;
            this.Birthday = ConvertBirthday(birthday);
            this.Address = address;
            this.BankAccount = bankAccount;
            this.CustomerType = customerType;
            this.TelephoneNumber = telephoneNumber;
            this.Rating = rating;
        }


        /// <summary>
        /// Check format of customers birthday
        /// </summary>
        /// <param name="birthday">birthday of customer</param>
        /// <returns></returns>
        private string ConvertBirthday(string birthday)
        {
            var defaultBirthday = DateTime.Now.Date;

            try
            {

                if (DateTime.TryParse(birthday, out defaultBirthday))
                {
                    var formatBirthday = Convert.ToDateTime(birthday);
                    return $"{formatBirthday.Day}.{formatBirthday.Month}.{formatBirthday.Year}";
                };
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Birthday is not in the correct Format: DD.MM.YYYY");
                throw;
            }

            return $"{defaultBirthday.Day}.{defaultBirthday.Month}.{defaultBirthday.Year}";
        }

        /// <summary>
        /// Read existing Customer Ids from XML-File to create
        /// a unique Ids (key) for new customers
        /// </summary>
        /// <returns>Amount of new customer Ids</returns>
        public int ReadCustomerIDs()
        {
            // Joined FilePath and FileName
            var source = Path.Combine(FilePath, FileName);
            // Check, if XML-File exists
            if (XmlHandler.CheckXmlFileExists(source))
            {
                // Get Content (objects) from XML
                var customersXdoc = XmlHandler.XmlDoc(source);

                // Linq-Query: Give back all customer Ids from XML-File
                var customerIdsToAdd = from customer in customersXdoc.Descendants("Customer")
                                       where customer.Element("Id") != null
                                       select (int)customer.Element("Id");

                // Add all existing customer Ids to List
                AllCustomerIds.AddRange(customerIdsToAdd);
                AllCustomerIds.Sort();
            }

            // New Customer ID
            return this.CustomerId = AllCustomerIds.Last() + 1; // also possible: AllCustomerIds[-1]
        }

    }

}



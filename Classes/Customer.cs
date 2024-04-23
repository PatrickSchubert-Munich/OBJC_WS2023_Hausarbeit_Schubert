using System.Xml.Serialization;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Data class for Customers
    /// </summary>
    public class Customer
    {
        // counter for customer Ids
        public static int countId;
        [XmlElement("Id")]
        public int CustomerId { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        [XmlElement("Age")]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - Birthday.Year;
                return age;
            }
        }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string BankAccount { get; set; }

        public string CustomerType { get; set; }

        public string TelephoneNumber { get; set; }

        public int Rating { get; set; }

        public Customer() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="surname">customers surname</param>
        /// <param name="name">customers name</param>
        /// <param name="gender">customers gender</param>
        /// <param name="birthday">customers birthday</param>
        /// <param name="address">customers address</param>
        /// <param name="bankAccount">customers account of bank (IBAN)</param>
        /// <param name="customerType">customer type like privat or bussiness</param>
        /// <param name="telephoneNumber">customers telephone number</param>
        /// <param name="rating">customers rating</param>
        public Customer(string surname, string name, string gender, DateTime birthday,
                        string address, string bankAccount, string customerType,
                        string telephoneNumber, int rating)
        {
            CustomerId = ++countId;
            Surname = surname;
            Name = name;
            Gender = gender;
            Birthday = birthday.Date;
            Address = address;
            BankAccount = bankAccount;
            CustomerType = customerType;
            TelephoneNumber = telephoneNumber;
            Rating = rating;
        }
    }
}



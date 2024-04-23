using System.Xml.Serialization;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Select customer which lends a tool
    /// </summary>
    public class LendingCustomer
    {
        public int _customerId;
        public List<Customer> ListOfCustomers = new List<Customer>();
        private CustomerManagement _customerManagement;

        /// <summary>
        /// Constructor for LendingCustomer
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        /// <param name="customerManagement">Instance of customers Management class</param>
        public LendingCustomer(int customerId, CustomerManagement customerManagement)
        {
            _customerId = customerId;
            _customerManagement = customerManagement;
        }

        public int CustomerId
        {
            get
            {
                return _customerId;
            }
        }

        [XmlIgnore]
        public List<Customer> Customer
        {
            get
            {
                var customers = _customerManagement.GetItem();

                foreach (var cust in customers)
                {
                    if (cust.CustomerId == _customerId)
                    {
                        ListOfCustomers.Add(cust);
                    }
                }
                return ListOfCustomers;
            }
        }
    }
}

using Werkzeugverleih.Classes;
using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Screens
{
    /// <summary>
    /// Screen for Customers
    /// </summary>
    public static class ScreenCustomerManagement
    {
        public static string userInputChar = null;
        public static int userInputNumber;
        public const int QUITT_MAIN_MENUE = 5;
        public static List<Customer> listOfCustomers = new List<Customer>();
        public static IReadPath _ReadFilePathCustomer = new ReadConfiguration();
        public static IStorageHandler _backgroundStorageCustomer = new XmlHandler<Customer>();

        /// <summary>
        /// Showing Screen for customers
        /// </summary>
        /// <returns>user input</returns>
        public static int ShowScreenCustomerManagement()
        {
            Console.Clear();
            Console.WriteLine("*********************************************************");
            Console.WriteLine("******************* Menue Customer **********************");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("1 Create Customers");
            Console.WriteLine("2 Edit Customers");
            Console.WriteLine("3 Delete Customers");
            Console.WriteLine("4 Display Customers");
            Console.WriteLine("5 Back to main menue");
            Console.WriteLine("*********************************************************");
            Console.Write("Please select menue item and hit enter: ");

            userInputNumber = ConvertNumbers.ConvertInteger();

            switch (userInputNumber)
            {
                // Create new Customers
                case 1:
                    var checkCustomerIdObject = new CustomerManagement(storageAccess: _backgroundStorageCustomer,
                                                                      source: _ReadFilePathCustomer.ReadFilePath("Customers"));
                    Customer.countId = checkCustomerIdObject.ReadCustomerIDs();

                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a first name: ");
                        var surname = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please enter a surname: ");
                        var name = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please enter a gender (m / w): ");
                        var gender = Console.ReadLine();
                        Console.WriteLine();

                        // auto correction of typo-mistakes
                        if (gender.Contains('m'))
                        {
                            gender = "m";
                        }
                        else gender = "w";

                        Console.WriteLine("Please enter the day of the birthday: ");
                        var day = ConvertNumbers.ConvertInteger();

                        Console.WriteLine("Please enter the month of the birthday: ");
                        var month = ConvertNumbers.ConvertInteger();

                        Console.WriteLine("Please enter the year of the birthday in followed format \"YYYY\": ");
                        var year = ConvertNumbers.ConvertInteger();

                        Console.WriteLine("Please enter the address: ");
                        var address = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please enter a valid bank account (i. e. IBAN): ");
                        var bankAccount = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please enter a customer type (privat / bussiness): ");
                        var customerType = Console.ReadLine();
                        Console.WriteLine();

                        // auto correction of typo-mistakes
                        if (customerType.Contains("priv") || customerType.Contains("vat"))
                        {
                            customerType = "privat";
                        }
                        else customerType = "bussiness";

                        Console.WriteLine("Please enter a valid phone number: ");
                        var telephoneNumber = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please enter actual rating of the customer (numeric value 1, 2, 3, 4 or 5): ");
                        var rating = ConvertNumbers.ConvertInteger();
                        Console.WriteLine();

                        // Customer anlegen
                        var customer = new Customer(surname: surname,
                                                    name: name,
                                                    gender: gender,
                                                    birthday: new DateTime(year, month, day),
                                                    address: address,
                                                    bankAccount: bankAccount,
                                                    customerType: customerType,
                                                    telephoneNumber: telephoneNumber,
                                                    rating: rating);

                        listOfCustomers.Add(customer);

                        Console.WriteLine("Do you want to add another customer? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }

                    var createCustomerObject = new CustomerManagement(storageAccess: _backgroundStorageCustomer,
                                                                      source: _ReadFilePathCustomer.ReadFilePath("Customers"),
                                                                      customers: listOfCustomers);
                    createCustomerObject.CreateItem();
                    listOfCustomers.Clear();
                    break;
                // Edit Customers
                case 2:
                    var editCustomerObject = new CustomerManagement(storageAccess: _backgroundStorageCustomer,
                                                                    source: _ReadFilePathCustomer.ReadFilePath("Customers"));
                    var customerElement = string.Empty;
                    userInputChar = "j";

                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the customers ID you want to edit: ");
                        var customerId = ConvertNumbers.ConvertInteger();
                        Console.WriteLine("You can edit the following properties");
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("1 Surname");
                        Console.WriteLine("2 Name");
                        Console.WriteLine("3 Birthday");
                        Console.WriteLine("4 Gender");
                        Console.WriteLine("5 Address");
                        Console.WriteLine("6 Bank Account");
                        Console.WriteLine("7 Customer Type");
                        Console.WriteLine("8 Phone Number");
                        Console.WriteLine("9 Rating");
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("Which property do you want to edit: ");

                        switch (ConvertNumbers.ConvertInteger())
                        {
                            case 1:
                                customerElement = "Surname";
                                break;
                            case 2:
                                customerElement = "Name";
                                break;
                            case 3:
                                customerElement = "Birthday";
                                break;
                            case 4:
                                customerElement = "Gender";
                                break;
                            case 5:
                                customerElement = "Address";
                                break;
                            case 6:
                                customerElement = "BankAccount";
                                break;
                            case 7:
                                customerElement = "CustomerType";
                                break;
                            case 8:
                                customerElement = "TelephoneNumber";
                                break;
                            case 9:
                                customerElement = "Rating";
                                break;
                            default:
                                break;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Please enter your changes: ");
                        var customerContent = Console.ReadLine();
                        if (customerContent != null)
                        {
                            editCustomerObject.EditItem(customerId, customerElement, customerContent);
                        }

                        Console.WriteLine();
                        Console.WriteLine("Do you want to make changes on another customer? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }
                    break;
                // Delete Customers
                case 3:
                    var deleteCustomerObject = new CustomerManagement(storageAccess: _backgroundStorageCustomer,
                                                                source: _ReadFilePathCustomer.ReadFilePath("Customers"),
                                                                customers: listOfCustomers);
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a customer ID you want to delete: ");
                        var idToDelete = ConvertNumbers.ConvertInteger();
                        Console.WriteLine();
                        deleteCustomerObject.DeleteItem(idToDelete);
                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete another category? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }

                    break;
                // Display Customers
                case 4:
                    var showCustomerObject = new CustomerManagement(storageAccess: _backgroundStorageCustomer,
                                                                      source: _ReadFilePathCustomer.ReadFilePath("Customers"));
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        showCustomerObject.ShowItem();
                        Console.WriteLine("To exit, press any key and/or press the Enter key...");
                        userInputChar = Console.ReadLine().ToUpper();
                    }

                    break;
                // Quitt Menue
                case 5:
                    userInputNumber = QUITT_MAIN_MENUE;
                    Console.Clear();
                    break;
                // Default case
                default:
                    Console.WriteLine("Unfortunately, your input cannot be read!");
                    break;
            }
            return userInputNumber;
        }
    }
}

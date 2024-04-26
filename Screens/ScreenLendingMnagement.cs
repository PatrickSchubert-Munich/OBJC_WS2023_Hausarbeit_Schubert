using Werkzeugverleih.Classes;
using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Screens
{
    /// <summary>
    /// Screen for lendings
    /// </summary>
    public static class ScreenLendingMnagement
    {
        public static string userInputChar = null;
        public static int userInputNumber;
        public const int QUITT_MAIN_MENUE = 5;
        public static List<Lending> listOfLendings = new List<Lending>();
        public static List<int> ListOfLendingToolIds = new List<int>();
        public static IReadPath _ReadFilePath = new ReadConfiguration();
        public static IStorageHandler _backgroundStorageCustomer = new XmlHandler<Customer>();
        public static IStorageHandler _backgroundStorageTool = new XmlHandler<Tool>();
        public static IStorageHandler _backgroundStorageToolCategory = new XmlHandler<ToolCategory>();
        public static IStorageHandler _backgroundStorageLendingManagement = new XmlHandler<LendingManagement>();

        /// <summary>
        /// Showing screen for lendings
        /// </summary>
        /// <returns>user input</returns>
        public static int ShowScreenLendingManagement()
        {
            Console.Clear();
            Console.WriteLine("*********************************************************");
            Console.WriteLine("******************** Menue Lending ***********************");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("1 Create new Lendings");
            Console.WriteLine("2 Edit Lendings");
            Console.WriteLine("3 Delete Lendings");
            Console.WriteLine("4 Display Lendings");
            Console.WriteLine("5 Back to main menue");
            Console.WriteLine("*********************************************************");
            Console.Write("Please select menue item and hit enter: ");

            userInputNumber = ConvertNumbers.ConvertInteger();

            switch (userInputNumber)
            {
                // Create new lendings
                case 1:
                    ToolManagement createLendingToolObject = new ToolManagement(storageAccess: _backgroundStorageTool,
                                                                                source: _ReadFilePath.ReadFilePath("Tools"));

                    CustomerManagement createLendingCustomerObject = new CustomerManagement(storageAccess: _backgroundStorageCustomer,
                                                                                            source: _ReadFilePath.ReadFilePath("Customers"));

                    ToolCategoryManagement createLendingToolCategoryObject = new ToolCategoryManagement(storageAccess: _backgroundStorageToolCategory,
                                                                                                        source: _ReadFilePath.ReadFilePath("ToolCategories"));

                    string custAdress = string.Empty;
                    string custFullName = string.Empty;
                    int customerId = -1;
                    decimal singleCosts = 0;
                    var toolId = -1;
                    TimeSpan lendingDays = TimeSpan.FromDays(0);

                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a customer Id: ");
                        var customId = ConvertNumbers.ConvertInteger(Console.ReadLine());

                        while (userInputChar.Equals("j"))
                        {
                            Console.WriteLine("Please enter a tool Id: ");
                            toolId = ConvertNumbers.ConvertInteger(Console.ReadLine());

                            Console.WriteLine("How many days would you like to borrow the tool? (input only in full days): ");
                            lendingDays = TimeSpan.FromDays(ConvertNumbers.ConvertInteger());

                            ListOfLendingToolIds.Add(toolId);

                            Console.WriteLine("Would you like to borrow another tool? Press key j or n: ");
                            userInputChar = Console.ReadLine().ToLower();
                            Console.WriteLine();
                        }

                        var lendingTool = new LendingTool(toolIds: ListOfLendingToolIds, toolManagement: createLendingToolObject);
                        var lendingCustomer = new LendingCustomer(customerId: customId, customerManagement: createLendingCustomerObject);
                        ListOfLendingToolIds.Clear();

                        var customer = lendingCustomer.Customer;
                        var tools = lendingTool.Tool;

                        foreach (var cust in customer)
                        {
                            if (cust.CustomerId == customId)
                            {
                                customerId = cust.CustomerId;
                                custAdress = cust.Address;
                                custFullName = cust.Surname + " " + cust.Name;
                            }
                        }

                        if (customerId != -1)
                        {
                            var toolCategories = createLendingToolCategoryObject.GetItem();

                            LendingManagement checkLendingIdObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                                          source: _ReadFilePath.ReadFilePath("Lendings"));

                            foreach (var tool in tools)
                            {
                                foreach (var toolCategory in toolCategories)
                                {
                                    if (toolCategory.CategoryId == tool.CategoryId)
                                    {
                                        singleCosts = toolCategory.PricePerDay;
                                        break;
                                    }
                                }

                                checkLendingIdObject.ReadLendingIDs();

                                var lending = new Lending(lendingDuration: lendingDays,
                                                          singleCosts: singleCosts,
                                                          customerId: customerId,
                                                          toolId: tool.ToolId,
                                                          categoryId: tool.CategoryId,
                                                          customerFullName: custFullName,
                                                          customerAdress: custAdress,
                                                          toolManufactorer: tool.Manufactorer,
                                                          toolName: tool.Description);

                                listOfLendings.Add(lending);
                            }

                            // LendingManagement aufrufen
                            var createLendingObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                            source: _ReadFilePath.ReadFilePath("Lendings"),
                                                                            lendings: listOfLendings);
                            createLendingObject.CreateItem();
                        }
                        else
                        {
                            Console.WriteLine("Customer with this Id not found! See in customers file which customer Ids you can choose.");
                            userInputChar = Console.ReadLine().ToLower();
                        }

                        listOfLendings.Clear();
                    }
                    break;
                // Edit lendings
                case 2:
                    var editLendingObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                  source: _ReadFilePath.ReadFilePath("Lendings"));

                    var lendingElement = string.Empty;
                    var lendingContent = string.Empty;

                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the lending Id you want to edit: ");
                        var lendingId = ConvertNumbers.ConvertInteger();
                        Console.WriteLine("You can edit the following properties");
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("1 Customer - Id");
                        Console.WriteLine("2 Customer name");
                        Console.WriteLine("3 Customer address");
                        Console.WriteLine("4 Tool - Id");
                        Console.WriteLine("5 Tool category");
                        Console.WriteLine("6 Tool manufactorer");
                        Console.WriteLine("7 Tool name");
                        Console.WriteLine("8 Whole lending costs");
                        Console.WriteLine("9 Lending duration");
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("Which property do you want to edit: ");

                        switch (ConvertNumbers.ConvertInteger())
                        {
                            case 1:
                                lendingElement = "CustomerId";
                                break;
                            case 2:
                                lendingElement = "CustomerFullName";
                                break;
                            case 3:
                                lendingElement = "CustomerAdress";
                                break;
                            case 4:
                                lendingElement = "ToolId";
                                break;
                            case 5:
                                lendingElement = "CategoryId";
                                break;
                            case 6:
                                lendingElement = "ToolManufactorer";
                                break;
                            case 7:
                                lendingElement = "ToolName";
                                break;
                            case 8:
                                lendingElement = "LendingCosts";
                                break;
                            case 9:
                                lendingElement = "LendingDuration";
                                break;
                            default:
                                break;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Please enter your change: ");

                        if (lendingElement != null)
                        {
                            if (!lendingElement.Equals("LendingCosts"))
                            {
                                lendingContent = Console.ReadLine();
                            }
                            else
                            {
                                lendingContent = Console.ReadLine().Replace(',', '.');
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("You didn't dial a number!");
                        }

                        if (editLendingObject != null)
                        {
                            editLendingObject.EditItem(lendingId, lendingElement, lendingContent);
                        }

                        Console.WriteLine();
                        Console.WriteLine("Do you want to make changes on another lending? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }
                    break;
                // Delete lendings
                case 3:
                    LendingManagement deleteLendingObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                            source: _ReadFilePath.ReadFilePath("Lendings"));

                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a lending Id you want to delete: ");
                        var idToDelete = ConvertNumbers.ConvertInteger();
                        Console.WriteLine();
                        deleteLendingObject.DeleteItem(idToDelete);
                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete another lending? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }
                    break;
                // Display lendings
                case 4:
                    var showLendingObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                    source: _ReadFilePath.ReadFilePath("Lendings"));
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        showLendingObject.ShowItem();
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

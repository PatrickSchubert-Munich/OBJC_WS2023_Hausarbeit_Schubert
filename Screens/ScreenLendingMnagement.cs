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
        public static IReadPath _backgroundReadSource = new ReadConfiguration();
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
                // Edit new lendings
                case 1:
                    ToolManagement ToolManagementObject = new ToolManagement(storageAccess: _backgroundStorageTool,
                                                                             source: _backgroundReadSource.ReadFilePath("Tools"));

                    CustomerManagement customerManagementObject = new CustomerManagement(storageAccess: _backgroundStorageCustomer,
                                                                                          source: _backgroundReadSource.ReadFilePath("Customers"));

                    ToolCategoryManagement toolCategoryManagementObject = new ToolCategoryManagement(storageAccess: _backgroundStorageToolCategory,
                                                                                                     source: _backgroundReadSource.ReadFilePath("ToolCategories"));

                    LendingManagement lendingManagementObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                                      source: _backgroundReadSource.ReadFilePath("Lendings"));

                    Lending.countLendingId = lendingManagementObject.ReadLendingIDs();

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
                            lendingDays = TimeSpan.FromDays(ConvertNumbers.ConvertInteger(Console.ReadLine()));

                            ListOfLendingToolIds.Add(toolId);

                            Console.WriteLine("Would you like to borrow another tool? Press key j or n: ");
                            userInputChar = Console.ReadLine().ToLower();
                            Console.WriteLine();
                        }


                        var lendingTool = new LendingTool(toolIds: ListOfLendingToolIds, toolManagement: ToolManagementObject);
                        var lendingCustomer = new LendingCustomer(customerId: customId, customerManagement: customerManagementObject);

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

                        var toolCategories = toolCategoryManagementObject.GetItem();

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
                    }

                    // LendingManagement aufrufen
                    var lendingCreateObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                            source: _backgroundReadSource.ReadFilePath("Lendings"),
                                                            lendings: listOfLendings);
                    lendingCreateObject.CreateItem();
                    listOfLendings.Clear();
                    break;
                // Edit lendings
                case 2:
                    var editLendingObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                  source: _backgroundReadSource.ReadFilePath("Lendings"));

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
                    // create object
                    LendingManagement lendingObject = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                            source: _backgroundReadSource.ReadFilePath("Lendings"));

                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a lending Id you want to delete: ");
                        var idToDelete = ConvertNumbers.ConvertInteger();
                        Console.WriteLine();
                        lendingObject.DeleteItem(idToDelete);
                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete another lending? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }
                    break;
                // Display lendings
                case 4:
                    var lendingObjectToShow = new LendingManagement(storageAccess: _backgroundStorageLendingManagement,
                                                                    source: _backgroundReadSource.ReadFilePath("Lendings"));
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        lendingObjectToShow.ShowItem();
                        Console.WriteLine("To exit, press any key and/or press the Enter key...");
                        userInputChar = Console.ReadLine().ToUpper();
                    }
                    break;
                // Quitt Menue
                case 5:
                    userInputNumber = QUITT_MAIN_MENUE;
                    //Program.backToMainMenue = Convert.ToString(QUITT_MAIN_MENUE);
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

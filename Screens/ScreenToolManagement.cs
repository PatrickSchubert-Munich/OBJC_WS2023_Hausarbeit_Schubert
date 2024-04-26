using Werkzeugverleih.Classes;
using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Screens
{
    /// <summary>
    /// Screen for Tools
    /// </summary>
    public static class ScreenToolManagement
    {
        public static string FilePath => @"C:\Users\Patri\OneDrive\Dokumente\VaWi\C_Sharp\Pruefungsleistung\OBJC_WS2023_Hausarbeit_Schubert\Filestore";
        public static string FileNameTool = @"Werkzeuge.xml";
        public static string userInputChar = null;
        public static int userInputNumber;
        public const int QUITT_MAIN_MENUE = 5;
        public static List<Tool> listOfTools = new List<Tool>();
        public static IReadPath _ReadFilePathTool = new ReadConfiguration();
        public static IStorageHandler _backgroundStorageTool = new XmlHandler<Tool>();

        /// <summary>
        /// Showing screen for tools
        /// </summary>
        /// <returns>user input</returns>
        public static int ShowScreenToolManagement()
        {
            Console.Clear();
            Console.WriteLine("*********************************************************");
            Console.WriteLine("********************* Menue Tools ***********************");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("1 Create new tools");
            Console.WriteLine("2 Edit tools");
            Console.WriteLine("3 Delete tools");
            Console.WriteLine("4 Display tools");
            Console.WriteLine("5 Back to main menue");
            Console.WriteLine("*********************************************************");
            Console.Write("Please select menue item and hit enter: ");

            userInputNumber = ConvertNumbers.ConvertInteger();

            switch (userInputNumber)
            {
                // Create new tools
                case 1:
                    var checkToolIdObject = new ToolManagement(storageAccess: _backgroundStorageTool,
                                                               source: _ReadFilePathTool.ReadFilePath("Tools"));
                    Tool.countId = checkToolIdObject.ReadToolIDs();

                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("\r\nPlease assign the tool to an ID from the tool category: ");
                        var toolCatId = ConvertNumbers.ConvertInteger();

                        Console.WriteLine("\r\nPlease enter the manufacturer: ");
                        var manufactorer = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please enter an identifier: ");
                        var description = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please enter a numeric value for the type of power supply: ");
                        Console.WriteLine("0: Battery pack");
                        Console.WriteLine("1: Cable");
                        Console.WriteLine("2: De-energized");
                        var powerSupply = ConvertNumbers.ConvertInteger();
                        Console.WriteLine();

                        var tool = new Tool(categoryId: toolCatId,
                                            manufactorer: manufactorer,
                                            description: description,
                                            powerSupply: powerSupply);

                        listOfTools.Add(tool);

                        Console.WriteLine("Do you want to add another tool? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }

                    var createToolObject = new ToolManagement(storageAccess: _backgroundStorageTool,
                                                            source: _ReadFilePathTool.ReadFilePath("Tools"),
                                                            tools: listOfTools);
                    createToolObject.CreateItem();
                    listOfTools.Clear();
                    break;
                // Edit tools
                case 2:
                    var editToolObject = new ToolManagement(storageAccess: _backgroundStorageTool,
                                                            source: _ReadFilePathTool.ReadFilePath("Tools"));

                    var toolElement = string.Empty;
                    var toolContent = string.Empty;
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the tool ID you want to edit: ");
                        var toolId = ConvertNumbers.ConvertInteger();
                        Console.WriteLine("You can edit the following properties");
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("1 Manufactorer");
                        Console.WriteLine("2 Description");
                        Console.WriteLine("3 Power supply");
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("Which property do you want to edit: ");

                        switch (ConvertNumbers.ConvertInteger())
                        {
                            case 1:
                                toolElement = "Manufactorer";
                                break;
                            case 2:
                                toolElement = "Description";
                                break;
                            case 3:
                                toolElement = "PowerSupply";
                                break;
                            default:
                                break;
                        }

                        if (toolElement.Equals("PowerSupply"))
                        {
                            Console.WriteLine("Please enter a numeric value for the type of power supply: ");
                            Console.WriteLine("0: Battery pack");
                            Console.WriteLine("1: Cable tied");
                            Console.WriteLine("2: De-energized");
                            var tempPowerSupply = ConvertNumbers.ConvertInteger();

                            if (tempPowerSupply.Equals(0))
                            {
                                toolContent = "BatteryPack";
                            }
                            else if (tempPowerSupply.Equals(1))
                            {
                                toolContent = "Cable";
                            }
                            else
                            {
                                toolContent = "DeEnergized";
                            }

                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please enter your change: ");
                            toolContent = Console.ReadLine();
                        }

                        if (toolContent != null)
                        {
                            editToolObject.EditItem(toolId, toolElement, toolContent);
                        }

                        Console.WriteLine();
                        Console.WriteLine("Do you want to edit another tool? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }
                    break;
                // Delete tools
                case 3:
                    var deleteToolObject = new ToolManagement(storageAccess: _backgroundStorageTool,
                                                        source: _ReadFilePathTool.ReadFilePath("Tools"),
                                                        tools: listOfTools);
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter the Id you want to delete: ");
                        var idToDelete = ConvertNumbers.ConvertInteger();

                        deleteToolObject.DeleteItem(idToDelete);

                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete another tool? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }
                    break;
                // Display tools
                case 4:
                    var showToolObject = new ToolManagement(storageAccess: _backgroundStorageTool,
                                                              source: _ReadFilePathTool.ReadFilePath("Tools"));
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        showToolObject.ShowItem();
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

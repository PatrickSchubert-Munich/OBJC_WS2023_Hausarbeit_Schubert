using Werkzeugverleih.Classes;
using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Screens
{
    /// <summary>
    /// Screen for Tool Categories
    /// </summary>
    public static class ScreenToolCategoryManagement
    {
        public static string userInputChar = null;
        public static int userInputNumber;
        public const int QUITT_MAIN_MENUE = 5;
        public static List<ToolCategory> listOfCategories = new List<ToolCategory>();
        public static IReadPath _backgroundReadSource = new ReadConfiguration();
        public static IStorageHandler _backgroundStorageToolCategory = new XmlHandler<ToolCategory>();

        /// <summary>
        /// Showing screen for tool categories
        /// </summary>
        /// <returns>user input</returns>
        public static int ShowScreenToolCategoryManagement()
        {
            Console.Clear();
            Console.WriteLine("*********************************************************");
            Console.WriteLine("****************** Menue Tool category ******************");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("1 Create new tool categories");
            Console.WriteLine("2 Edit tool categories");
            Console.WriteLine("3 Delete tool categories");
            Console.WriteLine("4 Display tool categories");
            Console.WriteLine("5 Back to main menue");
            Console.WriteLine("*********************************************************");
            Console.Write("Please select menue item and hit enter: ");

            userInputNumber = ConvertNumbers.ConvertInteger();

            switch (userInputNumber)
            {
                // Create new tool category
                case 1:
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please assign a unique Id for the tool category: ");
                        var toolCatId = ConvertNumbers.ConvertInteger();

                        Console.WriteLine("Please enter a name for your tool category: ");
                        var toolCategoryName = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please briefly describe the tool category: ");
                        var toolCategoryDescription = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("How much should the rental cost per day (numerical value): ");
                        var toolCategoryPricePerDay = ConvertNumbers.ConvertDecimal();
                        Console.WriteLine();

                        // Tool anlegen
                        var toolCategory = new ToolCategory(categoryId: toolCatId,
                                                            name: toolCategoryName,
                                                            description: toolCategoryDescription,
                                                            pricePerDay: toolCategoryPricePerDay);

                        listOfCategories.Add(toolCategory);


                        Console.WriteLine("Do you want to edit another lending? Press key j or n:");
                        userInputChar = Console.ReadLine().ToLower();
                    }

                    var toolCategoryManagement = new ToolCategoryManagement(storageAccess: _backgroundStorageToolCategory,
                                                                            source: _backgroundReadSource.ReadFilePath("ToolCategories"),
                                                                            categories: listOfCategories);
                    toolCategoryManagement.CreateItem();
                    listOfCategories.Clear();
                    userInputNumber = -1;
                    break;

                // Edit tool category
                case 2:
                    var editToolCategoryObject = new ToolCategoryManagement(storageAccess: _backgroundStorageToolCategory,
                                                                            source: _backgroundReadSource.ReadFilePath("ToolCategories"),
                                                                            categories: listOfCategories);
                    int toolCategoryId = -1;
                    var toolCategoryElement = string.Empty;
                    var toolCategoryContent = string.Empty;
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        Console.WriteLine("Enter the tool category Id you want to edit: ");
                        toolCategoryId = ConvertNumbers.ConvertInteger();
                        Console.WriteLine("You can edit the following properties");
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("1 Category Id");
                        Console.WriteLine("2 Category name");
                        Console.WriteLine("3 Description");
                        Console.WriteLine("4 Price per day");
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("Which property do you want to edit: ");

                        switch (ConvertNumbers.ConvertInteger())
                        {
                            case 1:
                                toolCategoryElement = "CategoryId";
                                break;
                            case 2:
                                toolCategoryElement = "Name";
                                break;
                            case 3:
                                toolCategoryElement = "Description";
                                break;
                            case 4:
                                toolCategoryElement = "PricePerDay";
                                break;
                            default:
                                break;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Please enter your change: ");


                        if (toolCategoryElement != null)
                        {
                            if (toolCategoryElement.Equals("CategoryId"))
                            {
                                toolCategoryContent = ConvertNumbers.ConvertInteger().ToString();
                            }
                            else if(toolCategoryElement.Equals("PricePerDay"))
                            {
                                toolCategoryContent = ConvertNumbers.ConvertDecimal().ToString();
                            }
                            else
                            {
                                toolCategoryContent = Console.ReadLine().Replace(',', '.');
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\r\nYou didn't dial a number!");
                        }

                        if (toolCategoryContent != null)
                        {
                            editToolCategoryObject.EditItem(toolCategoryId, toolCategoryElement, toolCategoryContent);
                        }

                        Console.WriteLine();
                        Console.WriteLine("Do you want to make changes on another tool category? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }
                    userInputNumber = -1;
                    break;

                // Delete tool category
                case 3:
                    // create object
                    var toolCategoryObject = new ToolCategoryManagement(storageAccess: _backgroundStorageToolCategory,
                                                                        source: _backgroundReadSource.ReadFilePath("ToolCategories"),
                                                                        categories: listOfCategories);
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        //Console.SetCursorPosition(0, 1);
                        Console.WriteLine("Please enter the Id of the tool category you want to delete: ");
                        var idToDelete = ConvertNumbers.ConvertInteger();
                        Console.WriteLine();
                        toolCategoryObject.DeleteItem(idToDelete);
                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete another tool category? Press key j or n: ");
                        userInputChar = Console.ReadLine().ToLower();
                    }
                    userInputNumber = -1;
                    break;
                // Display tool category
                case 4:
                    var toolCategoryObjectToShow = new ToolCategoryManagement(storageAccess: _backgroundStorageToolCategory,
                                                                        source: _backgroundReadSource.ReadFilePath("ToolCategories"));
                    userInputChar = "j";
                    while (userInputChar.Equals("j"))
                    {
                        Console.Clear();
                        toolCategoryObjectToShow.ShowItem();
                        Console.WriteLine("To exit, press any key and/or press the Enter key...");
                        userInputChar = Console.ReadLine().ToUpper();
                    }
                    userInputNumber = -1;
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

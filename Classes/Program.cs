using Werkzeugverleih.Screens;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Entree for application with their Main Menue and sub menues
    /// </summary>
    public class Program
    {
        public static int userInputNumber;
        public static int number;
        public static double money;
        public static string userInputChar = null;
        public static string backToMainMenue = null;
        public const int QUITT_MAIN_MENUE = 5;

        public static void Main(string[] args)
        {
            // Screens
            while (userInputNumber != QUITT_MAIN_MENUE)
            {
                // Screen Main Menue 
                Console.Clear();
                Console.WriteLine("***************************************");
                Console.WriteLine("Main Menue");
                Console.WriteLine("***************************************");
                Console.WriteLine("1 Customer Management");
                Console.WriteLine("2 Tool Management");
                Console.WriteLine("3 Tool Category Management");
                Console.WriteLine("4 Lending Management");
                Console.WriteLine("5 Quit Program");
                Console.WriteLine("***************************************");
                Console.Write("Please select menue item and hit enter: ");

                userInputNumber = ConvertNumbers.ConvertInteger();

                switch (userInputNumber)
                {
                    // Screen Customer Management
                    case 1:
                        while (userInputNumber != QUITT_MAIN_MENUE)
                        {
                            userInputNumber = ScreenCustomerManagement.ShowScreenCustomerManagement();
                        }
                        userInputNumber = -1;
                        break;

                    // Screen Tool Management
                    case 2:
                        while (userInputNumber != QUITT_MAIN_MENUE)
                        {
                            userInputNumber = ScreenToolManagement.ShowScreenToolManagement();
                        }
                        userInputNumber = -1;
                        break;

                    // Screen Tool Category Management
                    case 3:
                        while (userInputNumber != QUITT_MAIN_MENUE)
                        {
                            userInputNumber = ScreenToolCategoryManagement.ShowScreenToolCategoryManagement();
                        }
                        userInputNumber = -1;
                        break;
                    // Screen Lending Management
                    case 4:
                        while (userInputNumber != QUITT_MAIN_MENUE)
                        {
                            userInputNumber = ScreenLendingMnagement.ShowScreenLendingManagement();
                        }
                        userInputNumber = -1;
                        break;

                    // Quitt Main Menue
                    case 5:
                        userInputNumber = QUITT_MAIN_MENUE;
                        Console.Clear();
                        break;

                    // Default
                    default:
                        Console.WriteLine("Unfortunately your input cant be read.");
                        break;
                }
            }
        }
    }
}
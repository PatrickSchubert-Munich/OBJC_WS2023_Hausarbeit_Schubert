namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Converting inputs from user to different number formats
    /// </summary>
    public static class ConvertNumbers
    {
        private static string _text = "Your Input is not a number. Please try it again.";

        /// <summary>
        /// Convert input from user to integer
        /// </summary>
        /// <param name="optionalInput">user input</param>
        /// <returns>integer</returns>
        public static int ConvertInteger(string optionalInput = "")
        {
            bool isIntInputCorrect = false;
            string input = null;
            int intNumber = 0;

            while (isIntInputCorrect == false)
            {
                try
                {
                    if (optionalInput != "")
                    {
                        input = optionalInput;
                    }
                    else
                    {
                        input = Console.ReadLine();
                    }
                    intNumber = int.Parse(input);
                    isIntInputCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine(_text, input);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine();
                    Console.WriteLine(_text, input);
                }

            }
            Console.WriteLine();
            return intNumber;
        }

        /// <summary>
        /// Convert input from user to double
        /// </summary>
        /// <returns>double</returns>
        public static double ConvertDouble()
        {
            bool isDoubleInputCorrect = false;
            string entree = null;
            double doubleNumber = 0.0;

            while (isDoubleInputCorrect == false)
            {


                try
                {
                    entree = Console.ReadLine();
                    doubleNumber = double.Parse(entree);
                    isDoubleInputCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine(_text, entree);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine();
                    Console.WriteLine(_text, entree);
                }
            }
            Console.WriteLine();
            return doubleNumber;
        }

        /// <summary>
        /// Convert input from user to decimal
        /// </summary>
        /// <returns>decimal</returns>
        public static decimal ConvertDecimal()
        {
            bool isDoubleInputCorrect = false;
            string entree = null;
            decimal decimalNumber = 0;

            while (isDoubleInputCorrect == false)
            {


                try
                {
                    entree = Console.ReadLine();
                    decimalNumber = decimal.Parse(entree);
                    isDoubleInputCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine(_text, entree);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine();
                    Console.WriteLine(_text, entree);
                }
            }
            Console.WriteLine();
            return decimalNumber;
        }
    }
}

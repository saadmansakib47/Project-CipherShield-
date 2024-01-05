using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherShield_Beta
{
    public class PasswordGenerator
    {
        public static void CustomizedPasswordSubMenu()
        {
            Console.Clear();

            int length;
            while (true)
            {
                Console.WriteLine("Password Generator");
                Console.WriteLine("------------------\n ");
                Console.Write("Enter the desired password length: ");
                
                // Attempt to parse an integer from user input using the CustomTryParseInt method.
                // If parsing is successful, the parsed value is stored in the 'length' variable.
                if (CustomTryParseInt(Console.ReadLine(), out length))
                {    
                    //if method returns true
                    if (length <= 4)
                    {
                        ColorConsole.WriteError("Please enter a valid password length (4 < length < 30).");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {    // If the password length is valid, exit the current logic.
                        break;
                    }
                }
                else
                {   
                    //if method returns false
                    ColorConsole.WriteError("Invalid input. Please enter a positive integer as the password length.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            
            ColorConsole.WriteInfo("\nUse y/n to interact.");
            bool includeUppercase = ReadYesNoInput("Include uppercase letters (y/n): ");
            bool includeLowercase = ReadYesNoInput("Include lowercase letters (y/n): ");
            bool includeNumbers = ReadYesNoInput("Include numbers (y/n): ");
            bool includeSymbols = ReadYesNoInput("Include symbols (y/n): ");

            // Initialize an empty string to store excluded symbols.
            string excludedSymbols = string.Empty;          
            if (ReadYesNoInput("Exclude specific character(s) (y/n): "))
            {
                Console.Write("Enter the character(s) to exclude (for example, @$#): ");
                excludedSymbols = Console.ReadLine();
            }

            
            // Initialize a char variable to store the required symbol, defaulting to '\0'.
            char requiredSymbol = '\0';
            // If the user responds 'yes' (y), proceed to collect the required symbol.
            if (ReadYesNoInput("Require a specific symbol (y/n): "))
            {
                requiredSymbol = ReadRequiredSymbol();
            }

            string password = GenerateRandomPassword(length, includeUppercase, includeLowercase, includeNumbers, includeSymbols, excludedSymbols, requiredSymbol);
            ColorConsole.WriteLine("\nGenerated Password: " + CustomToString(password), ConsoleColor.Green);
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private static bool ReadYesNoInput(string prompt)
        {    
            /*
            *The method uses a while (true) loop, which means it will keep prompting the user 
            *until a valid input is received or until the user terminates the program.
            */
            
            while (true)
            {    
                // Display the prompt to the user.
                Console.Write(prompt);
                // Read a single key from the console without displaying it.
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                // Check if the pressed key is 'Y' or 'y' for Yes.
                if (key.Key == ConsoleKey.Y)
                {    
                    // Display "Yes" in green and return true.
                    ColorConsole.WriteLine("Yes", ConsoleColor.Green);
                    return true;
                }
                if (key.Key == ConsoleKey.N)
                {
                    ColorConsole.WriteLine("No", ConsoleColor.Red);
                    return false;
                }
                
                // If an invalid key is pressed, display an error message and continue the loop.
                ColorConsole.WriteLine("\nInvalid input. Please enter 'y' for yes or 'n' for no.", ConsoleColor.Red);
            }
        }

       // The method continues to prompt the user until a valid single-character input is received.
        private static char ReadRequiredSymbol()
        {
            char symbol;
            // The method uses a while loop to keep prompting the user until a valid input is received.
            while (true)
            {
                Console.Write("Enter the required symbol: ");
                
                // Try to parse the user's input into a single character
                if (char.TryParse(Console.ReadLine(), out symbol))
                {     
                    // If successful, return the parsed symbol.
                    return symbol;
                }

                // If the input is not a single character, display an error message and continue the loop.
                ColorConsole.WriteError("Invalid input. Please enter a single character.");
            }
        }

        private static string GenerateRandomPassword(int length, bool includeUppercase, bool includeLowercase, bool includeNumbers, bool includeSymbols, string excludedSymbols, char requiredSymbol)
        {
            string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            string numberChars = "0123456789";
            string symbolChars = "!@#$%^&*()_+";

            string allChars = "";

            if (includeUppercase)
                allChars += uppercaseChars;
            if (includeLowercase)
                allChars += lowercaseChars;
            if (includeNumbers)
                allChars += numberChars;
            if (includeSymbols)
                allChars += symbolChars;

            allChars = CustomReplaceSymbols(allChars, excludedSymbols);

            if (requiredSymbol != '\0' && !CustomContainsSymbol(allChars, requiredSymbol))
            {
                ColorConsole.WriteError($"Required symbol '{requiredSymbol}' is not in the list of allowed symbols.");
                return "";
            }

            byte[] randomBytes = CustomNextBytes(length);
            StringBuilder password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = randomBytes[i] % allChars.Length;
                password.Append(allChars[index]);
            }

            if (requiredSymbol != '\0')
            {
                int position = randomBytes[randomBytes.Length - 1] % length;
                password[position] = requiredSymbol;
            }

            return password.ToString();
        }

        private static byte[] CustomNextBytes(int length)
        {
            byte[] randomBytes = new byte[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                randomBytes[i] = (byte)random.Next(256);
            }
            return randomBytes;
        }

        //it is a method like int.TryParse but customed to check if the input has character/input is a neg number
        /*
        *The out int result parameter means that the result variable will be used to store the parsed integer value, 
        *and this value will be accessible outside the method after the method call.
        *The method is expected to assign a value to result during its execution.
        */
        public static bool CustomTryParseInt(string input, out int result)
        {
            result = 0;                   // Initialize the result to 0.
            bool isNegative = false;     // Flag to track if the number is negative.
            int currentIndex = 0;        // Index for iterating through the input string.
            int length = input.Length;   // Length of the input string.

            if (length == 0)
                return false;           // If the input string is empty, parsing is not possible.

            if (input[currentIndex] == '-')
            {
                isNegative = true;      // If the first character is '-', set the flag for negative.
                currentIndex++;
            }

            while (currentIndex < length)
            {
                char c = input[currentIndex];
                if (c >= '0' && c <= '9')
                {
                    int digit = c - '0';               // Convert character to integer.
                    result = result * 10 + digit;      // Build up the result by multiplying by 10 and adding the current digit.
                }
                else
                {
                    return false;          // If a non-digit character is encountered, parsing fails.              
                }
                currentIndex++;
            }

            if (isNegative)
            {
                result = -result;    // If the number is negative, negate the result.
            }

            return true;       // Parsing successful.
        }

        private static string CustomToString(string value)
        {
            return value;
        }

        private static string CustomReplaceSymbols(string source, string symbolsToExclude)
        {
            StringBuilder result = new StringBuilder(source);
            foreach (char symbol in symbolsToExclude)
            {
                result = new StringBuilder(CustomReplaceSymbol(result.ToString(), symbol, ' '));
            }
            return result.ToString();
        }

        private static string CustomReplaceSymbol(string source, char oldChar, char newChar)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in source)
            {
                if (c == oldChar)
                {
                    result.Append(newChar);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        private static bool CustomContainsSymbol(string source, char value)
        {
            foreach (char c in source)
            {
                if (c == value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

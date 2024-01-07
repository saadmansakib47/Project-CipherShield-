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
            // Define character sets for uppercase letters, lowercase letters, numbers, and symbols
            string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            string numberChars = "0123456789";
            string symbolChars = "!@#$%^&*()_+";

            // Initialize an empty string to store all allowed characters based on user preferences
            string allChars = "";

            // Add characters to the allowed set based on user preferences
            if (includeUppercase)
                allChars += uppercaseChars;
            if (includeLowercase)
                allChars += lowercaseChars;
            if (includeNumbers)
                allChars += numberChars;
            if (includeSymbols)
                allChars += symbolChars;
//             
            //Exclude specified symbols from the allowed set
            allChars = CustomReplaceSymbols(allChars, excludedSymbols);

            // Check if a required symbol is specified and not present in the allowed set
            if (requiredSymbol != '\0' && !CustomContainsSymbol(allChars, requiredSymbol))
            {    
                 // Print an error message and return an empty string if the required symbol is not allowed
                ColorConsole.WriteError($"Required symbol '{requiredSymbol}' is not in the list of allowed symbols.");
                return "";
            }


            // Generate an array of random bytes to be used as indices for selecting characters from the allowed set
            byte[] randomBytes = CustomNextBytes(length);
            // Initialize a StringBuilder to construct the final password
            StringBuilder password = new StringBuilder(length);

             // Iterate through the desired password length and select random characters from the allowed set
            for (int i = 0; i < length; i++)
            {
                int index = randomBytes[i] % allChars.Length;
                password.Append(allChars[index]);
            }

            // If a required symbol is specified, replace a randomly chosen character with the required symbol
            if (requiredSymbol != '\0')
            {
                int position = randomBytes[randomBytes.Length - 1] % length;
                password[position] = requiredSymbol;
            }

            // Return the generated password as a string
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


        // Replaces symbols specified in the 'symbolsToExclude' string with spaces in the 'source' string.
        private static string CustomReplaceSymbols(string source, string symbolsToExclude)
        {    
            // Create a StringBuilder to hold the result, initialized with the original source
            StringBuilder result = new StringBuilder(source);

            // Iterate through each symbol in 'symbolsToExclude' and replace it with a space in 'result'
            foreach (char symbol in symbolsToExclude)
            {
                result = new StringBuilder(CustomReplaceSymbol(result.ToString(), symbol, ' '));
            }

            // Convert the final result StringBuilder to a string and return
            return result.ToString();
        }

        // Replaces occurrences of a specified character with another character in the given string.
        private static string CustomReplaceSymbol(string source, char oldChar, char newChar)
        {    
            // Create a StringBuilder to store the modified string
            StringBuilder result = new StringBuilder();

            // Iterate through each character in the source string
            foreach (char c in source)
            {    
                // Check if the character is equal to the character to be replaced
                if (c == oldChar)
                {    
                    // If equal, append the replacement character to the result
                    result.Append(newChar);
                }
                else
                {    
                    // If not equal, append the original character to the result
                    result.Append(c);
                }
            }
            // Convert the StringBuilder to a string and return the result
            return result.ToString();
        }

        // Checks if the specified character is present in the given string.
        private static bool CustomContainsSymbol(string source, char value)
        {    
            // Iterate through each character in the source string
            foreach (char c in source)
            {    
                // Check if the current character is equal to the specified character
                if (c == value)
                {    
                    // If equal, return true indicating the character is found
                    return true;
                }
            }

            // If the loop completes without finding the character, return false
            return false;
        }
    }
}

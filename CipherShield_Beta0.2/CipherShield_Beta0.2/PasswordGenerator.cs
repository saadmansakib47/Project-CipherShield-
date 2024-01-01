using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherShield_Beta
{
    public class PasswordGenerator
    {
        public static void GenerateCustomizedPassword()
        {
            Console.Clear();

            int length;
            while (true)
            {
                Console.WriteLine("Password Generator");
                Console.WriteLine("------------------\n ");
                Console.Write("Enter the desired password length: ");
                if (CustomTryParseInt(Console.ReadLine(), out length))
                {
                    if (length <= 4)
                    {
                        ColorConsole.WriteError("Please enter a valid password length (4 < length < 30).");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
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

            string excludedSymbols = string.Empty;
            if (ReadYesNoInput("Exclude specific character(s) (y/n): "))
            {
                Console.Write("Enter the character(s) to exclude (for example, @$#): ");
                excludedSymbols = Console.ReadLine();
            }

            char requiredSymbol = '\0';
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
            while (true)
            {
                Console.Write(prompt);
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Y)
                {
                    ColorConsole.WriteLine("Yes", ConsoleColor.Green);
                    return true;
                }
                if (key.Key == ConsoleKey.N)
                {
                    ColorConsole.WriteLine("No", ConsoleColor.Red);
                    return false;
                }

                ColorConsole.WriteLine("\nInvalid input. Please enter 'y' for yes or 'n' for no.", ConsoleColor.Red);
            }
        }

        private static char ReadRequiredSymbol()
        {
            char symbol;
            while (true)
            {
                Console.Write("Enter the required symbol: ");
                if (char.TryParse(Console.ReadLine(), out symbol))
                {
                    return symbol;
                }
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

        public static bool CustomTryParseInt(string input, out int result)
        {
            result = 0;
            bool isNegative = false;
            int currentIndex = 0;
            int length = input.Length;

            if (length == 0)
                return false;

            if (input[currentIndex] == '-')
            {
                isNegative = true;
                currentIndex++;
            }

            while (currentIndex < length)
            {
                char c = input[currentIndex];
                if (c >= '0' && c <= '9')
                {
                    int digit = c - '0';
                    result = result * 10 + digit;
                }
                else
                {
                    return false;
                }
                currentIndex++;
            }

            if (isNegative)
            {
                result = -result;
            }

            return true;
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
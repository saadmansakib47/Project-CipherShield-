using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta0._2
{
    public class PasswordGenerator
    {
        public static void GenerateCustomizedPassword()
        {
            Console.Clear();
        
            int length;
            while (true)
            {
                Console.Write("Enter the desired password length: ");
                if (int.TryParse(Console.ReadLine(), out length))
                {
                    if (length <= 4)
                    {
                        Console.WriteLine("Password length can't be so small. Please enter a length greater than 4.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer as the password length.");
                }
            }
        
            bool includeUppercase = ReadYesNoInput("Include uppercase letters (y/n): ");
            bool includeLowercase = ReadYesNoInput("Include lowercase letters (y/n): ");
            bool includeNumbers = ReadYesNoInput("Include numbers (y/n): ");
            bool includeSymbols = ReadYesNoInput("Include symbols (y/n): ");
        
            string excludedSymbols = string.Empty;
            if (ReadYesNoInput("Exclude specific symbols (y/n): "))
            {
                Console.Write("Enter the symbols to exclude (e.g., @$#): ");
                excludedSymbols = Console.ReadLine();
            }
        
            char requiredSymbol = '\0';
            if (ReadYesNoInput("Require a specific symbol (y/n): "))
            {
                requiredSymbol = ReadRequiredSymbol();
            }
        
            string password = GenerateRandomPassword(length, includeUppercase, includeLowercase, includeNumbers, includeSymbols, excludedSymbols, requiredSymbol);
            Console.WriteLine("\nGenerated Password: ");
            ColorConsole.Write(password, ConsoleColor.Green);
            Console.WriteLine("\n\nPress any key to continue.");
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
                    Console.WriteLine('y');
                    return true;
                }
                if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine('n');
                    return false;
                }
        
                Console.WriteLine("\nInvalid input. Please enter 'y' for yes or 'n' for no.");
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
                Console.WriteLine("Invalid input. Please enter a single character.");
            }
        }
    }
}

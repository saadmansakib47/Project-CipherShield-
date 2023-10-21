using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta0._2
{
    public class PasswordGenerator
    {
        public void GenerateCustomizedPassword()
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
                    else if(length >= 15)
                    {
                        Console.WriteLine("Password length can't be so large. Please enter a length lesser than 15.");
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
            if (password.Length > 0)
            {
                Console.WriteLine("\nGenerated Password: ");
                ColorConsole.Write(password, ConsoleColor.Green);
                Console.WriteLine("\n\nPress any key to continue.");
            }
            Console.ReadKey();
        }
    
        public bool ReadYesNoInput(string prompt)
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
        
        public char ReadRequiredSymbol()
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
        
        public string GenerateRandomPassword(int length, bool includeUppercase, bool includeLowercase, bool includeNumbers, bool includeSymbols, string excludedSymbols, char requiredSymbol)
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
        
            // Remove excluded symbols from the list of symbols to use.
            foreach (char excludedSymbol in excludedSymbols)
            {
                allChars = allChars.Replace(excludedSymbol.ToString(), "");
            }
        
            // Ensure the required symbol is in the list of symbols to use.
            if (requiredSymbol != '\0' && !allChars.Contains(requiredSymbol.ToString()))
            {
                Console.WriteLine($"\nRequired symbol '{requiredSymbol}' is not in the list of allowed symbols.");
                return "";
            }
        
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);
        
                StringBuilder password = new StringBuilder(length);
        
                try
                {
                    for (int i = 0; i < length; i++)
                    {
                        int index = randomBytes[i] % allChars.Length;
                        password.Append(allChars[index]);
                    }
                }
                catch (Exception e)
                {
                    ColorConsole.WriteLine("\nInvalid input: At least one character type (uppercase, lowercase, numbers, or symbols) must be enabled.", ConsoleColor.Red);
                    Console.WriteLine("\nPress any key to continue.");            
                    return "";
                    
                }
        
                // If a required symbol is specified, replace one of the characters with it.
                if (requiredSymbol != '\0')
                {
                    int position = randomBytes[randomBytes.Length - 1] % length;
                    password[position] = requiredSymbol;
                }
        
                return password.ToString();
            }
        }
    }
}

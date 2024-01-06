using CipherShield_Beta0._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CipherShield_Beta
{
    public interface IPasswordToolbox
    {
        public static void PasswordToolbox()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Password Toolbox");
                Console.WriteLine("--------------------------\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine(" Password Creator");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" Password Strength Checker");
                ColorConsole.Write("[ 3 ]", ConsoleColor.Blue);
                Console.WriteLine(" Back to Main Menu");
                Console.WriteLine("");
                ColorConsole.Write("Select an option: ", ConsoleColor.White);
                string choice = Console.ReadLine(); // Handles user input. 


                switch (choice)
                {
                    case "1":
                        PasswordGenerator.CustomizedPasswordSubMenu(); 
                        break;
                    case "2":
                        StrengthChecker.PasswordStrengthReport();
                        break;
                    case "3":
                        Console.Clear();
                        return; // Return to the main menu.
                    default:
                        ColorConsole.Write("Invalid option. Please try again.", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }
            }
        }

    }
}


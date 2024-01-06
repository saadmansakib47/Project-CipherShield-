using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta0._2
{
    public interface IMonoAlphabetic
    {
        public static void MonoAlphabeticCipherSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose MonoAlphabetic Substitution Cipher");
                Console.WriteLine("-------------------------------\n\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine("Caesar Cipher");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine("Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine(); //Handles user input 

                switch (choice)
                {
                    case "1":
                        Caesar.CaesarCipherSubMenu(); 
                        break;
                    case "2":
                        Console.Clear();
                        return; // Return to the Previous Menu.
                    default:
                        ColorConsole.WriteError("Invalid option. \nPress any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}

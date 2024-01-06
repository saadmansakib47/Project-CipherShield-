using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta0._2
{
    public interface IHash
    {
        public static void HashingMenu()
        {
            Console.Clear();
            Console.WriteLine("Hashing");
            Console.WriteLine("------------\n");
            ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
            Console.WriteLine(" MD5 Hash");
            ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
            Console.WriteLine(" SHA-256 Hash");
            ColorConsole.Write("[ 3 ]", ConsoleColor.Blue);
            Console.WriteLine(" Back to Previous Menu");
            Console.Write("\nSelect an option: ");
            string choice = Console.ReadLine(); //Handles user input 

            switch (choice)
            {
                case "1":
                    MD5Converter.RunMD5();
                    break;
                case "2":
                    SHA256.RunSHA();
                    break;
                case "3":
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

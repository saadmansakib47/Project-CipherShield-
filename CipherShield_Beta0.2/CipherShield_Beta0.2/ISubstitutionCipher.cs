using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta
{
    public interface ISubstitutionCipher
    {
        public static void SubstitutionCipherSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose Substitution Cipher Type");
                Console.WriteLine("-------------------------------\n\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine(" MonoAlphabetic");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" PolyAlphabetic");
                ColorConsole.Write("[ 3 ]", ConsoleColor.Blue);
                Console.WriteLine(" PolyGraphic");
                ColorConsole.Write("[ 4 ]", ConsoleColor.Blue);
                Console.WriteLine(" Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
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

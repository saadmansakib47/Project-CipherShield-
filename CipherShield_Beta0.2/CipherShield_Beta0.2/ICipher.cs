using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta
{
    public interface ICipher
    {
        public static void CipherMenu()
        {
            while (true)
            {
                Console.WriteLine("Text Encryption/Decryption");
                Console.WriteLine("--------------------------\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine(" Cipher Text");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ICipherText.CipherTextSubMenu();
                        break;
                    case "2":
                        Console.Clear();
                        return; // Return to the main menu.
                    default:
                        ColorConsole.WriteError("\nInvalid option. \nPress any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

    }
}

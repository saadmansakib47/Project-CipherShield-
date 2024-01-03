using CipherShield_Beta0._2;
using System;

namespace CipherShield_Beta
{
    public interface IBlockCipher
    {
        public static void BlockCipherSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Block Cipher Submenu");
                Console.WriteLine("-------------------------------\n\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine(" AES (Advanced Encryption Standard) ");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" DES (Data Encryption Standard)");
                ColorConsole.Write("[ 3 ]", ConsoleColor.Blue);
                Console.WriteLine(" Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                DES des = new DES(); // Create an instance of the DES class

                switch (choice)
                {
                    case "1":
                        // Handle AES option 
                        break;
                    case "2":
                        // Handle DES option
                        DESSubMenu(des); // Pass the DES instance to the DESSubMenu method
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

        public static void DESSubMenu(DES des)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("DES Submenu");
                Console.WriteLine("-------------------------------\n\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine(" Encrypt Text ");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" Decrypt Text ");
                ColorConsole.Write("[ 3 ]", ConsoleColor.Blue);
                Console.WriteLine(" Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        des.EncryptText();
                        break;
                    case "2":
                        Console.Clear(); 
                        des.DecryptText();
                        break;
                    case "3":
                        Console.Clear();
                        return; // Return to the Block Cipher Submenu
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta0._2
{
    public static class Ceaser
    {
        public static void CeaserSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ceaser Cipher");
                Console.WriteLine("------------------\n\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Green);
                Console.WriteLine("Encrypt");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Green);
                Console.WriteLine("Decrypt");
                ColorConsole.Write("[ 3 ]", ConsoleColor.Green);
                Console.WriteLine("Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Enter plaintext to encrypt:");
                        string plaintext1 = Console.ReadLine();
                        Console.WriteLine("Enter the shift value for encryption :");
                        int shift = int.Parse(Console.ReadLine());
                        string Text1 = Encrypt(plaintext1, shift);
                        Console.WriteLine($"Encrypted Text: {Text1}");
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter ciphertext to decrypt:");
                        string ciphertext = Console.ReadLine();
                        Console.WriteLine("Enter the shift value for decryption:");
                        int keyDecrypt = int.Parse(Console.ReadLine());
                        string Text2 = Decrypt(ciphertext, keyDecrypt);
                        Console.WriteLine($"Decrypted Text: {Text2}");
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        return; // Return to the Previous Menu
                    default:
                        ColorConsole.WriteError("Invalid option. \nPress any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        static Alphabet alph = new Alphabet();

        
        public static string Encrypt(string sourcetext, int shift)
        {
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < sourcetext.Length; i++)
            {
                //searching for a character in the alphabet
                for (int j = 0; j < alph.lang.Length; j++)
                {
                    //if the symbol is found

                    if (sourcetext[i] == alph.lang[j])
                    {
                        code.Append(alph.lang[(j + shift) % alph.lang.Length]);
                        break;
                    }
                    //if the symbol is not found
                    else if (j == alph.lang.Length - 1)
                    {
                        code.Append(sourcetext[i]);
                    }
                }
            }

            return code.ToString();
        }

        public static string Decrypt(string sourcetext, int shift)
        {
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < sourcetext.Length; i++)
            {
                //searching for a character in the alphabet
                for (int j = 0; j < alph.lang.Length; j++)
                {
                    //if the symbol is found
                    if (sourcetext[i] == alph.lang[j])
                    {
                        code.Append(alph.lang[(j - shift + alph.lang.Length) % alph.lang.Length]);
                        break;
                    }
                    //if the symbol is not found
                    else if (j == alph.lang.Length - 1)
                    {
                        code.Append(sourcetext[i]);
                    }
                }
            }

            return code.ToString();
        }

    }
}

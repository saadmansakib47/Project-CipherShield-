using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield
{
    public static class Vignere
    {

        public static void VignereSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Vignere Cipher");
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
                        string plaintext2 = Console.ReadLine();
                        Console.WriteLine("Enter the key for encryption :");
                        string key1 = Console.ReadLine();
                        string Text3 = Encipher(plaintext2, key1);
                        Console.WriteLine($"Encrypted Text: {Text3}");
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter ciphertext to decrypt:");
                        string ciphertext1 = Console.ReadLine();
                        Console.WriteLine("Enter the key for decryption :");
                        string keyDecrypt1 = Console.ReadLine();
                        string Text4 = Decipher(ciphertext1, keyDecrypt1);
                        Console.WriteLine($"Decrypted Text: {Text4}");
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


        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        private static string Cipher(string input, string key, bool encipher)
        {
            for (int i = 0; i < key.Length; ++i)
                if (!char.IsLetter(key[i]))
                    return null; // Error

            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (char.IsLetter(input[i]))
                {
                    bool cIsUpper = char.IsUpper(input[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                    k = encipher ? k : -k;
                    char ch = (char)((Mod(((input[i] + k) - offset), 26)) + offset);
                    output += ch;
                }
                else
                {
                    output += input[i];
                    ++nonAlphaCharCount;
                }
            }

            return output;
        }

        public static string Encipher(string input, string key)
        {
            return Cipher(input, key, true);
        }

        public static string Decipher(string input, string key)
        {
            return Cipher(input, key, false);
        }


    }
}
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
                        string Text3 = Encipher(plaintext2,key1);
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
                        string Text4= Decipher(ciphertext1, keyDecrypt1);
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


        // Calculate the modulo operation and ensure the result is non-negative
        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        // Encrypts or decrypts a given input string using a substitution cipher and a provided key.
        // <param name="encipher">True for encryption, false for decryption.
        private static string Cipher(string input, string key, bool encipher)
        {    
            // Validate the key to ensure it contains only letters
            for (int i = 0; i < key.Length; ++i)
                if (!char.IsLetter(key[i]))
                    return null; // Error

            // Initialize an empty string to store the output
            string output = string.Empty;
            // Initialize a counter for non-alphabetic characters in the input
            int nonAlphaCharCount = 0;


            // Iterate through each character in the input string
            for (int i = 0; i < input.Length; ++i)
            {    
                // Check if the character is a letter
                if (char.IsLetter(input[i]))
                {    
                     // Determine if the letter is uppercase or lowercase
                    bool cIsUpper = char.IsUpper(input[i]);
                    //cIsUpper if true offset = 'A' ,if false offset = 'a'
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;

                    // Calculate the shift based on the key and whether it's encryption or decryption
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;

                    //If encipher is true, it keeps the value of k unchanged.
                    //If encipher is false, it negates the value of k.
                    k = encipher ? k : -k;

                    // Apply the substitution cipher and append the result to the output
                    char ch = (char)((Mod(((input[i] + k) - offset), 26)) + offset);
                    output += ch;
                }
                else
                {    
                    // If the character is not a letter, 
                    //append it to the output and update non-alphabetic character count
                    output += input[i];
                    ++nonAlphaCharCount;
                }
            }

            // Return the final encrypted or decrypted string
            return output;
        }

        public static string Encipher(string input, string key)
        {    
            // Delegates to the Cipher method with the encryption flag set to true
            return Cipher(input, key, true);
        }

        public static string Decipher(string input, string key)
        {    
            // Delegates to the Cipher method with the encryption flag set to false
            return Cipher(input, key, false);
        }


    }
}

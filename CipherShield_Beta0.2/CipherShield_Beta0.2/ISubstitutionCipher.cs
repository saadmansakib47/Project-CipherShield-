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
                Console.WriteLine(" Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":    

                                            //for mono alphabetic
                            try
                            {
                                Monoalphabetic monoalphabetic = new Monoalphabetic();
                
                                Console.WriteLine("Enter the plaintext to encrypt:");
                                string plaintext4 = Console.ReadLine();
                
                                Console.WriteLine("Enter the shift value for encryption:");
                                int shift = int.Parse(Console.ReadLine());
                
                                string encryptedText4 = monoalphabetic.Encrypt(plaintext4, shift);
                                Console.WriteLine($"Encrypted Text: {encryptedText4}");
                
                                string decryptedText4 = monoalphabetic.Decrypt(encryptedText4, shift);
                                Console.WriteLine($"Decrypted Text: {decryptedText4}");
                            }
                            catch ( Exception FormatException )
                            {
                                Console.WriteLine("please enter an 2 digit integer value for shift value ....");
                            }
                
                            Console.ReadLine(); // To keep the console window open

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

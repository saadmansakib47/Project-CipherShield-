using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta
{
    public interface ITranspositionCipher
    {
        public static void TranspositionCipherSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose Transposition Cipher");
                Console.WriteLine("-------------------------------\n\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine("Reil Fence Cipher ");
               
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                                    //for RailFence class

                                        Console.WriteLine("Rail Fence Cipher Encryption and Decryption");
                            
                                        // Get user input for the message
                                        Console.Write("Enter the message: ");
                                        string originalMessage = Console.ReadLine();
                            
                                        // Get user input for the key
                                        try
                                        {
                                            Console.Write("Enter the key: ");
                                            int key = int.Parse(Console.ReadLine());
                            
                                            // Encrypt the message
                                            string encryptedMessage = RailFence.EncryptRailFence(originalMessage, key);
                            
                                            Console.WriteLine($"Original Message: {originalMessage}");
                                            Console.WriteLine($"Encrypted Message: {encryptedMessage}");
                            
                                            // Decrypt the message
                                            string decryptedMessage = RailFence.DecryptRailFence(encryptedMessage, key);
                            
                                            Console.WriteLine($"Decrypted Message: {decryptedMessage}");
                                        }
                                        catch (Exception FormatException)
                                        {
                                            Console.WriteLine("please enter a 1 digit integer value for key ....");
                                        }
                                        Console.ReadLine(); // Pause to view the output
                            
                                                 
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


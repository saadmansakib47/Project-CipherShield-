using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield
{
    public class Hashing
    {
        public static void HashingMenu()
        {
            while (true)
            {
                Console.WriteLine("Hashing Menu");
                Console.WriteLine("1. MD5");
                Console.WriteLine("2. SHA-256");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();
            
                switch (choice)
                {
                    case "1":
                        
                        break;
                    case "2":
                        SHA256.RunSHA();
                        break;
            
                    // Add cases for other hash algorithms here.
                    case "3":
                        Console.Clear();
                        return; // Return to the main menu.
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        
    }
}

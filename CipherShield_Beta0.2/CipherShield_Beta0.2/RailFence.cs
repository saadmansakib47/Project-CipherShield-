using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta0._2
{

    public class RailFence
    {
        public static void RailFenceSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("RailFence Cipher");
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
                        string plaintext = Console.ReadLine();
                        Console.WriteLine("Enter the key for encryption (number of rails (an integer) :");
                        int keyEncrypt = int.Parse(Console.ReadLine());
                        string encryptedText = EncryptRailFence(plaintext, keyEncrypt);
                        Console.WriteLine($"Encrypted Text: {encryptedText}");
                        Console.WriteLine("\nPress any key to continue.");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter ciphertext to decrypt:");
                        string ciphertext = Console.ReadLine();
                        Console.WriteLine("Enter the key for decryption (number of rails (an integer) :");
                        int keyDecrypt = int.Parse(Console.ReadLine());
                        string decryptedText = DecryptRailFence(ciphertext, keyDecrypt);
                        Console.WriteLine($"Decrypted Text: {decryptedText}");
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

        // function to encrypt a message
        public static string EncryptRailFence(string text, int key)
        {

            // create the matrix to cipher plain text
            // key = rows, length(text) = columns
            char[,] rail = new char[key, text.Length];

            // filling the rail matrix to distinguish filled
            // spaces from blank ones
            for (int i = 0; i < key; i++)
                for (int j = 0; j < text.Length; j++)
                    rail[i, j] = '\n';

            bool dirDown = false;
            int row = 0, col = 0;

            for (int i = 0; i < text.Length; i++)
            {
                // check the direction of flow
                // reverse the direction if we've just
                // filled the top or bottom rail
                if (row == 0 || row == key - 1)
                    dirDown = !dirDown;

                // fill the corresponding alphabet
                rail[row, col++] = text[i];

                // find the next row using direction flag
                if (dirDown)
                    row++;
                else
                    row--;
            }

            // now we can construct the cipher using the rail
            // matrix
            string result = "";
            for (int i = 0; i < key; i++)
                for (int j = 0; j < text.Length; j++)
                    if (rail[i, j] != '\n')
                        result += rail[i, j];

            return result;
        }

        // This function receives cipher-text and key
        // and returns the original text after decryption
        public static string DecryptRailFence(string cipher, int key)
        {
            // create the matrix to cipher plain text
            // key = rows, length(text) = columns
            // create the matrix to cipher plain text
            // key = rows , length(text) = columns
            char[,] rail = new char[key, cipher.Length];

            // filling the rail matrix to distinguish filled
            // spaces from blank ones
            for (int i = 0; i < key; i++)
                for (int j = 0; j < cipher.Length; j++)
                    rail[i, j] = '\n';

            // to find the direction
            bool dirDown = true;
            int row = 0, col = 0;

            // mark the places with '*'
            for (int i = 0; i < cipher.Length; i++)
            {
                // check the direction of flow
                if (row == 0)
                    dirDown = true;
                if (row == key - 1)
                    dirDown = false;

                // place the marker
                rail[row, col++] = '*';

                // find the next row using direction flag
                if (dirDown)
                    row++;
                else
                    row--;
            }

            // now we can construct the fill using the rail matrix
            int index = 0;
            for (int i = 0; i < key; i++)
                for (int j = 0; j < cipher.Length; j++)
                    if (rail[i, j] == '*' && index < cipher.Length)
                        rail[i, j] = cipher[index++];

            // create the result string
            string result = "";
            row = 0;
            col = 0;

            // iterate through the rail matrix
            for (int i = 0; i < cipher.Length; i++)
            {
                // check the direction of flow
                if (row == 0)
                    dirDown = true;
                if (row == key - 1)
                    dirDown = false;

                // place the marker
                if (rail[row, col] != '*')
                    result += rail[row, col++];

                // find the next row using direction flag
                if (dirDown)
                    row++;
                else
                    row--;
            }
            return result;
        }

    }

}


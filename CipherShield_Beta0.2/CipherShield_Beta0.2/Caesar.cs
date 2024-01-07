using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Class representing the Caesar Cipher functionality
public class Caesar
{
    // Instance of the Alphabet class to access the alphabet characters
    Alphabet alph = new Alphabet();

    // Method to display and handle the Caesar Cipher sub-menu
    public static void CaesarCipherSubMenu()
    {
        // Create an instance of the Caesar class
        Caesar monoalphabetic = new Caesar();

        // Main loop for the Caesar Cipher sub-menu
        while (true)
        {
            // Display the menu options
            Console.Clear();
            Console.WriteLine("Caesar Cipher");
            Console.WriteLine("-------------\n\n");
            ColorConsole.Write("[ 1 ]", ConsoleColor.Green);
            Console.WriteLine(" Encrypt");
            ColorConsole.Write("[ 2 ]", ConsoleColor.Green);
            Console.WriteLine(" Decrypt");
            ColorConsole.Write("[ 3 ]", ConsoleColor.Green);
            Console.WriteLine(" Frequency Analysis");
            ColorConsole.Write("[ 4 ]", ConsoleColor.Green);
            Console.WriteLine("Back to Previous Menu");
            Console.Write("\nSelect an option: ");

            // Read the user's choice
            string choice = Console.ReadLine();

            // Switch based on user's choice
            switch (choice)
            {
                case "1":
                    // Encryption option
                    Console.Clear();
                    Console.WriteLine("Enter plaintext to encrypt:");
                    string plaintext = Console.ReadLine();
                    Console.WriteLine("Enter the shift value for encryption:");
                    int shiftEncrypt = int.Parse(Console.ReadLine());
                    string encryptedText = monoalphabetic.Encrypt(plaintext, shiftEncrypt);
                    Console.WriteLine($"Encrypted Text: {encryptedText}");
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                case "2":
                    // Decryption option
                    Console.Clear();
                    Console.WriteLine("Enter ciphertext to decrypt:");
                    string ciphertext = Console.ReadLine();
                    Console.WriteLine("Enter the shift value for decryption:");
                    int shiftDecrypt = int.Parse(Console.ReadLine());
                    string decryptedText = monoalphabetic.Decrypt(ciphertext, shiftDecrypt);
                    Console.WriteLine($"Decrypted Text: {decryptedText}");
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                case "3":
                    // Frequency analysis option
                    monoalphabetic.DecryptWithoutShiftValue();
                    break;
                case "4":
                    // Return to the Previous Menu option
                    Console.Clear();
                    return;
                default:
                    // Invalid option
                    ColorConsole.WriteError("Invalid option. \nPress any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
    }

    // Method to encrypt a given plaintext using the Caesar Cipher
    public string Encrypt(string sourcetext, int shift)
    {
        StringBuilder code = new StringBuilder();

        // Loop through each character in the plaintext
        foreach (char character in sourcetext)
        {
            if (char.IsLetter(character))
            {
                // Encrypt letters using the specified shift value
                char encryptedChar = EncryptChar(character, shift);
                code.Append(encryptedChar);
            }
            else
            {
                // Preserve spaces and non-letter characters
                code.Append(character);
            }
        }

        // Return the encrypted text
        return code.ToString();
    }

    // Method to encrypt a single character using the Caesar Cipher
    private char EncryptChar(char sourceChar, int shift)
    {
        // Loop through the alphabet characters
        for (int j = 0; j < alph.lang.Length; j++)
        {
            // If the symbol is found
            if (char.ToUpper(sourceChar) == alph.lang[j])
            {
                // Return the encrypted character
                return alph.lang[(j + shift) % alph.lang.Length];
            }
        }

        // If the symbol is not found (non-letter character), return as-is
        return sourceChar;
    }

    // Method to decrypt a given ciphertext using the Caesar Cipher
    public string Decrypt(string sourcetext, int shift)
    {
        StringBuilder code = new StringBuilder();

        // Loop through each character in the ciphertext
        foreach (char character in sourcetext)
        {
            if (char.IsLetter(character))
            {
                // Decrypt letters using the specified shift value
                char decryptedChar = DecryptChar(character, shift);
                code.Append(decryptedChar);
            }
            else
            {
                // Preserve spaces and non-letter characters
                code.Append(character);
            }
        }

        // Return the decrypted text
        return code.ToString();
    }

    // Method to decrypt a single character using the Caesar Cipher
    public char DecryptChar(char sourceChar, int shift)
    {
        // Loop through the alphabet characters
        for (int j = 0; j < alph.lang.Length; j++)
        {
            // If the symbol is found
            if (char.ToUpper(sourceChar) == alph.lang[j])
            {
                // Return the decrypted character
                return alph.lang[(j - shift + alph.lang.Length) % alph.lang.Length];
            }
        }

        // If the symbol is not found (non-letter character), return as-is
        return sourceChar;
    }

    // Method to decrypt a given ciphertext without knowing the shift value
    private string DecryptWithSpaces(string ciphertext, int shift)
    {
        StringBuilder code = new StringBuilder();

        // Loop through each character in the ciphertext
        foreach (char character in ciphertext)
        {
            if (char.IsLetter(character))
            {
                // Decrypt letters using the determined shift value
                char decryptedChar = DecryptChar(character, shift);
                code.Append(decryptedChar);
            }
            else
            {
                // Preserve spaces and non-letter characters
                code.Append(character);
            }
        }

        // Return the decrypted text
        return code.ToString();
    }

    // Method to perform frequency analysis and decrypt a given ciphertext
    public void DecryptWithoutShiftValue()
    {
        Console.Clear();
        Console.WriteLine("Enter ciphertext to decrypt without shift value:");
        string ciphertext = Console.ReadLine();

        // Analyze frequency of letters in the ciphertext
        Dictionary<char, int> letterFrequency = new Dictionary<char, int>();
        foreach (char letter in ciphertext)
        {
            if (char.IsLetter(letter))
            {
                char upperCaseLetter = char.ToUpper(letter);
                if (letterFrequency.ContainsKey(upperCaseLetter))
                {
                    letterFrequency[upperCaseLetter]++;
                }
                else
                {
                    letterFrequency[upperCaseLetter] = 1;
                }
            }
        }

        // Sort the letter frequencies in descending order
        var sortedFrequencies = letterFrequency.OrderByDescending(pair => pair.Value);

        // Probable letters in English (a, r, i, o, t, n, s, l)
        char[] probableLetters = { 'A', 'R', 'I', 'O', 'T', 'N', 'S', 'L' };

        // Try to decrypt using each probable letter
        foreach (char probableLetter in probableLetters)
        {
            // Determine the shift value based on the probable letter
            int shiftValue = (probableLetter - sortedFrequencies.First().Key + 26) % 26;

            // Decrypt the entire ciphertext (including spaces) using the determined shift value
            string decryptedText = DecryptWithSpaces(ciphertext, shiftValue);

            // Display the decrypted text with the estimated shift value and probable letter
            Console.WriteLine($"Decrypted Text (estimated shift value {shiftValue} with probable letter '{probableLetter}'): {decryptedText}");
        }

        // Prompt the user to continue
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
    }
}

/* the extra alphabet class
// Class representing the English alphabet
public class Alphabet
{
    // Array containing the English alphabet characters
    public char[] lang { get; }

    // Constructor to initialize the alphabet array
    public Alphabet()
    {
        // Assuming a standard English alphabet
        lang = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    }
*/

 class Alphabet
    {
        public char[] lang = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"#$%^&*()+=-_'?.,|/`~№:;@[]{}\\".ToCharArray();
    }
}

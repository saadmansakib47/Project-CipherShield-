using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CipherShield_Beta0._2
{
    public class RSACrypt
    {
        // Fields to store the ciphertext and prime numbers
        private int[] cypherText;
    
        private const int MaxValue = 25000;
        private readonly bool[] isNonPrime = new bool[MaxValue + 1];
        private readonly List<int> primes = new List<int>();
    
        // Fields to store the keys
        private int n;
        private int e;
        private int d;
    
        // Constructor to initialize prime numbers
        internal RSACrypt()
        {
            GeneratePrimes();
        }
    
        // Main method to run RSA cryptography operations
        public static void RunRSA()
        {
            // Create an instance of RSACrypt
            RSACrypt rsa = new RSACrypt();
            // Generate keys at the beginning
            rsa.GenerateKey(); 
    
            // Main menu loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine("RSA Cryptography Menu:");
                Console.WriteLine("-------------------------------\n\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine(" Encrypt Text ");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" Decrypt Text ");
                ColorConsole.Write("[ 3 ]", ConsoleColor.Blue);
                Console.WriteLine(" Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();
    
                // Switch statement for user options
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        rsa.EncryptOption();
                        break;
                    case "2":
                        Console.Clear();
                        rsa.DecryptWithProvidedKeysOption();
                        break;
                    case "3":
                        Console.Clear();
                        return;
                    default:
                        ColorConsole.WriteError("Invalid option. \nPress any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    
        // Method for the encryption option
        private void EncryptOption()
        {
            // Generate keys before encryption
            GenerateKey();
    
            //Take input from the user
            Console.WriteLine("Enter your text:");
            string message = Console.ReadLine();
            Console.WriteLine("\nOriginal Text: \"" + message + "\"");
    
            // Encrypt the message
            cypherText = Encrypt(message);
    
            // Display the ciphertext
            Console.Write("Cypher Text: ");
            var isFirstLetter = true;
    
            foreach (var place in cypherText)
            {
                if (isFirstLetter)
                {
                    isFirstLetter = false;
                    Console.Write(place);
                    continue;
                }
                // Print a space and a comma before every letter except the first letter
                Console.Write(", " + place);
            }
    
            Console.WriteLine();
            // Display the public and private keys
            Console.WriteLine("Public Key (e, n): (" + e + ", " + n + ")");
            Console.WriteLine("Private Key (d, n): (" + d + ", " + n + ")");
    
            Console.WriteLine("Press Any Key to Continue.");
            Console.ReadLine();
            // Go back to the main menu
            return;
        }
    
        // Method for decryption with provided keys option
        private void DecryptWithProvidedKeysOption()
        {
            // Take input
            Console.WriteLine("Enter public key (e):");
            int eKey = int.Parse(Console.ReadLine());
    
            Console.WriteLine("Enter private key (d):");
            int dKey = int.Parse(Console.ReadLine());
    
            Console.WriteLine("Enter modulus (n):");
            int nKey = int.Parse(Console.ReadLine());
    
            Console.WriteLine("Enter ciphertext (comma-separated):");
    
            // Convert the cyphertext into an array on integers
            int[] providedCypherText = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
    
            // Decrypt with provided keys
            var decryptedTextWithProvidedKeys = DecryptWithKeys(eKey, dKey, nKey, providedCypherText);
            Console.WriteLine("\nDecrypted Text with Provided Keys: \"" + decryptedTextWithProvidedKeys + "\"");
    
            Console.WriteLine("Press Any Key to Continue.");
            Console.ReadLine();
            // Go back to the main menu
            return; 
        }
    
        // Method to generate prime numbers up to MaxValue
        private void GeneratePrimes()
        {
            // Loop through numbers starting from 2 up to MaxValue
            for (var i = 2; i <= MaxValue; i++)
            {
                // Check if the current number (i) is marked as non-prime
                if (!isNonPrime[i])
                {
                    // If i is not marked as non-prime, it is a prime number
                    // Add it to the list of primes
                    primes.Add(i);
    
                    // Mark multiples of the current prime as non-prime starting from i squared
                    for (var j = i * i; j <= MaxValue; j += i)
                    {
                        // Mark j as non-prime
                        isNonPrime[j] = true;
                    }
                }
            }
        }
    
    
        // Method to generate public and private keys
        private void GenerateKey()
        {
            // Determine the range for selecting prime numbers
            var end = primes.Count - 1;
            var start = end / 4;
    
            // Initialize a random number generator
            var random = new Random();
    
            // Select two distinct prime numbers from the list within the specified range
            var primeOne = primes[random.Next(start, end)];
            var primeTwo = primes[random.Next(start, end)];
    
            // Ensure primeTwo is different from primeOne
            while (primeTwo == primeOne)
            {
                primeTwo = primes[random.Next(start, end)];
            }
    
            // Calculate the modulus (n) and Euler's totient function (phi)
            n = primeOne * primeTwo;
            var phi = (primeOne - 1) * (primeTwo - 1);
    
            // Select a public exponent (e) such that it is not equal to primeOne or primeTwo
            do
            {
                do
                {
                    e = primes[random.Next(start, end)];
                } while (e == primeOne || e == primeTwo);
    
              // Find a private exponent (d) that satisfies the modular inverse condition
            } while (!FindPrivateKey(phi));
    
            // Display the generated public and private keys
            Console.WriteLine("Public Key: (e, n) = (" + e + ", " + n + ")");
            Console.WriteLine("Private Key: (d, n) = (" + d + ", " + n + ")");
        }
    
        // Method to find a private exponent (d) satisfying
        // the modular inverse condition (e * d) % phi = 1
        private bool FindPrivateKey(int phi)
        {
            // Iterate over possible values for the private exponent in descending order
            for (var i = phi - 1; i > 1; i--)
            {
                // Calculate e * current private exponent candidate (i)
                var mul = BigInteger.Multiply(e, i);
    
                // Calculate (e * d) % phi
                var result = BigInteger.Remainder(mul, phi);
    
                // Check if result = 1, indicating a modular inverse
                if (result.Equals(1))
                {
                    // If found, assign the current private exponent to d and return true
                    d = i;
                    return true;
                }
            }
    
            // If no suitable private exponent is found, return false
            return false;
        }
    
        // Method for encryption
        internal int[] Encrypt(string message)
        {
            // Convert the input message into a char array
            var charArray = message.ToCharArray();
    
            // Create an array to store the encrypted values
            var array = new int[charArray.Length];
    
            // Loop through each character
            for (var i = 0; i < array.Length; i++)
            {
                // Encrypt the current character using ( charArray[i] ^ e ) % n
                array[i] = (int)BigInteger.ModPow(charArray[i], e, n);
            }
    
            // Return the array containing the encrypted values
            return array;
        }
    
        // Method for decryption with provided keys
        internal string DecryptWithKeys(int e, int d, int n, int[] cypherText)
        {
            // Create an array to store the decrypted characters
            var array = new char[cypherText.Length];
    
            // Loop through each ciphertext value in the array
            for (var i = 0; i < array.Length; i++)
            {
                // Ensure that the ciphertext value is less than n before decryption
                if (cypherText[i] >= n)
                {
                    throw new ArgumentException("Invalid ciphertext. Ensure ciphertext values are less than n.");
                }
    
                // Decrypt the current ciphertext value using ( cyphertext[i] ^ d ) % n
                array[i] = (char)BigInteger.ModPow(cypherText[i], d, n);
            }
    
            // Return the decrypted characters as a string
            return new string(array);
        }
    
    }
}

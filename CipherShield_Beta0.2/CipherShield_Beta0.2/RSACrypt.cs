using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CipherShield_Beta0._2
{
    public class RSACrypt
    {
        private int[] cypherText; 

        private const int MaxValue = 25000;
        private readonly bool[] isPrime = new bool[MaxValue + 1];
        private readonly List<int> primes = new List<int>();

        private int n;
        private int e;
        private int d;

        internal RSACrypt()
        {
            GeneratePrimes();
        }

        public static void RunRSA()
        {
            RSACrypt rsa = new RSACrypt();
            rsa.GenerateKey(); // Generate keys at the beginning

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

        private void EncryptOption()
        {
            // Generate keys before encryption
            GenerateKey();

            Console.WriteLine("Enter your text:");
            string message = Console.ReadLine();
            Console.WriteLine("\nOriginal Text: \"" + message + "\"");

            // Encrypt the message
            cypherText = Encrypt(message);

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
                Console.Write(", " + place);
            }

            Console.WriteLine();
            Console.WriteLine("Public Key (e, n): (" + e + ", " + n + ")");
            Console.WriteLine("Private Key (d, n): (" + d + ", " + n + ")");

            Console.WriteLine("Press Any Key to Continue.");
            Console.ReadLine();
            return ; // Go back to the main menu
        }



        private void DecryptWithProvidedKeysOption()
        {
            Console.WriteLine("Enter public key (e):");
            int eKey = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter private key (d):");
            int dKey = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter modulus (n):");
            int nKey = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter ciphertext (comma-separated):");
            int[] providedCypherText = Console.ReadLine().Split(',').Select(int.Parse).ToArray();

            // Decrypt with provided keys
            var decryptedTextWithProvidedKeys = DecryptWithKeys(eKey, dKey, nKey, providedCypherText);
            Console.WriteLine("\nDecrypted Text with Provided Keys: \"" + decryptedTextWithProvidedKeys + "\"");

            Console.WriteLine("Press Any Key to Continue."); 
            Console.ReadLine();
            return ; // Go back to the main menu
        }

        private void GeneratePrimes()
        {
            for (var i = 2; i <= MaxValue; i++)
            {
                if (!isPrime[i])
                {
                    primes.Add(i);
                    for (var j = i * i; j <= MaxValue; j += i)
                    {
                        isPrime[j] = true;
                    }
                }
            }
        }

        private void GenerateKey()
        {
            var end = primes.Count - 1;
            var start = end / 4;
            var random = new Random();
            var primeOne = primes[random.Next(start, end)];
            var primeTwo = primes[random.Next(start, end)];

            while (primeTwo == primeOne)
            {
                primeTwo = primes[random.Next(start, end)];
            }

            n = primeOne * primeTwo;
            var phi = (primeOne - 1) * (primeTwo - 1);

            do
            {
                do
                {
                    e = primes[random.Next(start, end)];
                } while (e == primeOne || e == primeTwo);
            } while (!FindPrivateKey(phi));

            Console.WriteLine("Public Key: (e, n) = (" + e + ", " + n + ")");
            Console.WriteLine("Private Key: (d, n) = (" + d + ", " + n + ")");
        }

        private bool FindPrivateKey(int phi)
        {
            for (var i = phi - 1; i > 1; i--)
            {
                var mul = BigInteger.Multiply(e, i);
                var result = BigInteger.Remainder(mul, phi);
                if (result.Equals(1))
                {
                    d = i;
                    return true;
                }
            }
            return false;
        }

        internal int[] Encrypt(string message)
        {
            var charArray = message.ToCharArray();
            var array = new int[charArray.Length];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = (int)BigInteger.ModPow(charArray[i], e, n);
            }
            return array;
        }

        internal string Decrypt(int[] cyphertext)
        {
            var array = new char[cyphertext.Length];

            for (var i = 0; i < array.Length; i++)
            {
                // Ensure that the ciphertext value is less than n before decryption
                if (cyphertext[i] >= n)
                {
                    throw new ArgumentException("Invalid ciphertext. Ensure ciphertext values are less than n.");
                }

                array[i] = (char)BigInteger.ModPow(cyphertext[i], d, n);
            }
            return new string(array);
        }


        internal string DecryptWithKeys(int e, int d, int n, int[] cypherText)
        {
            var array = new char[cypherText.Length];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = (char)BigInteger.ModPow(cypherText[i], d, n);
            }
            return new string(array);
        }
    }
}

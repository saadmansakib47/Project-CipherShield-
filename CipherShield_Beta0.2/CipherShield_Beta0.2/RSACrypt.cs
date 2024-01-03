using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CipherShield_Beta0._2
{
    public class RSACrypt
    {
        private const int MaxValue = 25000;
        private readonly bool[] isPrime = new bool[MaxValue + 1];
        private readonly List<int> primes = new List<int>();

        private int n;
        private int e;
        private int d;

        internal RSACrypt()
        {
            GeneratePrimes();
            GenerateKey();
        }

        public static void RunRSA() 
        {
            Console.WriteLine("Enter your text:");
            string message = Console.ReadLine();
            Console.WriteLine("\nOriginal Text: \"" + message + "\"\n");

            // Create an instance of RSACrypt
            var rsa = new RSACrypt();

            // Encrypt the message
            var cypherText = rsa.Encrypt(message);

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

            Console.WriteLine("\nPress Y/y to decrypt the cyphertext.");

            string a = Console.ReadLine();
            if (a == "y" || a == "Y")
            {
                // Decrypt and display the decrypted text
                var decryptedText = rsa.Decrypt(cypherText);
                Console.WriteLine("\nDecrypted Text: \"" + decryptedText + "\"");
            }

            Console.ReadLine();
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
                array[i] = (char)BigInteger.ModPow(cyphertext[i], d, n);
            }
            return new string(array);
        }
    }
}

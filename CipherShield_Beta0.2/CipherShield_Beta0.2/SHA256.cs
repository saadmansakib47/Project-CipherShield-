using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherShield_Beta0.2
{
    public class SHA256
    {
        public static void RunSHA()
        {
            // Clear the Previous Screen 
            Console.Clear();

            // Get input from the user
            Console.Write("Enter a message: ");
            string message = Console.ReadLine();

            // Calculate the SHA-256 hash
            string hash = Result(message);

            // Display the original message and the hash
            Console.WriteLine($"\nOriginal Message: {message}");
            Console.WriteLine($"SHA-256 Hash: {hash}");

            // Wait for user input before exiting
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        // Constants representing initial hash values for SHA-256
        // first 32 bits of the fractional part of the cube roots of the first 64 primes (2-311)
        private static uint[] keys = {
            0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
            0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
            0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
            0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
            0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
            0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
            0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
            0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2 };

        // Method to obtain the SHA-256 hash of a given message
        public static string Result(string message)
        {
            // Pad the message and then calculate the hash
            byte[] paddedMessage = Padding(message);
            string hash = Hashing(paddedMessage);

            return hash;
        }

        // Method to pad the original message as per SHA-256 requirements
        public static byte[] Padding(string originalMessage)
        {
            // Convert the message to bytes
            byte[] messageBytes = Encoding.ASCII.GetBytes(originalMessage);

            // Calculate the number of bits to pad
            int bitsToPad = 0;
            // the number of bits will be a multiple of 512
            while (((messageBytes.Length * 8) + 8 + 64 + bitsToPad) % 512 != 0)
            {
                // add additional zeroes to it as long as it isnt a multiple of 512 ( 1 zero = 8 bit )
                bitsToPad += 8;
            }

            // Create a new byte array for the padded message
            byte[] paddedMessage = new byte[((messageBytes.Length * 8) + 8 + 64 + bitsToPad) / 8];

            // Add the bit 1 at the index starting from original message length
            paddedMessage[messageBytes.Length] = 0x80; // binary = 10000000 ( 8 bits = 1 byte )

            // copy the original message to the paddedmessage at index 0
            messageBytes.CopyTo(paddedMessage, 0);

            // Fill the rest of the padded message with zeros
            for (int i = messageBytes.Length + 1; i < paddedMessage.Length - 8; i++)
            {
                paddedMessage[i] = 0x00; // adding zeroes
            }

            // Add the length of the original message to the end of the padded message
            byte[] messageLength = BitConverter.GetBytes(Convert.ToUInt64(messageBytes.Length * 8)).Reverse().ToArray();
            messageLength.CopyTo(paddedMessage, paddedMessage.Length - 8);

            return paddedMessage;
        }

        // Method to perform the hashing of the padded message
        public static string Hashing(byte[] paddedMessage)
        {
            // Divide the padded message into 512-bit chunks
            int numOfChunks = paddedMessage.Length * 8 / 512;

            // Create an array of byte arrays to hold these chunks
            byte[][] chunks = new byte[numOfChunks][];

            // Initial hash values
            // first 32 bits of the fractional part of the square roots of the first 8 primes (2-19)
            uint[] initHash = { 0x6a09e667,
                                0xbb67ae85,
                                0x3c6ef372,
                                0xa54ff53a,
                                0x510e527f,
                                0x9b05688c,
                                0x1f83d9ab,
                                0x5be0cd19 };

            // Divide the padded message into chunks
            for (int i = 0; i < numOfChunks; i++)
            {
                //a new byte array is created for each chunk
                chunks[i] = new byte[64];

                // this loop copies each byte from the padded message to the current chunk
                for (int j = 0; j < 64; j++)
                {
                    chunks[i][j] = paddedMessage[(i * 512 / 8) + j]; // divide by 8 to convert bits into bytes
                }
            }

            // Process each chunk
            for (int k = 0; k < numOfChunks; k++)
            {
                // Create array for the message schedule
                uint[] W = new uint[64];

                // Extract 16 words from the current chunk
                for (int m = 0; m < 16; m++)
                {
                    byte[] x = SubArray(chunks[k], m * 4, 4);

                    // Reverse the byte order (little-endian to big-endian)
                    Array.Reverse(x);

                    // convert to UInt and store it in the message schedule
                    W[m] = BitConverter.ToUInt32(x, 0);
                }

                // Extend the message schedule to 64 words
                for (int n = 16; n < 64; n++)
                {
                    // formula for extending σ1(n-2)+(n-7)+σ0(n-15)+(n-16)
                    W[n] = W[n - 16] + sigma0(W[n - 15]) + W[n - 7] + sigma1(W[n - 2]);
                }

                // Initialize hash values for this chunk
                uint a = initHash[0], 
                     b = initHash[1], 
                     c = initHash[2], 
                     d = initHash[3], 
                     e = initHash[4],
                     f = initHash[5],
                     g = initHash[6],
                     h = initHash[7];

                // Main SHA-256 compression loop
                for (int p = 0; p < 64; p++) 
                {
                    // Calculate temporary values using the formulas
                    // temp1 = h + Σ1(e) + Choice(e , f, g) + Keys[0] + Word[0]
                    // temp2 = Σ0(a) + Maj(a, b, c)
                    uint temp1 = h + Sigma1(e) + Choice(e, f, g) + keys[p] + W[p];
                    uint temp2 = Sigma0(a) + Majority(a, b, c);

                    // Update the working variables
                    h = g;
                    g = f;
                    f = e;
                    e = d + temp1;
                    d = c;
                    c = b;
                    b = a;
                    a = temp1 + temp2;
                }

                // Add the initial hash values with new values from the chunk
                initHash[0] = initHash[0] + a;
                initHash[1] = initHash[1] + b;
                initHash[2] = initHash[2] + c;
                initHash[3] = initHash[3] + d;
                initHash[4] = initHash[4] + e;
                initHash[5] = initHash[5] + f;
                initHash[6] = initHash[6] + g;
                initHash[7] = initHash[7] + h;
            }

            // converting to hexadecimal strings with at least 8 characters (X8)
            string H0 = initHash[0].ToString("X8");
            string H1 = initHash[1].ToString("X8");
            string H2 = initHash[2].ToString("X8");
            string H3 = initHash[3].ToString("X8");
            string H4 = initHash[4].ToString("X8");
            string H5 = initHash[5].ToString("X8");
            string H6 = initHash[6].ToString("X8");
            string H7 = initHash[7].ToString("X8");

            // Concatenate the hexadecimal strings
            return H0 + H1 + H2 + H3 + H4 + H5 + H6 + H7;
        }

        // Helper method to perform a right rotation on a 32-bit value (1 word)
        public static uint RightRotate(uint val, int numOfBits)
        {
            return (val >> numOfBits) | (val << (32 - numOfBits));
        }

        // Helper method to perform a right shift on a 32-bit value
        public static uint RightShift(uint val, int numOfBits)
        {
            return (val >> numOfBits);
        }

        // Helper method to calculate the majority of three 32-bit values
        public static uint Majority(uint x, uint y, uint z)
        {
            return (x & y) ^ (x & z) ^ (y & z);
        }

        // lowercase sigma0 function (σ0)
        public static uint sigma0(uint x)
        {
            // Perform bitwise rotations and shifts on the input
            return RightRotate(x, 7) ^ RightRotate(x, 18) ^ RightShift(x, 3);
        }

        // lowercase sigma1 function (σ1)
        public static uint sigma1(uint x)
        {
            // Perform bitwise rotations and shifts on the input
            return RightRotate(x, 17) ^ RightRotate(x, 19) ^ RightShift(x, 10);
        }

        // uppercase Sigma0 function (Σ0)
        public static uint Sigma0(uint x)
        {
            // Perform different bitwise rotations and shifts on the input
            return RightRotate(x, 2) ^ RightRotate(x, 13) ^ RightRotate(x, 22);
        }

        // uppercase Sigma1 function (Σ1)
        public static uint Sigma1(uint x)
        {
            // Perform different bitwise rotations and shifts on the input
            return RightRotate(x, 6) ^ RightRotate(x, 11) ^ RightRotate(x, 25);
        }

        // Helper method to perform the choice operation on three 32-bit values
        public static uint Choice(uint x, uint y, uint z)
        {
            return (x & y) ^ ((~x) & z);
        }

        // Helper method to extract a subarray from an array
        public static T[] SubArray<T>(T[] data, int index, int length)
        {
            // Create a new array to store the subarray
            T[] result = new T[length];

            // Use Array.Copy to copy the specified portion of the original array to the new array
            // Parameters: sourceArray, sourceIndex, destinationArray, destinationIndex, length
            Array.Copy(data, index, result, 0, length);

            // Return the extracted subarray
            return result;
        }

    }
}

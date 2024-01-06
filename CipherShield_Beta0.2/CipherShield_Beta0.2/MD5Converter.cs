using System;
using System.Text;

// Class for MD5 hashing
public class MD5Converter
{
    // Struct representing a union of a 32-bit unsigned integer and its byte array representation
    private struct MD5Union
    {
        // 32-bit unsigned integer
        public uint W;

        // Byte array representation of the 32-bit unsigned integer
        public byte[] B;

        // Constructor that initializes the struct with a 32-bit unsigned integer
        public MD5Union(uint w)
        {
            // Assign the integer value
            W = w;

            // Convert the integer to its byte array representation using BitConverter
            B = BitConverter.GetBytes(w);
        }
    }


    // Delegate for MD5 digest function
    private delegate uint DgstFctn(uint[] a);

    // Constants and arrays used in MD5 algorithm
    private static uint Func0(uint[] abcd) { return (abcd[1] & abcd[2]) | (~abcd[1] & abcd[3]); }
    private static uint Func1(uint[] abcd) { return (abcd[3] & abcd[1]) | (~abcd[3] & abcd[2]); }
    private static uint Func2(uint[] abcd) { return abcd[1] ^ abcd[2] ^ abcd[3]; }
    private static uint Func3(uint[] abcd) { return abcd[2] ^ (abcd[1] | ~abcd[3]); }
    // Calculate a table of constants for MD5
    private static uint[] CalcTable(uint[] k)
    {
        double s, pwr;
        int i;

        // Calculate 2^32 for later scaling
        pwr = Math.Pow(2.0, 32);

        // Iterate over 64 positions to generate constants
        for (i = 0; i < 64; i++)
        {
            // Calculate the absolute value of the sine of (1.0 + i)
            s = Math.Abs(Math.Sin(1.0 + i));

            // Scale the result by 2^32 and store it in the constants table
            k[i] = (uint)(s * pwr);
        }

        // Return the array of calculated constants
        return k;
    }


    // Rotate Left (circular left shift) operation on a 32-bit unsigned integer
    private static uint Rol(uint r, short N)
    {
        // Create a mask with N low-order bits set to 1
        uint mask1 = (uint)((1 << N) - 1);

        // Perform the circular left shift operation
        // - Shift the bits to the left by N positions
        // - Use the mask to keep only the low-order N bits
        // - Use the bitwise OR operation to combine the shifted bits with the rotated bits
        return ((r >> (32 - N)) & mask1) | ((r << N) & ~mask1);
    }


    // Main MD5 algorithm
    // Calculate MD5 hash for the given message
    private static uint[] AlgorithmsHashMD5(string msg)
    {
        // Initial MD5 hash values
        // Initial MD5 hash values
        uint[] h0 = { 0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476 };

        // Functions used in the MD5 algorithm
        DgstFctn[] ff = { Func0, Func1, Func2, Func3 };

        // Constants for each round
        short[] M = { 1, 5, 3, 7 };
        short[] O = { 0, 1, 5, 0 };
        short[] rot0 = { 7, 12, 17, 22 };
        short[] rot1 = { 5, 9, 14, 20 };
        short[] rot2 = { 4, 11, 16, 23 };
        short[] rot3 = { 6, 10, 15, 21 };
        short[][] rots = { rot0, rot1, rot2, rot3 };

        // Space for constants
        uint[] kspace = new uint[64];

        // Constants array (initialized to null initially)
        uint[] k = null;

        // Variables for hashing
        uint[] h = new uint[4];    // Hash result
        uint[] abcd = new uint[4]; // Working variables
        DgstFctn fctn;             // Function pointer
        short m, o, g;             // Constants for each round
        uint f;                    // Temporary variable
        short[] rotn;              // Rotation values for each round


        // Initialize constants and arrays
        if (k == null) k = CalcTable(kspace);

        // Set initial hash values
        for (int q = 0; q < 4; q++)
        {
            h[q] = h0[q];
        }

        // Prepare the input message for processing
        int grps = 1 + (msg.Length + 8) / 64;
        byte[] msg2 = new byte[64 * grps];
        Encoding.ASCII.GetBytes(msg, 0, msg.Length, msg2, 0);

        // Append the padding and length of the original message to the end of the padded message
        msg2[msg.Length] = 0x80;
        int qq = msg.Length + 1;
        while (qq < 64 * grps)
        {
            msg2[qq] = 0;
            qq++;
        }

        // Append the length of the original message (in bits) to the end of the padded message
        MD5Union u = new MD5Union();
        u.W = (uint)(8 * msg.Length);
        qq -= 8;
        Buffer.BlockCopy(BitConverter.GetBytes(u.W), 0, msg2, qq, 4);

        // Process each 64-byte block of the message
        int os = 0;
        for (int grp = 0; grp < grps; grp++)
        {
            // Copy the current 64-byte block to the beginning of the array
            Buffer.BlockCopy(msg2, os, msg2, 0, 64);

            // Initialize hash buffer with the current hash values
            for (qq = 0; qq < 4; qq++)
            {
                abcd[qq] = h[qq];
            }

            // Process each 16-word block within the 64-byte block
            for (int p = 0; p < 4; p++)
            {
                // Assign values based on the current round 'p'
                fctn = ff[p];   // Select the function for the current round
                rotn = rots[p]; // Select the rotation constants for the current round
                m = M[p];       // Select the constant 'm' for the current round
                o = O[p];       // Select the constant 'o' for the current round


                for (qq = 0; qq < 16; qq++)
                {
                    // Calculate the index within the 64-byte block
                    g = (short)((m * qq + o) % 16);

                    // Calculate the MD5 hash function for the current word
                    f = abcd[1] + Rol(abcd[0] + fctn(abcd) + k[qq + 16 * p] + BitConverter.ToUInt32(msg2, g * 4), rotn[qq % 4]);

                    // Update the hash buffer
                    abcd[0] = abcd[3];
                    abcd[3] = abcd[2];
                    abcd[2] = abcd[1];
                    abcd[1] = f;
                }
            }

            // Update the final hash values with the current block's result
            for (int p = 0; p < 4; p++)
            {
                h[p] += abcd[p];
            }

            // Move to the next 64-byte block
            os += 64;
        }

        // Return the final MD5 hash result
        return h;
    }


    // Get MD5 hash as a string
    private static string GetMD5String(string msg)
    {
        // Create a StringBuilder to efficiently build the string
        StringBuilder str = new StringBuilder();

        // Calculate the MD5 hash using the AlgorithmsHashMD5 method
        uint[] d = AlgorithmsHashMD5(msg);

        // Iterate through each uint in the MD5 hash result
        foreach (uint value in d)
        {
            // Convert each uint to a hexadecimal string and append it to the StringBuilder
            str.Append(value.ToString("x8"));
        }

        // Return the final MD5 hash as a string
        return str.ToString();
    }


    // Run the MD5 hashing process
    public static void RunMD5()
    {
        // Clear the console for a clean display
        Console.Clear();

        // Display header for MD5 Hash
        Console.WriteLine("MD5 Hash");
        Console.WriteLine("-----------\n");

        // Prompt the user to enter a message
        Console.Write("Enter a message: ");
        string message = Console.ReadLine();

        // Calculate the MD5 hash of the entered message
        string md5Hash = GetMD5String(message);

        // Display the original message and its MD5 hash
        Console.WriteLine($"\nOriginal Message: {message}");
        Console.WriteLine($"MD5 Hash: {md5Hash}");

        // Prompt the user to press any key to continue
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();

        // Clear the console for a clean display
        Console.Clear();
        return;
    }

}

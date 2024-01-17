using System.Text;

public class DES
{
    // Array to store the 16 subkeys used in DES encryption and decryption
    private static uint[] sub_key = new uint[16];

    // Main entry point for the DES submenu
    public static void DESSubMenu(DES des)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("DES Submenu");
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
                    des.EncryptText();
                    break;
                case "2":
                    Console.Clear();
                    des.DecryptText();
                    break;
                case "3":
                    Console.Clear();
                    return; // Return to the Block Cipher Submenu
                default:
                    ColorConsole.WriteError("Invalid option. \nPress any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
    }

    // Method to handle encryption functionality
    public void EncryptText()
    {
        Console.Write("Enter the 64-bit key in hexadecimal format (16 characters): ");
        string keyString = ValidateHexStringInputForEncryption();

        Console.Write("Enter the plaintext: ");
        string plaintext = Console.ReadLine();

        // Convert key to byte array
        byte[] key = HexStringToByteArray(keyString);

        StringBuilder encryptedSentence = new StringBuilder();

        // Split the plaintext into blocks of 64 bits
        for (int i = 0; i < plaintext.Length; i += 8)
        {
            // Extract a block of 64 bits (8 characters)
            string block = plaintext.Substring(i, Math.Min(8, plaintext.Length - i));

            // Pad the block to 8 characters if needed
            block = block.PadRight(8, ' ');

            // Convert the block to a byte array
            byte[] data = Encoding.ASCII.GetBytes(block);

            // Encrypt the data block
            byte[] encryptedData = Encrypt(key, data);

            // Append the encrypted block to the sentence
            encryptedSentence.Append(BitConverter.ToString(encryptedData).Replace("-", ""));
        }

        Console.WriteLine("\nEncrypted Text:");
        Console.WriteLine(encryptedSentence.ToString());

        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
        Console.Clear();
    }


    // Method to handle decryption functionality
    public void DecryptText()
    {
        Console.Write("Enter the 64-bit key in hexadecimal format (16 characters): ");
        string keyString = ValidateHexStringInputForEncryption();

        Console.Write("Enter the ciphertext: ");
        string ciphertext = Console.ReadLine();

        // Convert key to byte array
        byte[] key = HexStringToByteArray(keyString);

        StringBuilder decryptedSentence = new StringBuilder();

        // Split the ciphertext into blocks of 16 characters (128 bits)
        for (int i = 0; i < ciphertext.Length; i += 16)
        {
            // Extract a block of 16 characters
            string block = ciphertext.Substring(i, Math.Min(16, ciphertext.Length - i));

            // Convert the block to a byte array
            byte[] data = HexStringToByteArray(block);

            // Decrypt the data block
            byte[] decryptedData = Decrypt(key, data);

            // Convert the decrypted block to a string and append to the sentence
            decryptedSentence.Append(Encoding.ASCII.GetString(decryptedData).TrimEnd('\0'));
        }

        Console.WriteLine("\nDecrypted Text:");
        Console.WriteLine(decryptedSentence.ToString());

        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
        Console.Clear();
    }




    // Helper method for validating text input during encryption
    private string ValidateTextInput(int expectedLength)
    {
        string input;
        do
        {
            input = Console.ReadLine();

            if (input.Length != expectedLength)
            {
                ColorConsole.WriteLine($"Invalid input. Text must be exactly {expectedLength} characters.", ConsoleColor.Red);
            }
            else
            {
                break;
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        } while (true);
        return input;
    }

    // Helper method to validate hexadecimal input during encryption
    private string ValidateHexStringInputForEncryption()
    {
        string input;
        do
        {
            input = Console.ReadLine().ToUpper();

            if (input.Length != 16)
            {
                ColorConsole.WriteLine("Invalid input. Key must be exactly 16 characters.", ConsoleColor.Red);
            }
            else if (!IsHexadecimal(input))
            {
                ColorConsole.WriteLine("Invalid input. Key should only contain hexadecimal characters.", ConsoleColor.Red);
            }
            else
            {
                break;
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.Write("Enter the 64-bit key in hexadecimal format (16 characters): ");
        } while (true);
        return input;
    }

    // Helper method to check if a string is a valid hexadecimal
    // Check if the input string is a valid hexadecimal number
    private bool IsHexadecimal(string input)
    {
        // Iterate through each character in the input string
        foreach (char c in input)
        {
            // Check if the character is not a digit and not in the range of 'A' to 'F' (hexadecimal)
            if (!char.IsDigit(c) && !(c >= 'A' && c <= 'F'))
            {
                // If any character is invalid, return false
                return false;
            }
        }

        // If all characters are valid hexadecimal, return true
        return true;
    }


    // Convert a hexadecimal string to a byte array
    private byte[] HexStringToByteArray(string hexString)
    {
        int length = hexString.Length / 2;
        byte[] byteArray = new byte[length];
        for (int i = 0; i < length; i++)
        {
            byteArray[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        }
        return byteArray;
    }

    // Main encryption method
    public byte[] Encrypt(byte[] key, byte[] data)
    {
        // Generate the 16 subkeys used in the encryption
        GenerateSubKeys(key);

        // Perform the initial permutation on the data
        data = InitialPermutation(data);

        // Split the data into left and right halves
        uint left = BitConverter.ToUInt32(data, 0);
        uint right = BitConverter.ToUInt32(data, 4);

        // Perform the 16 rounds of the Feistel network
        for (int round = 0; round < 16; round++)
        {
            uint temp = FFunction(right, sub_key[round]);
            //swapping left & right halves
            temp ^= left;
            left = right;
            right = temp;
            // Swap
        }


        byte[] result = BitConverter.GetBytes(((long)right << 32) | left);
        result = FinalPermutation(result);

        return result;
    }

    // Main decryption method
    public byte[] Decrypt(byte[] key, byte[] data)
    {
        // Generate the 16 subkeys used in the decryption
        GenerateSubKeys(key);

        // Perform the initial permutation on the data
        data = InitialPermutation(data);

        // Split the data into left and right halves
        uint left = BitConverter.ToUInt32(data, 0);
        uint right = BitConverter.ToUInt32(data, 4);

        // Perform the 16 rounds of the Feistel network in reverse order
        for (int round = 15; round >= 0; round--)
        {
            uint temp = FFunction(left, sub_key[round]);
            temp ^= right;
            right = left;
            left = temp;
        }

        // Swap left and right before the final permutation
        byte[] result = BitConverter.GetBytes(((long)right << 32) | left);
        result = FinalPermutation(result);

        return result;
    }

    // Generate the 16 subkeys used in DES
    private void GenerateSubKeys(byte[] key)
    {
        // Ensure the key is 64 bits (8 bytes)
        if (key.Length != 8)
        {
            throw new ArgumentException("Key must be 64 bits (8 bytes) long.");
        }

        // Permutation Choice 1 (PC-1)
        uint keyBits = 0;
        for (int i = 0; i < 8; i++)
        {
            keyBits |= (uint)key[i] << ((7 - i) * 8);
        }

        uint permutedChoice1 = PermuteChoice1(keyBits);

        // Split into left and right halves
        uint c = (permutedChoice1 >> 28) & 0x0FFFFFFF;
        uint d = permutedChoice1 & 0x0FFFFFFF;

        for (int round = 0; round < 16; round++)
        {
            // Perform key schedule calculations
            c = LeftShift(c, ShiftBits[round]);
            d = LeftShift(d, ShiftBits[round]);

            // Permutation Choice 2 (PC-2)
            uint subKey = ((c & 0x0FFFFFFF) << 28) | (d & 0x0FFFFFFF);
            sub_key[round] = PermuteChoice2(subKey);
        }
    }

    // The number of bits to shift left for each round [ 1,2,9,16th round shift value will be 1] 
    private static int[] ShiftBits = {
        1, 1, 2, 2,
        2, 2, 2, 2,
        1, 2, 2, 2,
        2, 2, 2, 1
    };

    // Perform Permutation Choice 1 (PC-1) on the key
    private uint PermuteChoice1(uint key)
    {
        // Define the bit positions for PC-1 permutation
        int[] pc1 = {
        57, 49, 41, 33, 25, 17, 9,
        1, 58, 50, 42, 34, 26, 18,
        10, 2, 59, 51, 43, 35, 27,
        19, 11, 3, 60, 52, 44, 36,
        63, 55, 47, 39, 31, 23, 15,
        7, 62, 54, 46, 38, 30, 22,
        14, 6, 61, 53, 45, 37, 29,
        21, 13, 5, 28, 20, 12, 4
    };

        // Perform the permutation
        uint permutedKey = 0;
        for (int i = 0; i < pc1.Length; i++)
        {
            // Calculate the original position in the key before permutation
            int position = pc1[i] - 1;

            // Extract the bit value from the original key
            uint bitValue = (key >> position) & 1;

            // Update the corresponding position in the permuted key
            permutedKey |= bitValue << i;
        }

        return permutedKey;
    }


    // Perform Permutation Choice 2 (PC-2) on the key
    private uint PermuteChoice2(uint key)
    {
        // Define the bit positions for PC-2 permutation
        int[] pc2 = {
        14, 17, 11, 24, 1, 5, 3, 28,
        15, 6, 21, 10, 23, 19, 12, 4,
        26, 8, 16, 7, 27, 20, 13, 2,
        41, 52, 31, 37, 47, 55, 30, 40,
        51, 45, 33, 48, 44, 49, 39, 56,
        34, 53, 46, 42, 50, 36, 29, 32
    };

        // Perform the permutation
        uint permutedKey = 0;
        for (int i = 0; i < pc2.Length; i++)
        {
            // Calculate the original position in the key before permutation
            int position = pc2[i] - 1;

            // Extract the bit value from the original key
            uint bitValue = (key >> position) & 1;

            // Update the corresponding position in the permuted key
            permutedKey |= bitValue << i;
        }

        return permutedKey;
    }


    // Perform left circular shift on a 28-bit value
    private uint LeftShift(uint value, int shift)
    {
        // Perform left shift
        return (value << shift) | (value >> (28 - shift));
    }

    // Perform the initial permutation (IP) on the data
    // Perform the initial permutation (IP) on the data
    private byte[] InitialPermutation(byte[] data)
    {
        // Define the bit positions for the initial permutation (IP)
        int[] initialPermutation = {
        58, 50, 42, 34, 26, 18, 10, 2,
        60, 52, 44, 36, 28, 20, 12, 4,
        62, 54, 46, 38, 30, 22, 14, 6,
        64, 56, 48, 40, 32, 24, 16, 8,
        57, 49, 41, 33, 25, 17, 9, 1,
        59, 51, 43, 35, 27, 19, 11, 3,
        61, 53, 45, 37, 29, 21, 13, 5,
        63, 55, 47, 39, 31, 23, 15, 7
    };

        // Perform the initial permutation
        byte[] permutedData = new byte[8];
        for (int i = 0; i < initialPermutation.Length; i++)
        {
            // Calculate the original position in the data before permutation
            int position = initialPermutation[i] - 1;

            // Calculate the byte index and bit index based on the original position
            int byteIndex = position / 8;
            int bitIndex = position % 8;

            // Extract the bit value from the original data
            byte bitValue = (byte)((data[byteIndex] >> (7 - bitIndex)) & 1);

            // Update the corresponding position in the permuted data
            permutedData[i / 8] |= (byte)(bitValue << (7 - (i % 8)));
        }

        return permutedData;
    }


    // Perform the final permutation (IP^(-1)) on the data
    // Perform the final permutation (IP^(-1)) on the data
    private byte[] FinalPermutation(byte[] data)
    {
        // Define the bit positions for the final permutation (IP^(-1))
        int[] finalPermutation = {
        40, 8, 48, 16, 56, 24, 64, 32,
        39, 7, 47, 15, 55, 23, 63, 31,
        38, 6, 46, 14, 54, 22, 62, 30,
        37, 5, 45, 13, 53, 21, 61, 29,
        36, 4, 44, 12, 52, 20, 60, 28,
        35, 3, 43, 11, 51, 19, 59, 27,
        34, 2, 42, 10, 50, 18, 58, 26,
        33, 1, 41, 9, 49, 17, 57, 25
    };

        // Perform the final permutation
        byte[] permutedData = new byte[8];
        for (int i = 0; i < finalPermutation.Length; i++)
        {
            // Calculate the original position in the data before permutation
            int position = finalPermutation[i] - 1;

            // Calculate the byte index and bit index based on the original position
            int byteIndex = position / 8;
            int bitIndex = position % 8;

            // Extract the bit value from the original data
            byte bitValue = (byte)((data[byteIndex] >> (7 - bitIndex)) & 1);

            // Update the corresponding position in the permuted data
            permutedData[i / 8] |= (byte)(bitValue << (7 - (i % 8)));
        }

        return permutedData;
    }



    private uint FFunction(uint data, uint key)
    {
        // Expansion permutation
        uint expandedData = ExpansionPermutation(data);

        // XOR with the subkey
        uint xoredData = expandedData ^ key;

        // Substitution using S-boxes
        uint substitutedData = Substitution(xoredData);

        // Permutation (P-box)
        uint permutedData = Permutation(substitutedData);

        return permutedData;
    }

    // Perform expansion permutation on a 32-bit data input
    private uint ExpansionPermutation(uint data)
    {
        // Define the bit positions for the expansion permutation
        int[] expansionPermutation = {
        32, 1, 2, 3, 4, 5, 4, 5,
        6, 7, 8, 9, 8, 9, 10, 11,
        12, 13, 12, 13, 14, 15, 16, 17,
        16, 17, 18, 19, 20, 21, 20, 21,
        22, 23, 24, 25, 24, 25, 26, 27,
        28, 29, 28, 29, 30, 31, 32, 1
    };

        // Initialize a variable to store the expanded data
        uint expandedData = 0;

        // Iterate through each position in the expansion permutation
        for (int i = 0; i < expansionPermutation.Length; i++)
        {
            // Get the position for the current iteration
            int position = expansionPermutation[i] - 1;

            // Extract the bit value from the original data at the specified position
            uint bitValue = (data >> (32 - position)) & 1;

            // Set the bit in the expanded data at the current iteration position
            expandedData |= bitValue << i;
        }

        // Return the result of the expansion permutation
        return expandedData;
    }


    private uint Substitution(uint data)
    {
        // Define the actual DES S-boxes
        int[,] sBoxes = {
        // S-box 1
        {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
        // S-box 2
        {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
        // S-box 3
        {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
        // S-box 4
        {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13},
        // S-box 5
        {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
        // S-box 6
        {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
        // S-box 7
        {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
        // S-box 8
        {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}
    };

        // Perform substitution using S-boxes
        uint substitutedData = 0;
        int bitPosition = 0;
        for (int i = 0; i < 8; i++)
        {
            // Extract 6 bits for each S-box
            int sixBits = (int)((data >> bitPosition) & 0x3F);

            // Determine the column for the S-box
            int col = (sixBits >> 1) & 0x0F;

            // Retrieve the 4-bit value from the actual DES S-box
            int sBoxValue = sBoxes[i, col];

            // Incorporate the 4-bit value into the result
            substitutedData |= (uint)sBoxValue << bitPosition;

            // Move to the next 6 bits
            bitPosition += 6;
        }

        return substitutedData;
    }


    // Perform permutation (P-box) on a 32-bit data input
    private uint Permutation(uint data)
    {
        // Define the bit positions for the permutation (P-box)
        int[] permutation = {
        16, 7, 20, 21, 29, 12, 28, 17,
        1, 15, 23, 26, 5, 18, 31, 10,
        2, 8, 24, 14, 32, 27, 3, 9,
        19, 13, 30, 6, 22, 11, 4, 25
    };

        // Initialize a variable to store the permuted data
        uint permutedData = 0;

        // Iterate through each position in the permutation (P-box)
        for (int i = 0; i < permutation.Length; i++)
        {
            // Get the position for the current iteration
            int position = permutation[i] - 1;

            // Extract the bit value from the original data at the specified position
            uint bitValue = (data >> position) & 1;

            // Set the bit in the permuted data at the current iteration position
            permutedData |= bitValue << i;
        }

        // Return the result of the permutation (P-box)
        return permutedData;
    }


    // Helper method to pad the input to a multiple of 64 bits
    private string PadToMultipleOf64Bits(string input)
    {
        int remainder = input.Length % 8;
        if (remainder != 0)
        {
            // Pad the input to the next multiple of 8 (64 bits)
            input = input.PadRight(input.Length + 8 - remainder, ' ');
        }
        return input;
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{

    class Transposition
    {
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

            // now we can construct the fill the rail matrix
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


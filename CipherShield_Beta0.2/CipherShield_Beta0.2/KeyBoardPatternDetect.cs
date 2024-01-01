using System.Collections.Generic;
using CipherShield_Beta;

public class KeyboardPatternDetect : IStrengthCheckerAlgorithms
{
    public int CheckStrength(string password)
    {
        if (ContainsRepeatingPatterns(password) || ContainsCommonKeyboardPatterns(password))
        {
            return 0; // Password contains keyboard patterns, considered weak.
        }

        return 90; // Password does not contain keyboard patterns, considered strong.
    }

    public string GetFeedback(int strength)
    {
        return strength >= 80 ? "Low" : "High";
    }

    private bool ContainsCommonKeyboardPatterns(string password)
    {
        List<string> patterns = new List<string>
        {
            "abcdefghijklmnopqrstuvwxyz",
            "qwertyuiop",
            "asdfghjkl",
            "zxcvbnm",
            "1234567890",
            "password"
        };

        foreach (string pattern in patterns)
        {
            for (int i = 0; i <= pattern.Length - 4; i++)
            {
                string subPattern = Substring(pattern, i, 4);
                if (Contains(password, subPattern) || Contains(password, ReverseString(subPattern)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private string ReverseString(string input)
    {
        char[] charArray = ToCharArray(input);
        ArrayReverse(charArray);
        return new string(charArray);
    }

    private bool ContainsRepeatingPatterns(string password)
    {
        // Check for repeating patterns of length 3 or more.
        for (int i = 0; i < password.Length - 2; i++)
        {
            string subsequence = Substring(password, i, 3);
            if (LastIndexOf(password, subsequence) > i)
            {
                return true;
            }
        }

        return false;
    }

    private char[] ToCharArray(string input)
    {
        char[] charArray = new char[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            charArray[i] = input[i];
        }
        return charArray;
    }

    private void ArrayReverse(char[] array)
    {
        int left = 0;
        int right = array.Length - 1;

        while (left < right)
        {
            char temp = array[left];
            array[left] = array[right];
            array[right] = temp;
            left++;
            right--;
        }
    }

    private bool Contains(string input, string value)
    {
        for (int i = 0; i <= input.Length - value.Length; i++)
        {
            if (Substring(input, i, value.Length) == value)
            {
                return true;
            }
        }
        return false;
    }

    private int LastIndexOf(string input, string value)
    {
        for (int i = input.Length - value.Length; i >= 0; i--)
        {
            if (Substring(input, i, value.Length) == value)
            {
                return i;
            }
        }
        return -1;
    }

    private string Substring(string input, int startIndex, int length)
    {
        char[] charArray = new char[length];
        for (int i = 0; i < length; i++)
        {
            charArray[i] = input[startIndex + i];
        }
        return new string(charArray);
    }
}

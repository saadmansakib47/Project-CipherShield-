using CipherShield_Beta;
using System.Collections.Generic;

// Class implementing IStrengthCheckerAlgorithms to check password strength based on keyboard patterns
public class KeyboardPatternDetect : IStrengthCheckerAlgorithms
{
    // Check password strength based on keyboard patterns
    public int CheckStrength(string password)
    {
        if (ContainsRepeatingPatterns(password) || ContainsCommonKeyboardPatterns(password))
        {
            return 0; // Password contains keyboard patterns, considered weak.
        }

        return 90; // Password does not contain keyboard patterns, considered strong.
    }

    // Get feedback based on password strength
    public string GetFeedback(int strength)
    {
        return strength >= 80 ? "Low" : "High";
    }

    // Check if the password contains common keyboard patterns
    private bool ContainsCommonKeyboardPatterns(string password)
    {
        // List of common keyboard patterns
        List<string> patterns = new List<string>
        {
            "abcdefghijklmnopqrstuvwxyz",
            "qwertyuiop",
            "asdfghjkl",
            "zxcvbnm",
            "1234567890",
            "password"
        };

        // Iterate through each pattern
        foreach (string pattern in patterns)
        {
            // Check for subsequences of length 4
            for (int i = 0; i <= pattern.Length - 4; i++)
            {
                string subPattern = pattern.Substring(i, 4);
                // Check if the password contains the subPattern or its reverse
                if (password.Contains(subPattern) || password.Contains(ReverseString(subPattern)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Reverse a given string
    private string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    // Check if the password contains repeating patterns
    private bool ContainsRepeatingPatterns(string password)
    {
        // Iterate through the password up to the third-to-last character
        for (int i = 0; i < password.Length - 2; i++)
        {
            // Extract a subsequence of length 3
            string subsequence = password.Substring(i, 3);

            // Check if the subsequence occurs later in the password
            if (password.LastIndexOf(subsequence) > i)
            {
                // If yes, it means there's a repeating pattern
                return true;
            }
        }

        // If no repeating patterns are found, return false
        return false;
    }

}

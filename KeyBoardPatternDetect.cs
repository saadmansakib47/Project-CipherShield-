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
        "password",
        "1qaz2wsx" 
    };

        foreach (string pattern in patterns)
        {
            for (int i = 0; i <= pattern.Length - 4; i++)
            {
                string subPattern = pattern.Substring(i, 4);
                if (password.Contains(subPattern) || password.Contains(ReverseString(subPattern)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }



    private bool ContainsRepeatingPatterns(string password)
    {
        // Check for repeating patterns of length 3 or more.
        for (int i = 0; i < password.Length - 2; i++)
        {
            string subsequence = password.Substring(i, 3);
            if (password.LastIndexOf(subsequence) > i)
            {
                return true;
            }
        }

        return false;
    }

}


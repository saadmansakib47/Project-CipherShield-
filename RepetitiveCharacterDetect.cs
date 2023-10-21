namespace CipherShield_Beta
{
    public class RepetitiveCharacterDetect : IStrengthCheckerAlgorithms
    {
        public int CheckStrength(string password)
        {
            // Assess the password based on repetitive character patterns.
            int repeatedChars = 0;

            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] == password[i + 1])
                {
                    repeatedChars++;
                }
            }

            // Define a scoring strategy for repetitive characters.
            if (repeatedChars == 0 || repeatedChars == 1)
            {
                return 100; // No repetitive characters, strong.
            }
            else if (repeatedChars>1 && repeatedChars<=2)
            {
                return 70; // Few repetitive characters, moderate.
            }
            else
            {
                return 40; // Many repetitive characters, weak.
            }
        }

        public string GetFeedback(int strength)
        {
            // Provide feedback based on the Repetitive Character Detection strength score.
            if (strength >= 80)
            {
                return "Low";
            }
            else if (strength >= 50)
            {
                return "Moderate";
            }
            else
            {
                return "High";
            }
        }
    }

}


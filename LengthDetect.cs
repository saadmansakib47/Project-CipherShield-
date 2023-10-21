namespace CipherShield_Beta
{
    public class LengthDetect : IStrengthCheckerAlgorithms
    {
        public int CheckStrength(string password)
        {
            // Assess the password based on its length.
            int passwordLength = password.Length;

            // Define a scoring strategy for password length.
            if (passwordLength >= 12)
            {
                return 100; // Long password, strong.
            }
            else if (passwordLength >= 8)
            {
                return 70; // Moderately long password, moderate.
            }
            else
            {
                return 40; // Short password, weak.
            }
        }

        public string GetFeedback(int strength)
        {
            // Provide feedback based on the Length Detection strength score.
            if (strength >= 80)
            {
                return "Good";
            }
            else if (strength >= 50)
            {
                return "Moderate";
            }
            else
            {
                return "Short";
            }
        }
    }
}


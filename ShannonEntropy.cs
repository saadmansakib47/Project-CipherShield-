namespace CipherShield_Beta
{
    public class ShannonEntropy : IStrengthCheckerAlgorithms
    {
        public int CheckStrength(string password)
        {
            // Shannon Entropy algorithm implementation for password strength.
            int passwordLength = password.Length;

            // Count the number of unique characters in the password.
            Dictionary<char, int> characterCounts = new Dictionary<char, int>();
            foreach (char character in password)
            {
                if (characterCounts.ContainsKey(character))
                {
                    characterCounts[character]++;
                }
                else
                {
                    characterCounts.Add(character, 1);
                }
            }

            // Calculate the entropy using the Shannon Entropy formula.
            double entropy = 0.0;
            foreach (var characterCount in characterCounts)
            {
                double characterProbability = (double)characterCount.Value / passwordLength;
                entropy -= characterProbability * Math.Log(characterProbability, 2);
            }

            // Convert entropy to a strength score (e.g., mapping to a scale from 0 to 100).
            int strength = (int)((entropy / Math.Log(94, 2)) * 100);

            return strength;
        }

        public string GetFeedback(int strength)
        {
            // Provide feedback based on the Shannon Entropy strength score.
            if (strength >= 70)
            {
                return "High";
            }
            else if (strength >= 50)
            {
                return "Moderate";
            }
            else
            {
                return "Low";
            }
        }
    }

}


namespace CipherShield_Beta
{
    public interface IStrengthCheckerAlgorithms
    {
        // Method to calculate the strength of a password based on the algorithm.
        int CheckStrength(string password);

        // Method to provide feedback on the password's strength.
        string GetFeedback(int strength);
    }
}

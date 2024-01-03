using CipherShield_Beta0._2;

namespace CipherShield_Beta
{
    public interface ICipherText
    {
        static void CipherTextSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Text Encryption/Decryption");
                Console.WriteLine("--------------------------\n\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine(" Substitution Cipher");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" Transposition Cipher");
                ColorConsole.Write("[ 3 ]", ConsoleColor.Blue);
                Console.WriteLine(" Block Cipher");
                ColorConsole.Write("[ 4 ]", ConsoleColor.Blue);
                Console.WriteLine(" RSA (Rivest - Shamir - Adleman) Algorithm");
                ColorConsole.Write("[ 5 ]", ConsoleColor.Blue);
                Console.WriteLine(" Back to Previous Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ISubstitutionCipher.SubstitutionCipherSubMenu();
                        break;
                    case "2":
                        ITranspositionCipher.TranspositionCipherSubMenu();
                        break;
                    case "3":
                        IBlockCipher.BlockCipherSubMenu();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Enter your text:");
                        string message = Console.ReadLine();
                        Console.WriteLine("\nOriginal Text: \"" + message + "\"\n");
                        var rsa = new RSACrypt();
                        var cypherText = rsa.Encrypt(message);
                        
                        Console.Write("Cypher Text: ");
                        var isFirstLetter = true;
                        
                        foreach (var place in cypherText)
                        {
                            if (isFirstLetter)
                            {
                                isFirstLetter = false;
                                Console.Write(place);
                                continue;
                            }
                            Console.Write(", " + place);
                        }
                        Console.WriteLine();
                        
                        Console.WriteLine("\nPress Y/y to decrypt the cyphertext.");
                        
                        string a = Console.ReadLine();
                        if (a == "y" || a == "Y")
                        {
                            var decryptedText = rsa.Decrypt(cypherText);
                            Console.WriteLine("\nDecrypted Text: \"" + decryptedText + "\"");
                        }
                        
                        Console.ReadLine();
                        
                        break;
                    case "5":
                        Console.Clear();
                        return; // Return to the Cipher Menu.
                    default:
                        ColorConsole.WriteError("Invalid option. \nPress any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

    }
}

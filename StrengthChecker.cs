using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CipherShield_Beta
{
    public class StrengthChecker
    {
        private static string ReadMaskedPassword()
        {
            string password = "";
            bool showPassword = false; // Flag to toggle password visibility
            bool displayRangeInfo = false; // display scoring range information 
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    if (showPassword)
                    {
                        Console.Write(key.KeyChar);
                    }
                    else
                    {
                        ColorConsole.Write("*", ConsoleColor.DarkYellow);
                    }
                }
                else if (key.Key == ConsoleKey.Tab)
                {
                    showPassword = !showPassword; // Toggle password visibility
                    Console.SetCursorPosition(Console.CursorLeft - password.Length, Console.CursorTop);
                    Console.Write(new string(' ', password.Length));
                    Console.SetCursorPosition(Console.CursorLeft - password.Length, Console.CursorTop);
                    if (showPassword)
                    {
                        Console.Write(password);
                    }
                    else
                    {
                        ColorConsole.Write(new string('*', password.Length), ConsoleColor.DarkYellow);
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        public static void PasswordStrengthReport()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Password Strength-Checker");
                Console.WriteLine("-------------------------\n");
                ColorConsole.Write("[ 1 ]", ConsoleColor.Blue);
                Console.WriteLine(" Enter your password ");
                ColorConsole.Write("[ 2 ]", ConsoleColor.Blue);
                Console.WriteLine(" Return to Main Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter your password : ");
                        ColorConsole.WriteInfo("[ Press tab to toggle visibility ]\n");
                        string password = ReadMaskedPassword();
                        CalculateAndDisplayStrength(password);
                        break;
                    case "2":
                        return; // Return to the main menu.
                    default:
                        ColorConsole.WriteError("Invalid option. \nPress any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        static void CalculateAndDisplayStrength(string password)
        {
            // Handle input edge cases.
            if (password.Length <= 1)
            {
                ColorConsole.WriteError("Password length can't be 0 or 1.\nPress any key to continue.");
                Console.ReadKey();
                return;
            }

            // Create instances for the strength checking algorithms
            ShannonEntropy shannonEntropy = new ShannonEntropy();
            KeyboardPatternDetect keyboardPatternDetect = new KeyboardPatternDetect();
            RepetitiveCharacterDetect repetitiveCharacterDetect = new RepetitiveCharacterDetect();
            LengthDetect lengthDetect = new LengthDetect();

            // Calculate individual metrics
            int shannonEntropyScore = shannonEntropy.CheckStrength(password);
            int keyboardPatternScore = keyboardPatternDetect.CheckStrength(password);
            int repetitiveCharsScore = repetitiveCharacterDetect.CheckStrength(password);
            int lengthScore = lengthDetect.CheckStrength(password);


            // Define multipliers
            double shannonEntropyMultiplier = 3.5;
            double keyboardPatternMultiplier = 1.0;
            double repetitiveCharsMultiplier = 2.0;
            double lengthMultiplier = 1.75;

            // Calculate overall strength based on multipliers
            int overallStrengthScore = (int)(shannonEntropyScore * shannonEntropyMultiplier +
                                   keyboardPatternScore * keyboardPatternMultiplier +
                                   repetitiveCharsScore * repetitiveCharsMultiplier +
                                   lengthScore * lengthMultiplier);

            string passwordStrengthLevel = "Very weak";
            if (overallStrengthScore >= 700)
            {
                passwordStrengthLevel = "Very Strong";
            }
            else if (overallStrengthScore >= 600 && overallStrengthScore < 700)
            {
                passwordStrengthLevel = "Strong";
            }
            else if (overallStrengthScore >= 500 && overallStrengthScore < 600)
            {
                passwordStrengthLevel = "Moderate";
            }
            else if (overallStrengthScore >= 460 && overallStrengthScore < 500)
            {
                passwordStrengthLevel = "Weak";
            }


            // Determine feedback based on the overall strength
            string shannonEntropyFeedback = shannonEntropy.GetFeedback(shannonEntropyScore);
            string keyboardPatternFeedback = keyboardPatternDetect.GetFeedback(keyboardPatternScore);
            string repetitiveCharsFeedback = repetitiveCharacterDetect.GetFeedback(repetitiveCharsScore);
            string lengthFeedback = lengthDetect.GetFeedback(lengthScore);

            // Display the password strength report.


            Console.WriteLine("\nPassword Strength Report:");
            Console.WriteLine("------------------------");

            //For shannonEntropy
            ColorConsole.Write("Randomness: ", ConsoleColor.DarkCyan);
            if (shannonEntropyFeedback == "Low")
            {
                ColorConsole.WriteLine("Low", ConsoleColor.Red);
            }
            else if (shannonEntropyFeedback == "Moderate")
            {
                ColorConsole.WriteLine("Moderate", ConsoleColor.DarkYellow);
            }
            else
            {
                ColorConsole.WriteLine("High", ConsoleColor.Green);
            }

            //For keyboardPattern
            ColorConsole.Write("Pattern Detection: ", ConsoleColor.DarkCyan);
            if (keyboardPatternFeedback == "High")
            {
                ColorConsole.WriteLine("High", ConsoleColor.Red);
            }
            else if (keyboardPatternFeedback == "Moderate")
            {
                ColorConsole.WriteLine("Moderate", ConsoleColor.DarkYellow);
            }
            else
            {
                ColorConsole.WriteLine("Low", ConsoleColor.Green);
            }

            //For Repetition 
            ColorConsole.Write("Repetition: ", ConsoleColor.DarkCyan);
            if (repetitiveCharsFeedback == "High")
            {
                ColorConsole.WriteLine("High", ConsoleColor.Red);
            }
            else if (repetitiveCharsFeedback == "Moderate")
            {
                ColorConsole.WriteLine("Moderate", ConsoleColor.DarkYellow);
            }
            else
            {
                ColorConsole.WriteLine("Low", ConsoleColor.Green);
            }

            //For length 
            ColorConsole.Write("Length: ", ConsoleColor.DarkCyan);
            if (lengthFeedback == "Short")
            {
                ColorConsole.WriteLine("Short", ConsoleColor.Red);
            }
            else if (lengthFeedback == "Moderate")
            {
                ColorConsole.WriteLine("Moderate", ConsoleColor.DarkYellow);
            }
            else
            {
                ColorConsole.WriteLine("Good", ConsoleColor.Green);
            }

            ColorConsole.Write("\nPassword Strength Score: ", ConsoleColor.DarkCyan);
            Console.WriteLine(overallStrengthScore);


            ColorConsole.Write("Password Strength: ", ConsoleColor.DarkCyan);
            if (passwordStrengthLevel == "Very weak")
            {
                ColorConsole.Write("Very weak", ConsoleColor.Red);
            }
            else if (passwordStrengthLevel == "Weak")
            {
                ColorConsole.Write("Weak", ConsoleColor.Magenta);
            }
            else if (passwordStrengthLevel == "Moderate")
            {
                ColorConsole.Write("Moderate", ConsoleColor.DarkYellow);
            }
            else if (passwordStrengthLevel == "Strong")
            {
                ColorConsole.Write("Strong", ConsoleColor.Green);
            }
            else if (passwordStrengthLevel == "Very Strong")
            {
                ColorConsole.Write("Very Strong", ConsoleColor.DarkGreen);
            }
            else
            {
                ColorConsole.WriteWarning("Error : Feedback is not obtainable. ");
            }

            // Prompt the user to return to the report menu or exit.
            Console.WriteLine("\n\nPress tab to see scoring-range info \nor any other key to continue.");

            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Tab)
            {
                ColorConsole.WriteLine("\nScore < 460 : Very Weak\r\n460 ≤ Score < 500 : Weak\r\n500 ≤ Score < 600 :Moderate\r\n600 ≤ Score < 700 :Strong\r\n700 ≤ Score < 800 : Very Strong", ConsoleColor.DarkGray);
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
            }
            else
            {
                return;
            }
        }

    }
}


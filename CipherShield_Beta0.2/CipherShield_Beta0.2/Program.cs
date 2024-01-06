using CipherShield_Beta;
using CipherShield_Beta0._2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("CipherShield Main Page");
            Console.WriteLine("----------------------");
            Console.WriteLine("                             ___ _       _               __ _     _      _     _");
            Console.WriteLine("                            / __(_)_ __ | |__   ___ _ __/ _\\ |__ (_) ___| | __| |");
            ColorConsole.WriteLine("                           / /  | | '_ \\| '_ \\ / _ \\ '__\\ \\| '_ \\| |/ _ \\ |/ _` |", ConsoleColor.Red);
            ColorConsole.WriteLine("                          / /___| | |_) | | | |  __/ |  _\\ \\ | | | |  __/ | (_| |", ConsoleColor.Red);
            ColorConsole.WriteLine("                          \\____/|_| .__/|_| |_|\\___|_|  \\__/_| |_|_|\\___|_|\\__._|", ConsoleColor.Yellow);
            ColorConsole.WriteLine("                                  | _|", ConsoleColor.Yellow);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                                Main Menu ");
            Console.WriteLine("");
            Console.WriteLine("                                                    ");
            ColorConsole.Write("                                       [ 1 ]", ConsoleColor.Green);
            Console.WriteLine(" Password ToolBox");
            ColorConsole.Write("                                       [ 2 ]", ConsoleColor.Green);
            Console.WriteLine(" Text Encryption/Decryption");
            ColorConsole.Write("                                       [ 3 ]", ConsoleColor.Green);
            Console.WriteLine(" Hashing");
            ColorConsole.Write("                                       [ 4 ]", ConsoleColor.Green);
            Console.WriteLine(" Parse Log Files");
            ColorConsole.Write("                                       [ 5 ]", ConsoleColor.Green);
            Console.WriteLine(" Exit");
            Console.WriteLine("                                                    ");
            Console.Write("                                         Select an option: ");
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    IPasswordToolbox.PasswordToolbox();
                    break;
                case "2":
                    ICipherText.CipherTextSubMenu(); 
                    break;
                case "3":
                    IHash.HashingMenu();
                    break;
                case "4":
                    LogFileParser.ParseLogFiles();
                    break;
                case "5":
                    return; 
                default:
                    ColorConsole.WriteLine("Invalid option. Press any key to continue.", ConsoleColor.Red);
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
    }
}


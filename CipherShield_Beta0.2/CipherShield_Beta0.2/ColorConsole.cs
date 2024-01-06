﻿using System;
using System.Text.RegularExpressions;

// Static class for colored console output
public static class ColorConsole
{
    /// <summary>
    /// WriteLine with color
    /// </summary>
    /// <param name="text">Text to write</param>
    /// <param name="color">Color of the text (optional)</param>
    public static void WriteLine(string text, ConsoleColor? color = null)
    {
        if (color.HasValue)
        {
            var oldColor = Console.ForegroundColor;
            if (color == oldColor)
                Console.WriteLine(text);
            else
            {
                Console.ForegroundColor = color.Value;
                Console.WriteLine(text);
                Console.ForegroundColor = oldColor;
            }
        }
        else
            Console.WriteLine(text);
    }

    /// <summary>
    /// Writes out a line with a specific color as a string
    /// </summary>
    /// <param name="text">Text to write</param>
    /// <param name="color">A console color. Must match ConsoleColors collection names (case insensitive)</param>
    public static void WriteLine(string text, string color)
    {
        if (string.IsNullOrEmpty(color))
        {
            WriteLine(text);
            return;
        }

        if (!Enum.TryParse(color, true, out ConsoleColor col))
        {
            WriteLine(text);
        }
        else
        {
            WriteLine(text, col);
        }
    }

    /// <summary>
    /// Write with color
    /// </summary>
    /// <param name="text">Text to write</param>
    /// <param name="color">Color of the text (optional)</param>
    public static void Write(string text, ConsoleColor? color = null)
    {
        if (color.HasValue)
        {
            var oldColor = Console.ForegroundColor;
            if (color == oldColor)
                Console.Write(text);
            else
            {
                Console.ForegroundColor = color.Value;
                Console.Write(text);
                Console.ForegroundColor = oldColor;
            }
        }
        else
            Console.Write(text);
    }

    /// <summary>
    /// Writes out a line with color specified as a string
    /// </summary>
    /// <param name="text">Text to write</param>
    /// <param name="color">A console color. Must match ConsoleColors collection names (case insensitive)</param>
    public static void Write(string text, string color)
    {
        if (string.IsNullOrEmpty(color))
        {
            Write(text);
            return;
        }

        if (!ConsoleColor.TryParse(color, true, out ConsoleColor col))
        {
            Write(text);
        }
        else
        {
            Write(text, col);
        }
    }

    #region Wrappers and Templates

    /// <summary>
    /// Writes a line of header text wrapped in a pair of lines of dashes:
    /// -----------
    /// Header Text
    /// -----------
    /// and allows you to specify a color for the header. The dashes are colored
    /// </summary>
    /// <param name="headerText">Header text to display</param>
    /// <param name="wrapperChar">Wrapper character (-)</param>
    /// <param name="headerColor">Color for header text (yellow)</param>
    /// <param name="dashColor">Color for dashes (gray)</param>
    public static void WriteWrappedHeader(string headerText,
                                            char wrapperChar = '-',
                                            ConsoleColor headerColor = ConsoleColor.Yellow,
                                            ConsoleColor dashColor = ConsoleColor.DarkGray)
    {
        if (string.IsNullOrEmpty(headerText))
            return;

        string line = new string(wrapperChar, headerText.Length);

        WriteLine(line, dashColor);
        WriteLine(headerText, headerColor);
        WriteLine(line, dashColor);
    }

    // Lazy instantiation of Regex for color block extraction
    private static Lazy<Regex> colorBlockRegEx = new Lazy<Regex>(
        () => new Regex("\\[(?<color>.*?)\\](?<text>[^[]*)\\[/\\k<color>\\]", RegexOptions.IgnoreCase),
        isThreadSafe: true);

    /// <summary>
    /// Allows a string to be written with embedded color values using:
    /// This is [red]Red[/red] text and this is [cyan]Blue[/blue] text
    /// </summary>
    /// <param name="text">Text to display</param>
    /// <param name="baseTextColor">Base text color</param>
    public static void WriteEmbeddedColorLine(string text, ConsoleColor? baseTextColor = null)
    {
        if (baseTextColor == null)
            baseTextColor = Console.ForegroundColor;

        if (string.IsNullOrEmpty(text))
        {
            WriteLine(string.Empty);
            return;
        }

        int at = text.IndexOf("[");
        int at2 = text.IndexOf("]");
        if (at == -1 || at2 <= at)
        {
            WriteLine(text, baseTextColor);
            return;
        }

        while (true)
        {
            var match = colorBlockRegEx.Value.Match(text);
            if (match.Length < 1)
            {
                Write(text, baseTextColor);
                break;
            }

            // write up to expression
            Write(text.Substring(0, match.Index), baseTextColor);

            // strip out the expression
            string highlightText = match.Groups["text"].Value;
            string colorVal = match.Groups["color"].Value;

            Write(highlightText, colorVal);

            // remainder of string
            text = text.Substring(match.Index + match.Value.Length);
        }

        Console.WriteLine();
    }

    #endregion

    #region Success, Error, Info, Warning Wrappers

    /// <summary>
    /// Write a Success Line - green
    /// </summary>
    /// <param name="text">Text to write out</param>
    public static void WriteSuccess(string text)
    {
        WriteLine(text, ConsoleColor.Green);
    }

    /// <summary>
    /// Write an Error Line - Red
    /// </summary>
    /// <param name="text">Text to write out</param>
    public static void WriteError(string text)
    {
        WriteLine(text, ConsoleColor.Red);
    }

    /// <summary>
    /// Write a Warning Line - Yellow
    /// </summary>
    /// <param name="text">Text to write out</param>
    public static void WriteWarning(string text)
    {
        WriteLine(text, ConsoleColor.DarkYellow);
    }

    /// <summary>
    /// Write an Info Line - dark cyan
    /// </summary>
    /// <param name="text">Text to write out</param>
    public static void WriteInfo(string text)
    {
        WriteLine(text, ConsoleColor.DarkCyan);
    }

    #endregion
}

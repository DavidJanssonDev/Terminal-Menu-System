using System;
using Terminal.Gui;

namespace TerminalMenuGUI.TerminalGui.Utilities;

/// <summary>
/// Converts Core ConsoleColor values into Terminal.Gui Color values.
/// </summary>
public static class ColorConverter
{
    public static Color FromConsole(ConsoleColor c) => c switch
    {
        ConsoleColor.Black => Color.Black,

        ConsoleColor.DarkBlue => Color.Blue,
        ConsoleColor.DarkGreen => Color.Green,
        ConsoleColor.DarkCyan => Color.Cyan,
        ConsoleColor.DarkRed => Color.Red,
        ConsoleColor.DarkMagenta => Color.Magenta,
        ConsoleColor.DarkYellow => Color.Brown,   // closest match
        ConsoleColor.Gray => Color.Gray,

        ConsoleColor.DarkGray => Color.DarkGray,
        ConsoleColor.Blue => Color.BrightBlue,
        ConsoleColor.Green => Color.BrightGreen,
        ConsoleColor.Cyan => Color.BrightCyan,
        ConsoleColor.Red => Color.BrightRed,
        ConsoleColor.Magenta => Color.BrightMagenta,
        ConsoleColor.Yellow => Color.BrightYellow,
        ConsoleColor.White => Color.White,

        _ => Color.Gray
    };
}

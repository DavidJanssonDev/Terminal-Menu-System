using Terminal.Gui;
using TerminalMenuGUI.Core.Styling;
using TerminalMenuGUI.TerminalGui.Utilities;
using Attribute = Terminal.Gui.Attribute;

namespace TerminalMenuGUI.TerminalGui.Mapping;

/// <summary>
/// Maps Core <see cref="Style"/> objects to Terminal.Gui visual properties.
/// </summary>
public sealed class StyleMapper
{
    /// <summary>
    /// Applies the specified style to the given view, updating its color scheme if foreground or background colors are
    /// defined.
    /// </summary>
    /// <remarks>If the style specifies foreground or background colors, the view's color scheme is updated
    /// accordingly. Padding settings in the style are currently ignored due to platform limitations.</remarks>
    /// <param name="view">The view to which the style will be applied. Must not be null.</param>
    /// <param name="style">The style containing foreground and background color information to apply to the view.</param>
    public void Apply(View view, Style style)
    {
        // Colors: Core uses ConsoleColor?, Terminal.Gui uses Color
        if (style.Fg is not null || style.Bg is not null)
        {
            var fg = style.Fg is null
                ? Colors.Base.Normal.Foreground
                : ColorConverter.FromConsole(style.Fg.Value);

            var bg = style.Bg is null
                ? Colors.Base.Normal.Background
                : ColorConverter.FromConsole(style.Bg.Value);

            view.ColorScheme = new ColorScheme
            {
                Normal = Attribute.Make(fg, bg)
            };
        }

        // NOTE: Terminal.Gui 1.19 View does not have View.Padding.
        // For now ignore Core Padding to get the project compiling.
        // Later: implement padding by wrapping views or using Border (if you add it).
    }
}

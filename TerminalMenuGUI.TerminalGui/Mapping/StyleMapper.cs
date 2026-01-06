using Terminal.Gui;
using TerminalMenuGUI.Core.Styling;
using Attribute = Terminal.Gui.Attribute;

namespace TerminalMenuGUI.TerminalGui.Mapping;

/// <summary>
/// Maps Core <see cref="Style"/> objects to Terminal.Gui visual properties.
/// </summary>
public sealed class StyleMapper
{
    /// <summary>
    /// Applies a Core style to a Terminal.Gui view.
    /// </summary>
    /// <param name="view">The target view.</param>
    /// <param name="style">The style to apply.</param>
    public void Apply(View view, Style style)
    {
        if (style.Fg is not null || style.Bg is not null)
        {
            view.ColorScheme = new ColorScheme
            {
                Normal = Attribute.Make(
                    style.Fg ?? Colors.Base.Normal.Foreground,
                    style.Bg ?? Colors.Base.Normal.Background
                )
            };
        }

        if (style.Padding > 0)
        {
            view.Padding = new Thickness(
                style.Padding,
                style.Padding,
                style.Padding,
                style.Padding
            );
        }
    }
}

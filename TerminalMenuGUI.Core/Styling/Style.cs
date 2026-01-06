using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TerminalMenuGUI.Core.Styling;

/// <summary>
/// Represents a set of visual styling properties for an element.
/// </summary>
/// <remarks>
/// This is similar to a computed CSS style object in web development.
/// Properties are intentionally minimal at first; add more as the framework grows.
/// </remarks>
public sealed class Style
{
    /// <summary>
    /// Gets or sets the padding applied inside the element's border.
    /// </summary>
    public int Padding { get; set; }

    /// <summary>
    /// Gets or sets the top margin applied outside the element.
    /// </summary>
    public int MarginTop { get; set; }

    /// <summary>
    /// Gets or sets the foreground color for text/content.
    /// </summary>
    public ConsoleColor? Fg { get; set; }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public ConsoleColor? Bg { get; set; }

    /// <summary>
    /// Merges values from another style into this style.
    /// </summary>
    /// <param name="other">The style whose values should be merged into this instance.</param>
    /// <remarks>
    /// The merging semantics are currently simple:
    /// <list type="bullet">
    /// <item><description>For nullable properties, non-null values override.</description></item>
    /// <item><description>For integer properties, non-zero values override.</description></item>
    /// </list>
    /// This is a pragmatic starting point; later you may want explicit "unset" values
    /// and more CSS-like behavior.
    /// </remarks>
    public void MergeFrom(Style other)
    {
        Padding = other.Padding != 0 ? other.Padding : Padding;
        MarginTop = other.MarginTop != 0 ? other.MarginTop : MarginTop;

        Fg = other.Fg ?? Fg;
        Bg = other.Bg ?? Bg;
    }

    /// <summary>
    /// Creates a shallow copy of this <see cref="Style"/>.
    /// </summary>
    /// <returns>A cloned <see cref="Style"/> with the same property values.</returns>
    public Style Clone() => new()
    {
        Padding = Padding,
        MarginTop = MarginTop,
        Fg = Fg,
        Bg = Bg
    };
}

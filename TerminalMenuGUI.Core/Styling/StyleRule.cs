namespace TerminalMenuGUI.Core.Styling;

/// <summary>
/// Represents a stylesheet rule consisting of a selector and a style payload.
/// </summary>
/// <remarks>
/// A rule is comparable to a CSS rule block, e.g.:
/// <c>button.primary { background: green; }</c>
/// </remarks>
public sealed class StyleRule
{
    /// <summary>
    /// Initializes a new <see cref="StyleRule"/> instance.
    /// </summary>
    /// <param name="selector">The selector used to match elements.</param>
    /// <param name="style">The style applied when the selector matches.</param>
    public StyleRule(Selector selector, Style style)
    {
        Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        Style = style ?? throw new ArgumentNullException(nameof(style));
    }

    /// <summary>
    /// Gets the selector used to match elements.
    /// </summary>
    public Selector Selector { get; }

    /// <summary>
    /// Gets the style payload applied when the selector matches.
    /// </summary>
    public Style Style { get; }
}

using TerminalMenuGUI.Core.Dom;

namespace TerminalMenuGUI.Core.Styling;

/// <summary>
/// Represents a simple CSS-like selector used to match elements.
/// </summary>
/// <remarks>
/// Supported selector forms (initial scope):
/// <list type="bullet">
/// <item><description><c>button</c> (type selector)</description></item>
/// <item><description><c>.primary</c> (class selector)</description></item>
/// <item><description><c>#app</c> (id selector)</description></item>
/// <item><description><c>div.menu</c> (type + class selector)</description></item>
/// </list>
/// Descendant selectors, multiple classes, attributes, and pseudo-selectors can be added later.
/// </remarks>
public sealed class Selector
{
    private Selector(string? type, string? id, string? @class)
    {
        Type = type;
        Id = id;
        Class = @class;
    }

    /// <summary>
    /// Gets the element type to match (e.g. <c>div</c>, <c>button</c>), or <c>null</c>.
    /// </summary>
    public string? Type { get; }

    /// <summary>
    /// Gets the element id to match (e.g. <c>app</c> for <c>#app</c>), or <c>null</c>.
    /// </summary>
    public string? Id { get; }

    /// <summary>
    /// Gets the class name to match (e.g. <c>primary</c> for <c>.primary</c>), or <c>null</c>.
    /// </summary>
    public string? Class { get; }

    /// <summary>
    /// Gets a numeric specificity score used for rule ordering.
    /// </summary>
    /// <remarks>
    /// This is a simplified specificity model:
    /// <list type="bullet">
    /// <item><description>Id selectors contribute 100 points.</description></item>
    /// <item><description>Class selectors contribute 10 points.</description></item>
    /// <item><description>Type selectors contribute 1 point.</description></item>
    /// </list>
    /// Higher specificity should override lower specificity when multiple rules match.
    /// </remarks>
    public int Specificity =>
        (Id is null ? 0 : 100) +
        (Class is null ? 0 : 10) +
        (Type is null ? 0 : 1);

    /// <summary>
    /// Determines whether this selector matches a given element.
    /// </summary>
    /// <param name="element">The element to test.</param>
    /// <returns><c>true</c> if the selector matches; otherwise <c>false</c>.</returns>
    public bool Matches(Element element)
    {
        ArgumentNullException.ThrowIfNull(element);

        if (Type is not null && !string.Equals(Type, element.Type, StringComparison.OrdinalIgnoreCase))
            return false;

        if (Id is not null && !string.Equals(Id, element.Id, StringComparison.Ordinal))
            return false;

        if (Class is not null && !element.HasClass(Class))
            return false;

        return true;
    }

    /// <summary>
    /// Parses a selector string into a <see cref="Selector"/> instance.
    /// </summary>
    /// <param name="selectorText">Selector text (e.g. <c>button</c>, <c>.primary</c>, <c>#app</c>, <c>div.menu</c>).</param>
    /// <returns>A parsed <see cref="Selector"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="selectorText"/> is empty or whitespace.</exception>
    /// <remarks>
    /// This parser intentionally supports only a small subset of CSS in the early milestone.
    /// Extend it gradually as you add features.
    /// </remarks>
    public static Selector Parse(string selectorText)
    {
        if (string.IsNullOrWhiteSpace(selectorText))
            throw new ArgumentException("Selector text must be a non-empty string.", nameof(selectorText));

        selectorText = selectorText.Trim();

        string? type = null;
        string? id = null;
        string? cls = null;

        // Support: "div.menu" (type + class)
        var parts = selectorText.Split('.', 2);
        if (parts.Length == 2)
        {
            type = parts[0].Length == 0 ? null : parts[0];
            cls = parts[1];
        }
        else if (selectorText.StartsWith('#'))
        {
            id = selectorText[1..];
        }
        else if (selectorText.StartsWith('.'))
        {
            cls = selectorText[1..];
        }
        else
        {
            type = selectorText;
        }

        if (string.IsNullOrWhiteSpace(type)) type = null;
        if (string.IsNullOrWhiteSpace(id)) id = null;
        if (string.IsNullOrWhiteSpace(cls)) cls = null;

        return new Selector(type, id, cls);
    }
}

namespace TerminalMenuGUI.Core.Styling;

/// <summary>
/// Represents a collection of style rules.
/// </summary>
/// <remarks>
/// A <see cref="StyleSheet"/> is comparable to a CSS stylesheet file containing
/// ordered rules. Rule order matters when specificity is equal.
/// </remarks>
public sealed class StyleSheet
{
    private readonly List<StyleRule> _rules = new();

    /// <summary>
    /// Gets the ordered list of rules in this stylesheet.
    /// </summary>
    public IReadOnlyList<StyleRule> Rules => _rules;

    /// <summary>
    /// Adds a new rule to the stylesheet.
    /// </summary>
    /// <param name="selectorText">Selector text to parse (e.g. <c>button</c>, <c>.primary</c>, <c>#app</c>, <c>div.menu</c>).</param>
    /// <param name="configure">Callback used to configure the rule's <see cref="Style"/> payload.</param>
    /// <returns>The current <see cref="StyleSheet"/> instance, allowing fluent chaining.</returns>
    /// <remarks>
    /// Rules are applied in insertion order. When multiple rules match an element:
    /// <list type="bullet">
    /// <item><description>Higher specificity wins.</description></item>
    /// <item><description>If specificity ties, later rules win (\"last rule wins\").</description></item>
    /// </list>
    /// </remarks>
    public StyleSheet Rule(string selectorText, Action<Style> configure)
    {
        if (configure is null)
            throw new ArgumentNullException(nameof(configure));

        var selector = Selector.Parse(selectorText);
        var style = new Style();
        configure(style);

        _rules.Add(new StyleRule(selector, style));
        return this;
    }
}

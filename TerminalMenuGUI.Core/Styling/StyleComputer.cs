using TerminalMenuGUI.Core.Dom;

namespace TerminalMenuGUI.Core.Styling;

/// <summary>
/// Computes the final (computed) style for each element in a UI tree.
/// </summary>
/// <remarks>
/// This corresponds to the browser step where CSS rules are matched against DOM elements
/// to produce computed styles.
///
/// Initial scope:\n
/// <list type="bullet">
/// <item><description>Only simple selectors (type/class/id, and type+class) are supported.</description></item>
/// <item><description>No inheritance (yet).</description></item>
/// <item><description>No descendant selectors (yet).</description></item>
/// </list>
/// </remarks>
public static class StyleComputer
{
    /// <summary>
    /// Computes a style for each element in the tree rooted at <paramref name="root"/>.
    /// </summary>
    /// <param name="root">The root element of the UI tree.</param>
    /// <param name="sheet">The stylesheet containing the rules to apply.</param>
    /// <returns>
    /// A dictionary mapping each <see cref="Element"/> to its computed <see cref="Style"/>.
    /// </returns>
    public static IReadOnlyDictionary<Element, Style> Compute(Element root, StyleSheet sheet)
    {
        if (root is null)
            throw new ArgumentNullException(nameof(root));
        if (sheet is null)
            throw new ArgumentNullException(nameof(sheet));

        var result = new Dictionary<Element, Style>();
        Visit(root);
        return result;

        void Visit(Element el)
        {
            result[el] = ComputeForElement(el, sheet);

            foreach (var child in el.Children)
            {
                if (child is Element childEl)
                    Visit(childEl);
            }
        }
    }

    /// <summary>
    /// Computes the final style for a single element by applying all matching rules.
    /// </summary>
    /// <param name="element">The element to compute a style for.</param>
    /// <param name="sheet">The stylesheet containing candidate rules.</param>
    /// <returns>The computed style for the element.</returns>
    /// <remarks>
    /// Matching rules are ordered by specificity and then by rule insertion order.
    /// </remarks>
    private static Style ComputeForElement(Element element, StyleSheet sheet)
    {
        var matched = sheet.Rules
            .Where(r => r.Selector.Matches(element))
            .Select((r, idx) => (rule: r, idx))
            .OrderBy(x => x.rule.Selector.Specificity)
            .ThenBy(x => x.idx);

        var computed = new Style();
        foreach (var (rule, _) in matched)
            computed.MergeFrom(rule.Style);

        return computed;
    }
}


namespace TerminalMenuGUI.Core.Dom;

/// <summary>
/// Factory helpers for building UI trees in a concise, HTML-like style.
/// </summary>
/// <remarks>
/// The <see cref="Ui"/> class is intentionally small and opinionated. Its purpose is to make it easy
/// to construct an <see cref="Element"/> tree (a retained-mode UI model) using fluent calls.
///
/// Example:
/// <code>
/// var root = Ui.Div(id: "app", @class: "layout").Add(
///     Ui.Text("Console UI"),
///     Ui.Div(@class: "menu").Add(
///         Ui.Button("Start"),
///         Ui.Button("Settings"),
///         Ui.Button("Quit")
///     )
/// );
/// </code>
/// </remarks>
public static class Ui
{
    /// <summary>
    /// Creates a <c>div</c> element.
    /// </summary>
    /// <param name="id">Optional element id used for <c>#id</c> selectors.</param>
    /// <param name="class">
    /// Optional space-separated class list used for <c>.class</c> selectors
    /// (for example <c>\"menu selected\"</c>).
    /// </param>
    /// <returns>A new <see cref="Element"/> of type <c>div</c>.</returns>
    public static Element Div(string? id = null, string? @class = null) =>
        Element("div", id, @class);
    /// <summary>
    /// Create a <c>button</c> element contining a singel <see cref="TextNode"/> child
    /// </summary>
    /// <param name="text">The visible button label</param>
    /// <param name="id">Optional element id used for <c>#class</c> selectors</param>
    /// <param name="class">
    /// Optional space-separated class list used for <c>.class</c> selectors
    /// (for example <c>"primary"</c> or <c>"selected"</c>).
    /// </param>
    /// <returns>A new <see cref="Element"/> of type <c>button</c>.</returns>
    public static Element Button(string text, string? id = null, string? @class = null) =>
        Element("button", id, @class).Add(new TextNode(text));


    /// <summary>
    /// Creates a text node.
    /// </summary>
    /// <param name="text">The text content.</param>
    /// <returns>A new <see cref="TextNode"/>.</returns>
    public static TextNode Text(string text) => new(text);

    /// <summary>
    /// Creates an element with the specified type and optional id/class attributes
    /// </summary>
    /// <param name="type">The element type (similar to an HTML tag name).</param>
    /// <param name="id">Optional element id used for <c>#id</c> selectors.</param>
    /// <param name="class">
    /// Optional space-separated class list used for <c>.class</c> selectors.
    /// </param>
    /// <returns>A new <see cref="Element"/> instance.</returns>
    /// <remarks>
    /// Class names are split on whitespace. Emptu segments are ignored.
    /// </remarks>
    public static Element Element(string type, string? id = null, string? @class = null)
    {
        var element = new Element(type) { Id = id };

        if (!string.IsNullOrWhiteSpace(@class))
        {
            foreach (var c in @class.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                element.Classes.Add(c);
        }

        return element;
    }
}

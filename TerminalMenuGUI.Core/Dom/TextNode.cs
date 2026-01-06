namespace TerminalMenuGUI.Core.Dom;

/// <summary>
/// Represents a text node in the UI tree.
/// </summary>
/// <remarks>
/// A <see cref="TextNode"/> contains raw text content and does not
/// have child nodes. It is comparable to a text node in the HTML DOM.
/// </remarks>
/// <remarks>
/// Initializes a new <see cref="TextNode"/> with the specified text.
/// </remarks>
/// <param name="text">The text content of the node.</param>
/// <exception cref="ArgumentNullException">
/// Thrown if <paramref name="text"/> is <c>null</c>.
/// </exception>
public sealed class TextNode(string text) : Node
{
    /// <summary>
    /// Gets the text content of this node.
    /// </summary>
    public string Text { get; } = text ?? throw new ArgumentNullException(nameof(text));
}

using System.Collections.ObjectModel;

namespace TerminalMenuGUI.Core.Dom;

/// <summary>
/// Represents an element node in the UI tree.
/// </summary>
/// <remarks>
/// An <see cref="Element"/> is comparable to an HTML element. It has:
/// <list type="bullet">
/// <item><description>A type (e.g. <c>div</c>, <c>button</c>)</description></item>
/// <item><description>An optional <c>id</c></description></item>
/// <item><description>Zero or more CSS-like classes</description></item>
/// <item><description>Child nodes</description></item>
/// </list>
/// </remarks>
public sealed class Element : Node
{
    private readonly List<Node> _children = [];

    /// <summary>
    /// Initializes a new <see cref="Element"/> with the given type.
    /// </summary>
    /// <param name="type">
    /// The element type, similar to an HTML tag name
    /// (for example <c>div</c>, <c>button</c>, or <c>text</c>).
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="type"/> is null or empty.
    /// </exception>
    public Element(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Element type must be a non-empty string.", nameof(type));

        Type = type;
    }

    /// <summary>
    /// Gets the type of this element.
    /// </summary>
    /// <remarks>
    /// This value is used by the styling system to match type selectors
    /// (for example <c>button</c> or <c>div</c>).
    /// </remarks>
    public string Type { get; }

    /// <summary>
    /// Gets or sets the optional identifier of this element.
    /// </summary>
    /// <remarks>
    /// The identifier corresponds to a CSS <c>#id</c> selector.
    /// Identifiers are expected to be unique within a UI tree,
    /// but this is not enforced by the core system.
    /// </remarks>
    public string? Id { get; set; }


    /// <summary>
    /// Gets the set of CSS-like class names applied to this element.
    /// </summary>
    /// <remarks>
    /// Class names are used by the styling system to match
    /// <c>.class</c> selectors. The comparison is case-sensitive
    /// and uses ordinal string comparison.
    /// </remarks>
    public ISet<string> Classes { get; } = new HashSet<string>(StringComparer.Ordinal);

    /// <summary>
    /// Gets the child nodes of this element.
    /// </summary>
    /// <remarks>
    /// The returned collection is read-only. Child nodes must be added
    /// using <see cref="Add(Node[])"/> to ensure parent relationships
    /// are maintained correctly.
    /// </remarks>
    public IReadOnlyList<Node> Children => new ReadOnlyCollection<Node>(_children);


    /// <summary>
    /// Adds one or more child nodes to this element.
    /// </summary>
    /// <param name="nodes">The nodes to add as children.</param>
    /// <returns>
    /// The current <see cref="Element"/>, allowing fluent chaining.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a node already has a parent.
    /// </exception>
    /// <remarks>
    /// Adding a node will automatically set its <see cref="Node.Parent"/>
    /// property. A node may only belong to a single parent element.
    /// </remarks>
    public Element Add(params Node[] nodes)
    {
        foreach (Node node in nodes)
        {
            if (node is Element e && e.Parent is not null)
                throw new InvalidOperationException("Element already has a parent.");

            node.Parent = this;
            _children.Add(node);
        }

        return this;
    }

    /// <summary>
    /// Determines whether this element has the specified class name.
    /// </summary>
    /// <param name="className">The class name to check.</param>
    /// <returns>
    /// <c>true</c> if the element has the specified class;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool HasClass(string className) => Classes.Contains(className);

}

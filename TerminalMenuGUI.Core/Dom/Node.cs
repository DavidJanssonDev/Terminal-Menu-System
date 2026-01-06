namespace TerminalMenuGUI.Core.Dom;

/// <summary>
/// Represents a node in the UI tree.
/// </summary>
/// <remarks>
/// A <see cref="Node"/> is the base type for all items in the retained UI tree.
/// Nodes are owned by an <see cref="Element"/> parent, forming a tree structure
/// similar to the DOM in web development.
/// </remarks>
public abstract class Node
{
    /// <summary>
    /// Gets the parent element of this node, or <c>null</c> if this node
    /// is the root of the tree.
    /// </summary>
    /// <remarks>
    /// The parent is managed internally by the framework when nodes are added
    /// to an <see cref="Element"/> via <see cref="Element.Add(Node[])"/>.
    /// </remarks>
    public Element? Parent { get; internal set; }
}

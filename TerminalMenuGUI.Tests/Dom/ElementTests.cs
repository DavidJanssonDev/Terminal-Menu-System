using TerminalMenuGUI.Core.Dom;
using Xunit;

namespace TerminalMenuGUI.Tests.Dom;



/// <summary>
/// Tests for the <see cref="Element"/> class.
/// </summary>
/// <remarks>
/// These tests verify the core invariants of the UI element tree,
/// including parent-child relationships and structural constraints.
/// </remarks>
public class ElementTests
{
    /// <summary>
    /// Verifies the adding a child element sets its parent reference.
    /// </summary>
    /// <remarks>
    /// A node must always know which element ows it. This relationship 
    /// is required for tree traversal, styling, and layout.
    /// </remarks>
    [Fact]
    public void Add_SetsParent()
    {
        var parent = new Element("div");
        var child = new Element("button");

        parent.Add(child);

        Assert.Same(parent, child.Parent);
        Assert.Single(parent.Children);
    }

    /// <summary>
    /// Verifies that an element cannot be added to more than one parent.
    /// </summary>
    /// <remarks>
    /// Allowing multiple parents would break the tree invariant and lead
    /// to undefined behavior in layout and rendering.
    /// </remarks>
    [Fact]
    public void Add_Throws_WhenElementAlreadyHasParent()
    {
        var parent1 = new Element("div");
        var parent2 = new Element("div");
        var child = new Element("button");

        parent1.Add(child);

        Assert.Throws<InvalidOperationException>(() =>
            parent2.Add(child));
    }

}

using TerminalMenuGUI.Core.Dom;
using TerminalMenuGUI.Core.Styling;
using Xunit;

namespace TerminalMenuGUI.Tests.Styling;

/// <summary>
/// Tests for the <see cref="Selector"/> matching logic.
/// </summary>
/// <remarks>
/// These tests define the supported selector semantics for the initial
/// styling system (type, class, and id selectors).
/// </remarks>
public class SelectorTests
{
    /// <summary>
    /// Verifies that a type selector matches an element of the same type.
    /// </summary>
    [Fact]
    public void Selector_Type_Matches()
    {
        var element = new Element("button");
        var selector = Selector.Parse("button");

        Assert.True(selector.Matches(element));
    }

    /// <summary>
    /// Verifies that a class selector matches an element containing that class.
    /// </summary>
    [Fact]
    public void ClassSelector_MatchesElementClass()
    {
        var element = new Element("button");
        element.Classes.Add("primary");

        var selector = Selector.Parse(".primary");

        Assert.True(selector.Matches(element));
    }

    /// <summary>
    /// Verifies that a combined type and class selector matches correctly.
    /// </summary>
    [Fact]
    public void TypeAndClassSelector_MatchesWhenBothConditionsHold()
    {
        var element = new Element("button");
        element.Classes.Add("selected");

        var selector = Selector.Parse("button.selected");

        Assert.True(selector.Matches(element));
    }

    /// <summary>
    /// Verifies that a selector does not match when required conditions are missing.
    /// </summary>
    [Fact]
    public void Selector_DoseNotMatch_WhenConditionsFail()
    {
        var element = new Element("div");
        var selector = Selector.Parse("button");

        Assert.False(selector.Matches(element));
    }

}

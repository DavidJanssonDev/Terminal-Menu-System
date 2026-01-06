using TerminalMenuGUI.Core.Dom;
using TerminalMenuGUI.Core.Styling;
using Xunit;

namespace TerminalMenuGUI.Tests.Styling;

/// <summary>
/// Tests for <see cref="StyleSheet"/> rule registration and ordering.
/// </summary>
/// <remarks>
/// These tests verify that style rules are stored in insertion order
/// and are available for style computation.
/// </remarks>
public sealed class StyleSheetTests
{
    /// <summary>
    /// Verifies that rules are added to the stylesheet in the order
    /// they are defined.
    /// </summary>
    [Fact]
    public void Rule_AddsRuleInOrder()
    {
        var sheet = new StyleSheet()
            .Rule("button", s => s.Padding = 1)
            .Rule(".primary", s => s.Padding = 2);

        Assert.Equal(2, sheet.Rules.Count);
        Assert.Equal(1, sheet.Rules[0].Style.Padding);
        Assert.Equal(2, sheet.Rules[1].Style.Padding);
    }
}

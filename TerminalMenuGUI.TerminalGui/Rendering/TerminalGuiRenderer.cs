using Terminal.Gui;
using TerminalMenuGUI.Core.Dom;
using TerminalMenuGUI.Core.Styling;
using TerminalMenuGUI.TerminalGui.Mapping;

namespace TerminalMenuGUI.TerminalGui.Rendering;

/// <summary>
/// Renders a Core <see cref="Element"/> tree using Terminal.Gui.
/// </summary>
/// <remarks>
/// This class acts as an adapter between the Core rendering model and the
/// Terminal.Gui view system. It is intentionally minimal and stateless.
/// </remarks>
public sealed class TerminalGuiRenderer
{
    private readonly StyleMapper _styleMapper = new();

    /// <summary>
    /// Initializes the Terminal.Gui application and renders the UI tree.
    /// </summary>
    /// <param name="root">The root element of the UI tree.</param>
    /// <param name="styles">Computed styles for each element.</param>
    public void Run(Element root, IReadOnlyDictionary<Element, Style> styles)
    {
        Application.Init();

        var top = Application.Top;
        top.RemoveAll();

        var rootView = RenderElement(root, styles);
        top.Add(rootView);

        Application.Run();
        Application.Shutdown();
    }

    private View RenderElement(Element element, IReadOnlyDictionary<Element, Style> styles)
    {
        // For now everything is vertical stacked
        var view = new View
        {
            X = 0,
            Y = Pos.(),
            Width = Dim.Fill(),
            Height = Dim.Auto()
        };

        if (styles.TryGetValue(element, out var style))
            _styleMapper.Apply(view, style);

        foreach (var child in element.Children)
        {
            switch (child)
            {
                case TextNode text:
                    view.Add(RenderText(text, style));
                    break;

                case Element el:
                    view.Add(RenderElement(el, styles));
                    break;
            }
        }

        return view;
    }

    private View RenderText(TextNode text, Style? parentStyle)
    {
        var label = new Label(text.Text)
        {
            X = 0,
            Y = Pos.Auto()
        };

        if (parentStyle is not null)
            _styleMapper.Apply(label, parentStyle);

        return label;
    }
}

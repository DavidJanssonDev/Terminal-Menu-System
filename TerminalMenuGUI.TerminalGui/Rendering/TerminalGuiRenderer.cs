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

        var rootView = RenderElement(root, styles).view;
        rootView.X = 0;
        rootView.Y = 0;
        rootView.Width = Dim.Fill();
        rootView.Height = Dim.Fill();

        top.Add(rootView);

        Application.Run();
        Application.Shutdown();
    }

    private (View view, int height) RenderElement(Element element, IReadOnlyDictionary<Element, Style> styles)
    {
        var view = new View
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill()
        };

        styles.TryGetValue(element, out var style);
        if (style is not null)
            _styleMapper.Apply(view, style);

        int y = 0;

        foreach (var child in element.Children)
        {
            switch (child)
            {
                case TextNode text:
                    {
                        RenderTextNode(text, ref view, style, ref y);
                        break;
                    }

                case Element el:
                    {
                        var rendered = RenderElement(el, styles);
                        ElementConfig(ref rendered, ref view, ref y);
                        break;
                    }
            }
        }

        // Make the container tall enough for its children
        view.Height = y <= 0 ? 1 : y;

        return (view, y <= 0 ? 1 : y);
    }

    private void RenderTextNode(TextNode text, ref View view, Style? style, ref int y)
    {
        var label = new Label(text.Text)
        {
            X = 0,
            Y = y,
            Width = Dim.Fill(),
            Height = 1
        };

        if (style is not null)
            _styleMapper.Apply(label, style);

        view.Add(label);
        y += 1;
    }

    private static void ElementConfig(ref (View view, int height) rendered, ref View view, ref int y)
    {
        rendered.view.X = 0;
        rendered.view.Y = y;
        rendered.view.Width = Dim.Fill();
        rendered.view.Height = rendered.height <= 0 ? 1 : rendered.height;

        view.Add(rendered.view);
        y += rendered.view.Height is not null ? rendered.height : rendered.height; // keep it simple
    }
}

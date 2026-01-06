using TerminalMenuGUI.Core;
using TerminalMenuGUI.Core.Dom;
using TerminalMenuGUI.Core.Styling;

var ui =
    Ui.Div(id: "app", @class: "layout").Add(
        Ui.Text("Console UI"),
        Ui.Div(@class: "menu").Add(
            Ui.Button("Start"),
            Ui.Button("Settings", @class: "selected"),
            Ui.Button("Quit")
        )
    );

var css = new StyleSheet()
    .Rule("div.layout", s => s.Padding = 1)
    .Rule("button", s => { s.Fg = ConsoleColor.Black; s.Bg = ConsoleColor.Gray; })
    .Rule("button.selected", s => { s.Bg = ConsoleColor.Green; });


var computed = StyleComputer.Compute(ui, css);

// quick debug output
foreach (var (el, style) in computed)
{
    Console.WriteLine($"{el.Type} id={el.Id} classes=[{string.Join(",", el.Classes)}] bg={style.Bg}");
}

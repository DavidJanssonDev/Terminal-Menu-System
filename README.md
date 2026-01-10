# TerminalMenuGUI

**TerminalMenuGUI** is a small, retained-mode UI framework for building structured, styled terminal interfaces in .NET.

It lets you build terminal UIs using an **HTML + CSS–inspired mental model**, rendered using [`Terminal.Gui`](https://github.com/gui-cs/Terminal.Gui).

This is **not** a collection of ad-hoc menus — it is a tiny UI engine designed for clarity, testability, and separation of concerns.

---

## Features

- 🧱 **Retained-mode UI** – build a UI tree once, let the framework handle rendering
- 🎨 **CSS-like styling** – type, class, and id selectors with deterministic behavior
- 🧠 **Pure Core** – no terminal dependencies, fully unit-testable
- 🖥️ **Terminal.Gui backend** – clean adapter layer, swappable in the future

---

## Installation

Clone the repository and reference the required projects:

```bash
git clone https://github.com/DavidJanssonDev/Terminal-Menu-System.git
```

Reference:
- `TerminalMenuGUI.Core`
- `TerminalMenuGUI.TerminalGui`

---

## Quick Example

```csharp
using TerminalMenuGUI.Core;
using TerminalMenuGUI.Core.Dom;
using TerminalMenuGUI.Core.Styling;
using TerminalMenuGUI.TerminalGui.Rendering;

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

var styles = StyleComputer.Compute(ui, css);

new TerminalGuiRenderer().Run(ui, styles);
```

---

## Core Concepts

### UI Tree (DOM-like)

Your UI is represented as a tree of elements and text nodes.  
This tree is the **single source of truth**.

### Styling

Styles are applied using CSS-inspired selectors:
- `button`
- `.class`
- `#id`
- `div.menu`

Rules are applied by specificity and insertion order.

### Rendering

Rendering is handled by a backend adapter.  
The Core never depends on terminal-specific APIs.

---

## Project Structure

```
TerminalMenuGUI.Core
TerminalMenuGUI.TerminalGui
TerminalMenuGUI.Demo
TerminalMenuGUI.Tests
```

---

## Status

🚧 **Early / experimental**

APIs may evolve as the architecture matures.

---

## License

MIT

# TerminalMenuGUI – Architecture & Mental Model

This document explains **how to think about the system before writing code**.
It describes what kind of system you are building, what subsystems exist, and how they relate.

---

## What you are building

You are building a **mini web engine for the terminal**.

Not just a menu.  
Not just a UI wrapper.

This is a **retained-mode UI framework** inspired by **HTML + CSS**, rendered in a terminal.

Mental mapping:

- **HTML / DOM** → `Element` tree
- **CSS** → `StyleSheet` rules
- **Browser renderer** → Terminal.Gui backend adapter

---

## High-level architecture

```
Application (Demo)
        ↓
Backend Adapter (Terminal.Gui)
        ↓
Core Engine
        ↓
DOM + Style + Layout
```

Each layer should have **one responsibility**.

---

## Core principles

- **Core is pure logic**
  - no Terminal.Gui
  - no console IO
  - fully unit-testable

- **Backend is an adapter**
  - maps Core data → Terminal.Gui Views
  - handles platform and input details

- **UI is retained-mode**
  - you build a tree once
  - the framework owns the tree
  - rendering happens from the tree

---

## Subsystems you are building

### 1) DOM (Element tree)

Your HTML equivalent.

An `Element`:
- has a type (e.g. `div`, `button`, `text`)
- has children
- has attributes (`id`, `class`)
- can have event handlers

Example tree:

```
Div (#app)
 ├── Text("Title")
 └── Div (.menu)
     ├── Button("Start")
     ├── Button("Settings")
     └── Button("Quit")
```

This tree is the **source of truth**.

---

### 2) Styling system (CSS-inspired)

Your CSS equivalent.

Rules look like:

```
button { fg: black; bg: gray }
.primary { bg: green }
#app { padding: 1 }
```

Start simple:
- type / class / id selectors
- last rule wins
- add cascade & specificity later

---

### 3) Layout engine

Answers the question:

> Where does each element go?

Start with:
- vertical stack layout
- padding and spacing
- fixed sizes where needed

Later additions:
- horizontal layout
- alignment
- min / max sizes

---

### 4) Rendering layer

Converts **abstract layout → concrete UI**.

Core produces:
- element
- computed style
- layout box

Backend converts into:
- Terminal.Gui Views
- ColorSchemes
- key bindings

This layer should be replaceable.

---

### 5) Event & interaction system

Makes UI interactive.

Flow:

```
Key press → Backend → Core event → State change → Re-render
```

Menus are **components built on top** of this system.

---

## Project responsibilities

### TerminalMenuGUI.Core

Owns:
- element tree (DOM)
- styling rules & computed styles
- layout calculations
- abstract event model

Must NOT know:
- Terminal.Gui
- Console IO
- OS / platform specifics

---

### TerminalMenuGUI.TerminalGui

Owns:
- rendering to terminal
- mapping styles → colors
- translating key input → Core events

Must NOT:
- contain layout logic
- contain application UI rules

---

### TerminalMenuGUI.Demo

Owns:
- example screens
- usage examples
- manual testing

---

### TerminalMenuGUI.Tests

Owns:
- unit tests for Core logic
- selector / styling tests
- layout tests

---

## How to think while coding

Ask constantly:

> Does this belong in Core or the backend?

- If it can be unit-tested without a terminal → **Core**
- If it draws or reads keys → **Backend**

---

## First milestone

Initial goal:
- vertical menu
- Up / Down navigation
- selected item styled differently

If you can build that, your architecture is sound.

---

## Next step

- implement `Element`
- write tests for it
- grow outward one subsystem at a time

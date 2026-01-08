using System;
using System.Collections.Generic;
using System.Text;

namespace TerminalMenuGUI.Demo.Config;

public sealed class DemoConfig
{
    public DebugConfig Debug { get; init; } = new();
    public string Theme { get; init; } = "Default";
    public TerminalConfig Terminal { get; init; } = new();
}

public sealed class DebugConfig
{
    public bool ShowLayoutBounds { get; init; }
    public bool LogComputedStyles { get; init; }
}

public sealed class TerminalConfig
{
    public bool ForceMonochrome { get; init; }
}
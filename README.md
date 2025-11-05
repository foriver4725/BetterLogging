# BetterLogging

## Description

This library provides a set of wrapper APIs for Unity’s native logging methods — `Debug.Log`, `Debug.LogWarning`, and
`Debug.LogError`.

While Unity’s standard logging is useful for debugging, a common optimization technique is to surround log calls with
`#if` / `#endif` directives so they can be disabled in release builds.  
However, this approach does **not** eliminate the method call itself and therefore still introduces unnecessary
overhead.

**BetterLogging** takes a more efficient approach: all APIs are decorated with the `[Conditional]` attribute, ensuring
that log method calls are **completely omitted** from the compiled release binary.  
This guarantees zero runtime overhead when logging is disabled.

In addition, log messages can be freely color-coded, allowing each development team to customize their own appearance
and improve visual clarity.  
The library also automatically appends information such as the source script, line number, and calling method name below
each log message, providing a quick and readable context for debugging.

## Installation & Setup

Install via Unity Package Manager (UPM):

```
https://github.com/foriver4725/BetterLogging.git?path=Assets/foriver4725/BetterLogging
```

The names of both the assembly and the namespace are `foriver4725.BetterLogging`.

---

To enable the logging features, add the following symbol to **Project Settings → Player → Scripting Define Symbols**:

```
BETTER_LOGGING
```

If this symbol is not defined, the library’s assembly will remain disabled and none of the logging APIs will be included
in the build.

This library’s assembly is **editor-only**, meaning it is always excluded from build data regardless of whether the
scripting define symbol is set.  
In other words, the logging functionality is only available within the Unity Editor and will never be included in player
builds.

## Usage

You can explore all available APIs in
the [sample code here](https://github.com/foriver4725/BetterLogging/blob/main/Assets/foriver4725/BetterLogging/Tests/LoggingTests.cs).

---

If you simply want to print a quick log message, you can do it like this:

```csharp
"Hello, World!".Print();
```

---

If you want to distinguish between different log levels such as normal, warning, or error — which is the most common
case — you can write:

```csharp
"This is a normal message.".Print(LogSettings.Normal);
"This is a warning message.".Print(LogSettings.Warning);
"This is an error message.".Print(LogSettings.Error);
```

`LogSettings.Normal` is equivalent to the version shown above without explicitly specifying a log type.  
In other words, if you omit the log setting, it will automatically use the default **normal log** configuration.

---

`LogSettings` is implemented as a **bit-field enum**, allowing you to combine multiple pieces of information such as log
type and log color (including hue and tone).  
Predefined templates like `LogSettings.Warning` are simply ready-to-use presets, and you can explore
them [here](https://github.com/foriver4725/BetterLogging/blob/main/Assets/foriver4725/BetterLogging/LogSettings.cs).

Depending on your project, you may want to create more fine-grained color variations.  
In that case, you can define your own compile-time constants in the same way as `LogSettings.Warning`, for example:

```csharp
private const LogSettings MyLogSettings
    = LogSettings.Level_Warning | LogSettings.Color_Hue_Cyan | LogSettings.Color_Tone_Light;

// ... (omitted)

"This is a custom colored message.".Print(MyLogSettings);
```

---

Finally, for maximum flexibility, you can directly specify the log color yourself:

```csharp
"This is a custom colored message with override color.".Print(LogSettings.Level_Normal, Color.magenta);
```

This is the core API of the library — all other logging methods internally delegate to this one.

## Dummy Class (for Global Using)

This section applies only if your project supports **C# Global Using**.

If you want to explicitly prevent the use of `UnityEngine.Debug`, it is recommended to define a global using directive
as follows:

```csharp
global using Debug = foriver4725.BetterLogging.Dummy;
```

With this setup, any attempt to use `Debug.Log`, `Debug.LogWarning`, or `Debug.LogError` will result in a compile-time
error.  
This encourages team members to use the unified logging API provided by this library instead.

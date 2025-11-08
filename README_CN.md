# BetterLogging

## 概述

本库提供了一组包装 API，用于替代 Unity 原生的日志方法  
（`Debug.Log`、`Debug.LogWarning`、`Debug.LogError`）。  

虽然 Unity 的标准日志输出对调试非常有用，  
但常见的做法是使用 `#if` / `#endif` 包裹日志语句以在发布版本中禁用它们。  
然而，这种方式仍然会保留函数调用本身，从而产生**不必要的性能开销**。  

**BetterLogging** 采用了更高效的方式：  
通过 `[Conditional]` 特性在编译时直接移除日志调用，  
确保在禁用日志时 **完全零运行时开销（Zero Overhead）**。  

此外，日志输出可以自由设置颜色，  
团队可以根据需求自定义外观以提高可读性。  
每条日志还会自动附加脚本名、行号和调用方法等信息，  
方便快速定位问题。

---

## 安装与配置

通过 Unity Package Manager（UPM）添加以下路径安装：

```
https://github.com/foriver4725/BetterLogging.git?path=Assets/foriver4725/BetterLogging
```

程序集与命名空间名称均为 `foriver4725.BetterLogging`。

---

### 启用日志功能

在 **Project Settings → Player → Scripting Define Symbols** 中添加以下宏定义：

```
BETTER_LOGGING
```

BetterLogging 的启用与否完全取决于此宏是否被定义。  

例如，在 Unity 6 及更高版本中，你可以通过 **Build Profiles** 功能  
仅在 Debug 构建中定义该宏，而在 Release 构建中禁用它。  
这样即可在正式版本中完全排除日志功能，同时保留调试环境下的输出。

---

## 使用方法

完整 API 示例可在  
[示例代码](https://github.com/foriver4725/BetterLogging/blob/main/Assets/foriver4725/BetterLogging/Tests/LoggingTests.cs) 中查看。

---

### 快速输出日志

若仅需简单输出一条消息，可直接编写如下代码：

```csharp
"Hello, World!".Print();
```

---

### 区分日志级别

若需区分普通、警告、错误等不同级别，可使用：

```csharp
"This is a normal message.".Print(LogSettings.Normal);
"This is a warning message.".Print(LogSettings.Warning);
"This is an error message.".Print(LogSettings.Error);
```

`LogSettings.Normal` 等价于未指定时的默认设置，  
即省略参数时会自动使用普通日志。

---

### 日志颜色与自定义

`LogSettings` 是一个 **位字段枚举（bit-field enum）**，  
可组合日志类型、色相、亮度等信息。  

例如，`LogSettings.Warning` 是一个预设模板，  
其定义可参考 [LogSettings.cs](https://github.com/foriver4725/BetterLogging/blob/main/Assets/foriver4725/BetterLogging/LogSettings.cs)。  

若需创建更细致的颜色变化，可自定义常量如下：

```csharp
private const LogSettings MyLogSettings
    = LogSettings.Level_Warning | LogSettings.Color_Hue_Cyan | LogSettings.Color_Tone_Light;

// ...

"This is a custom colored message.".Print(MyLogSettings);
```

---

### 自定义颜色指定

若想手动指定颜色，可直接传入 `Color` 参数：

```csharp
"This is a custom colored message with override color.".Print(LogSettings.Level_Normal, Color.magenta);
```

此方法是整个库的核心 API，  
其他所有日志方法都基于此实现。

---

## Dummy 类（Global Using 支持）

若项目启用了 **C# Global Using**，  
并希望禁止直接使用 `UnityEngine.Debug`，  
可在全局导入中加入以下定义：

```csharp
global using Debug = foriver4725.BetterLogging.Dummy;
```

此设置会使 `Debug.Log`、`Debug.LogWarning`、`Debug.LogError`  
在编译阶段直接报错，  
从而强制团队成员统一使用 BetterLogging 的日志接口。

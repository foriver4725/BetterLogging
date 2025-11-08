# BetterLogging

## 概要

このライブラリは、Unity標準のログ出力メソッド（`Debug.Log`、`Debug.LogWarning`、`Debug.LogError`）を  
より効率的に扱うための**ラッパAPI群**を提供します。  

Unityの標準ログは便利ですが、リリースビルドで無効化するために  
`#if` / `#endif` で囲む手法には依然として関数呼び出しのオーバーヘッドが残ります。  

**BetterLogging** は、`[Conditional]` 属性を用いることで、  
**コンパイル時に不要なログ呼び出し自体を完全に削除**します。  
これにより、ログ無効時でも**実行時オーバーヘッドがゼロ**になります。  

さらに、ログの**カラー設定**（色分け）が可能で、  
チームごとにわかりやすい出力形式を自由にカスタマイズできます。  
出力ログには、スクリプト名・行番号・呼び出し元メソッドなどの情報も自動的に付加されます。  

---

## インストール方法

Unity Package Manager（UPM）から以下のURLを追加します：

```
https://github.com/foriver4725/BetterLogging.git?path=Assets/foriver4725/BetterLogging
```

アセンブリ名と名前空間はいずれも `foriver4725.BetterLogging` です。  

---

### ログ機能の有効化

次のスクリプト定義シンボルを **Project Settings → Player → Scripting Define Symbols** に追加します：

```
BETTER_LOGGING
```

このシンボルが設定されているかどうかによって、  
BetterLoggingの有効・無効が切り替わります。  

たとえば、Unity 6以降で追加された **Build Profiles** 機能を使えば、  
Debugビルド時のみこのシンボルを定義し、  
Releaseビルドでは定義を外すことで、  
**本番ビルドから完全にログ処理を除外**することができます。

---

## 使い方

すべてのAPIは[こちらのサンプルコード](https://github.com/foriver4725/BetterLogging/blob/main/Assets/foriver4725/BetterLogging/Tests/LoggingTests.cs)で確認できます。

---

### シンプルなログ出力

最も簡単な例として、文字列を直接ログ出力できます：

```csharp
"Hello, World!".Print();
```

---

### ログレベルの指定

通常・警告・エラーなど、ログの種類を区別したい場合は次のように記述します：

```csharp
"This is a normal message.".Print(LogSettings.Normal);
"This is a warning message.".Print(LogSettings.Warning);
"This is an error message.".Print(LogSettings.Error);
```

`LogSettings.Normal` は、引数を省略したときのデフォルト設定に相当します。  
つまり、指定を省略すると自動的に「通常ログ」として扱われます。

---

### カラー設定・カスタマイズ

`LogSettings` は **ビットフィールド列挙型（bit-field enum）** で実装されており、  
ログ種別や色調（色相・明度など）を組み合わせて指定できます。  

`LogSettings.Warning` などのプリセットは単なる定義済みテンプレートで、  
詳細は[LogSettings.cs](https://github.com/foriver4725/BetterLogging/blob/main/Assets/foriver4725/BetterLogging/LogSettings.cs)を参照してください。  

独自の色バリエーションを追加したい場合は、  
以下のように定数を定義すれば拡張可能です：

```csharp
private const LogSettings MyLogSettings
    = LogSettings.Level_Warning | LogSettings.Color_Hue_Cyan | LogSettings.Color_Tone_Light;

// ...

"This is a custom colored message.".Print(MyLogSettings);
```

---

### 任意カラーの直接指定

より柔軟に色を指定したい場合は、以下のように `Color` を直接渡すこともできます：

```csharp
"This is a custom colored message with override color.".Print(LogSettings.Level_Normal, Color.magenta);
```

このAPIがBetterLoggingの中核であり、  
他のすべてのログ出力メソッドはこの処理に委譲されています。

---

## ダミークラス（Global Using対応）

C#の **Global Using** を使用している場合、  
プロジェクト内で `UnityEngine.Debug` の使用を防止したいときは、  
以下のように別名定義を追加することを推奨します：

```csharp
global using Debug = foriver4725.BetterLogging.Dummy;
```

これにより、`Debug.Log` や `Debug.LogWarning` などを直接呼び出すと  
**コンパイルエラー** になります。  
チーム全体でBetterLoggingのAPIを統一的に利用する助けとなります。

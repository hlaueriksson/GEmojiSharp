# GEmojiSharp :octocat:<!-- omit in toc -->

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild)
[![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

[![GEmojiSharp](https://img.shields.io/nuget/v/GEmojiSharp.svg?label=GEmojiSharp)](https://www.nuget.org/packages/GEmojiSharp)
[![GEmojiSharp.AspNetCore](https://img.shields.io/nuget/v/GEmojiSharp.AspNetCore.svg?label=GEmojiSharp.AspNetCore)](https://www.nuget.org/packages/GEmojiSharp.AspNetCore)
[![GEmojiSharp.Blazor](https://img.shields.io/nuget/v/GEmojiSharp.Blazor.svg?label=GEmojiSharp.Blazor)](https://www.nuget.org/packages/GEmojiSharp.Blazor)
[![GEmojiSharp.DotnetTool](https://img.shields.io/nuget/v/GEmojiSharp.DotnetTool.svg?label=GEmojiSharp.DotnetTool)](https://www.nuget.org/packages/GEmojiSharp.DotnetTool)
[![GEmojiSharp.McpServer](https://img.shields.io/nuget/v/GEmojiSharp.McpServer.svg?label=GEmojiSharp.McpServer)](https://www.nuget.org/packages/GEmojiSharp.McpServer)

> GitHub Emoji for C# and .NET:
>
> - `netstandard2.0`
> - ASP.NET Core
> - Blazor
> - `dotnet` tool
> - PowerToys Run plugin
> - PowerToys Command Palette extension
> - MCP Server

```txt
🐙 :octopus:
➕ :heavy_plus_sign:
🐈 :cat2:
⩵
❤️ :heart:
```

## Content<!-- omit in toc -->

- [Introduction](#introduction)
- [`GEmojiSharp`](#gemojisharp)
- [`GEmojiSharp.AspNetCore`](#gemojisharpaspnetcore)
- [`GEmojiSharp.Blazor`](#gemojisharpblazor)
- [`GEmojiSharp.DotnetTool`](#gemojisharpdotnettool)
- [`GEmojiSharp.PowerToysRun`](#gemojisharppowertoysrun)
- [`GEmojiSharp.McpServer`](#gemojisharpmcpserver)
- [Samples](#samples)
- [Attribution](#attribution)

## Introduction

[Using emojis](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax#using-emojis) on GitHub is accomplish with emoji aliases enclosed by colons:

`:+1: This PR looks great - it's ready to merge! :shipit:`

:+1: This PR looks great - it's ready to merge! :shipit:

`GEmojiSharp` make this possible in C#. The library contains a static array of all valid emoji in GitHub Flavored Markdown.
That is the intersection of the [emoji.json](https://raw.githubusercontent.com/github/gemoji/master/db/emoji.json) database and the API with [available emojis](https://api.github.com/emojis).

A visual referense of all GitHub Emoji:

- https://github.com/hlaueriksson/github-emoji

## `GEmojiSharp`

[![NuGet](https://img.shields.io/nuget/dt/GEmojiSharp)](https://www.nuget.org/packages/GEmojiSharp/)

> GitHub Emoji for C# and .NET 📦

Static methods:

```csharp
Emoji.Get(":tada:").Raw; // 🎉
Emoji.Get("🎉").Alias(); // :tada:
Emoji.Raw(":tada:"); // 🎉
Emoji.Alias("🎉"); // :tada:
Emoji.Emojify(":tada: initial commit"); // 🎉 initial commit
Emoji.Demojify("🎉 initial commit"); // :tada: initial commit
Emoji.Find("party popper").First().Raw; // 🎉
Emoji.Get("✌️").RawSkinToneVariants(); // ✌🏻, ✌🏼, ✌🏽, ✌🏾, ✌🏿
```

Extension methods:

```csharp
":tada:".GetEmoji().Raw; // 🎉
"🎉".GetEmoji().Alias(); // :tada:
":tada:".RawEmoji(); // 🎉
"🎉".EmojiAlias(); // :tada:
":tada: initial commit".Emojify(); // 🎉 initial commit
"🎉 initial commit".Demojify(); // :tada: initial commit
"party popper".FindEmojis().First().Raw; // 🎉
```

Regular expression pattern to match all emojis:

```csharp
var text = "Lorem 😂😂 ipsum";

var matches = Regex.Matches(text, Emoji.RegexPattern);
string.Join(string.Empty, matches.Select(x => x.Value)); // 😂😂

Regex.Replace(text, Emoji.RegexPattern, string.Empty); // Lorem  ipsum
```

## `GEmojiSharp.AspNetCore`

[![NuGet](https://img.shields.io/nuget/dt/GEmojiSharp.AspNetCore)](https://www.nuget.org/packages/GEmojiSharp.AspNetCore/)

> GitHub Emoji for ASP.NET Core 📦

The package includes:

- TagHelpers
- HtmlHelpers

### TagHelpers<!-- omit in toc -->

Update the `_ViewImports.cshtml` file, to enable tag helpers in all Razor views:

```cshtml
@addTagHelper *, GEmojiSharp.AspNetCore
```

Use the `<emoji>` tag or `emoji` attribute to render emojis:

```html
<span emoji=":tada:"></span>
<emoji>:tada: initial commit</emoji>
```

Standard emoji characters are rendered like this:

```html
<g-emoji class="g-emoji" alias="tada" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f389.png">🎉</g-emoji>
```

Custom GitHub emojis are rendered as images:

```html
<img class="emoji" title=":octocat:" alt=":octocat:" src="https://github.githubassets.com/images/icons/emoji/octocat.png" height="20" width="20" align="absmiddle">
```

Use CSS to properly position the custom GitHub emojis images:

```css
.emoji {
    background-color: transparent;
    max-width: none;
    vertical-align: text-top;
}
```

Use the JavaScript from [`g-emoji-element`](https://github.com/github/g-emoji-element) to support old browsers.

> Backports native emoji characters to browsers that don't support them by replacing the characters with fallback images.

Add a [`libman.json`](https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-6.0) file:

```json
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  "libraries": [
    {
      "provider": "unpkg",
      "library": "@github/g-emoji-element@1.2.0",
      "destination": "wwwroot/lib/g-emoji-element/"
    }
  ]
}
```

And add the script to the `_Layout.cshtml` file:

```html
<script src="~/lib/g-emoji-element/dist/index.js"></script>
```

Do you want to use emoji anywhere, on any tag, in the `body`? Then you can use the `BodyTagHelperComponent`.

Use any tag to render emojis:

```html
<h1>Hello, :earth_africa:</h1>
```

[Registration](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/th-components?view=aspnetcore-6.0#registration-via-services-container) via services container:

```cs
using GEmojiSharp.AspNetCore;
using Microsoft.AspNetCore.Razor.TagHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddTransient<ITagHelperComponent, BodyTagHelperComponent>();
```

[Registration](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/th-components?view=aspnetcore-6.0#registration-via-razor-file) via Razor file:

```cshtml
@page
@model GEmojiSharp.Sample.Web.Pages.ComponentModel
@using Microsoft.AspNetCore.Mvc.Razor.TagHelpers
@using GEmojiSharp.AspNetCore
@inject ITagHelperComponentManager manager;
@{
    ViewData["Title"] = "Component";
    manager.Components.Add(new BodyTagHelperComponent());
}
```

[Registration](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/th-components?view=aspnetcore-6.0#registration-via-page-model-or-controller) via Page Model or controller:

```cs
using GEmojiSharp.AspNetCore;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GEmojiSharp.Sample.Web.Pages
{
    public class ComponentModel : PageModel
    {
        private readonly ITagHelperComponentManager _tagHelperComponentManager;

        public IndexModel(ITagHelperComponentManager tagHelperComponentManager)
        {
            _tagHelperComponentManager = tagHelperComponentManager;
        }

        public void OnGet()
        {
            _tagHelperComponentManager.Components.Add(new BodyTagHelperComponent());
        }
    }
}
```

### HtmlHelpers<!-- omit in toc -->

Update the `_ViewImports.cshtml` file, to enable HTML helpers in all Razor views:

```cshtml
@using GEmojiSharp.AspNetCore
```

Use the `Emoji` extension methods to render emojis:

```cshtml
@Html.Emoji(":tada: initial commit")
@Html.Emoji(x => x.Text)
```

## `GEmojiSharp.Blazor`

[![NuGet](https://img.shields.io/nuget/dt/GEmojiSharp.Blazor)](https://www.nuget.org/packages/GEmojiSharp.Blazor/)

> GitHub Emoji for Blazor 📦

The package is a Razor class library (RCL) with a Razor component.

Update the `_Imports.razor` file, to enable the component in all Razor views:

```cshtml
@using GEmojiSharp.Blazor
```

> [!NOTE]
> In a Blazor Web App (.NET 8 or later), the component requires an interactive render mode applied either globally to the app or to the component definition.

Set the global render mode in `App.razor`:

```cshtml
<Routes @rendermode="InteractiveServer" />
```

or per page/component:

```cshtml
@rendermode InteractiveServer
```

Use the `<Emoji>` component to render emojis:

```html
<Emoji>:tada: initial commit</Emoji>
```

Standard emoji characters are rendered like this:

```html
<g-emoji class="g-emoji" alias="tada" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f389.png">🎉</g-emoji>
```

Custom GitHub emojis are rendered as images:

```html
<img class="emoji" title=":octocat:" alt=":octocat:" src="https://github.githubassets.com/images/icons/emoji/octocat.png" height="20" width="20" align="absmiddle">
```

## `GEmojiSharp.DotnetTool`

[![NuGet](https://img.shields.io/nuget/dt/GEmojiSharp.DotnetTool)](https://www.nuget.org/packages/GEmojiSharp.DotnetTool/)

> GitHub Emoji `dotnet` tool 🧰

![GEmojiSharp.DotnetTool](GEmojiSharp.DotnetTool.gif)

### Installation<!-- omit in toc -->

Install:

```cmd
dotnet tool install -g GEmojiSharp.DotnetTool
```

Update:

```cmd
dotnet tool update -g GEmojiSharp.DotnetTool
```

Uninstall:

```cmd
dotnet tool uninstall -g GEmojiSharp.DotnetTool
```

Enable emoji in the terminal:

- Open Settings / Time & Language / Language / Administrative Language Settings / Change system locale...
- Check "Beta: Use Unicode UTF-8 for worldwide language support" and click OK
- Reboot the PC for the change to take effect

![Beta: Use Unicode UTF-8 for worldwide language support](Unicode.png)

- [Set a process code page to UTF-8](https://learn.microsoft.com/en-us/windows/apps/design/globalizing/use-utf8-code-page#set-a-process-code-page-to-utf-8)

### Usage<!-- omit in toc -->

```cmd
emoji --help
```

```cmd
Description:
  GitHub Emoji dotnet tool

Usage:
  emoji [command] [options]

Options:
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  r, raw <args>       Get raw emojis
  a, alias <args>     Get emoji aliases
  e, emojify <args>   Replace aliases in text with raw emojis
  d, demojify <args>  Replace raw emojis in text with aliases
  export <args>       Export emoji data to <json|toml|xml|yaml>
```

#### Raw<!-- omit in toc -->

```cmd
emoji raw --help
```

```cmd
Description:
  Get raw emojis

Usage:
  emoji raw [<args>...] [options]

Arguments:
  <args>  Find emojis via description, category, alias or tag

Options:
  -st, --skin-tones  Include skin tone variants
  -c, --copy         Copy to clipboard
  -?, -h, --help     Show help and usage information
```

<details>
<summary>Examples 💁</summary>

Get raw emojis:

```cmd
emoji raw "grinning cat"
emoji raw grinning cat
emoji r grinning cat
```

```cmd
😺
😸
```

Copy to clipboard:

```cmd
emoji raw "grinning cat" --copy
emoji r grinning cat -c
```

```txt
😺😸
```

Skin tone variants:

```cmd
emoji raw "victory" --skin-tones
emoji r victory -st
```

```txt
✌️
✌🏻
✌🏼
✌🏽
✌🏾
✌🏿
```

</details>

#### Alias<!-- omit in toc -->

```cmd
emoji alias --help
```

```cmd
Description:
  Get emoji aliases

Usage:
  emoji alias [<args>...] [options]

Arguments:
  <args>  Find emojis via description, category, alias or tag

Options:
  -c, --copy      Copy to clipboard
  -?, -h, --help  Show help and usage information
```

<details>
<summary>Examples 💁</summary>

Get emoji aliases:

```cmd
emoji alias "grinning cat"
emoji alias grinning cat
emoji a grinning cat
```

```cmd
:smiley_cat:
:smile_cat:
```

Copy to clipboard:

```cmd
emoji alias "grinning cat" --copy
emoji a grinning cat -c
```

```txt
:smiley_cat::smile_cat:
```

</details>

#### Emojify<!-- omit in toc -->

```cmd
emoji emojify --help
```

```cmd
Description:
  Replace aliases in text with raw emojis

Usage:
  emoji emojify [<args>...] [options]

Arguments:
  <args>  A text with emoji aliases

Options:
  -c, --copy      Copy to clipboard
  -?, -h, --help  Show help and usage information
```

<details>
<summary>Examples 💁</summary>

Replace aliases in text with raw emojis:

```cmd
emoji emojify ":tada: initial commit"
emoji emojify :tada: initial commit
emoji e :tada: initial commit
```

```cmd
🎉 initial commit
```

Copy to clipboard:

```cmd
emoji emojify ":tada: initial commit" --copy
emoji e :tada: initial commit -c
```

</details>

#### Demojify<!-- omit in toc -->

```cmd
emoji demojify --help
```

```cmd
Description:
  Replace raw emojis in text with aliases

Usage:
  emoji demojify [<args>...] [options]

Arguments:
  <args>  A text with raw emojis

Options:
  -c, --copy      Copy to clipboard
  -?, -h, --help  Show help and usage information
```

<details>
<summary>Examples 💁</summary>

Replace raw emojis in text with aliases:

```cmd
emoji demojify "🎉 initial commit"
emoji demojify 🎉 initial commit
emoji d 🎉 initial commit
```

```cmd
:tada: initial commit
```

Copy to clipboard:

```cmd
emoji demojify "🎉 initial commit" --copy
emoji d 🎉 initial commit -c
```

</details>

#### Export<!-- omit in toc -->

```cmd
emoji export --help
```

```cmd
Description:
  Export emoji data to <json|toml|xml|yaml>

Usage:
  emoji export [<args>...] [options]

Arguments:
  <args>  Find emojis via description, category, alias or tag

Options:
  -f, --format <format>  Format the data as <json|toml|xml|yaml>
  -c, --copy             Copy to clipboard
  -?, -h, --help         Show help and usage information
```

Formats:

- `json`
- `toml`
- `xml`
- `yaml`

<details>
<summary>Examples 💁</summary>

Export emoji data to `json`:

```cmd
emoji export "grinning cat" --format json
emoji export grinning cat --format json
emoji export grinning cat -f json
emoji export grinning cat
```

```json
[
  {
    "Raw": "😺",
    "Description": "grinning cat",
    "Category": "Smileys & Emotion",
    "Aliases": [
      "smiley_cat"
    ],
    "Tags": null,
    "UnicodeVersion": "6.0",
    "IosVersion": "6.0",
    "Filename": "1f63a",
    "IsCustom": false
  },
  {
    "Raw": "😸",
    "Description": "grinning cat with smiling eyes",
    "Category": "Smileys & Emotion",
    "Aliases": [
      "smile_cat"
    ],
    "Tags": null,
    "UnicodeVersion": "6.0",
    "IosVersion": "6.0",
    "Filename": "1f638",
    "IsCustom": false
  }
]
```

Copy to clipboard:

```cmd
emoji export "grinning cat" --format json --copy
emoji export "grinning cat" -c
```

</details>

## `GEmojiSharp.PowerToysRun`

[![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/hlaueriksson/GEmojiSharp/total)](https://github.com/hlaueriksson/GEmojiSharp/releases/latest)
[![Mentioned in Awesome PowerToys Run Plugins](https://awesome.re/mentioned-badge.svg)](https://github.com/hlaueriksson/awesome-powertoys-run-plugins)

> GitHub Emoji [PowerToys Run](https://docs.microsoft.com/en-us/windows/powertoys/run) plugin 🗂️🔎🔌

![GEmojiSharp.PowerToysRun](GEmojiSharp.PowerToysRun.gif)

### Installation<!-- omit in toc -->

The plugin is developed and tested with `PowerToys` `v0.83.0`.

Install:

0. [Install PowerToys](https://docs.microsoft.com/en-us/windows/powertoys/install)
1. Exit PowerToys
2. Download the `.zip` file from the latest [release](https://github.com/hlaueriksson/GEmojiSharp/releases/latest) and extract it to:
   - `%LocalAppData%\Microsoft\PowerToys\PowerToys Run\Plugins`
3. Start PowerToys

![GEmojiSharp.PowerToysRun](GEmojiSharp.PowerToysRun.png)

### Usage<!-- omit in toc -->

1. Open PowerToys Run with `alt + space`
2. Type `emoji`
   - A list of all emojis will be displayed
3. Continue to type to find emojis via description, category, alias or tag
4. Use ⬆️ and ⬇️ keys to select an emoji
5. Press `Enter` to copy the selected raw emoji to clipboard
6. Press `ctrl + c` to copy the selected emoji aliases to clipboard
7. Press `ctrl + Enter` to copy the selected raw emoji skin tone variants to clipboard
   - For emoji that supports skin tone modifiers

Emojify:

- You can paste a text containing emoji aliases to replace them with raw emojis

Demojify:

- You can paste a text containing raw emojis to replace them with aliases

### Configuration<!-- omit in toc -->

Change action keyword:

1. Open PowerToys
2. Select PowerToys Run
3. Scroll down to Plugins
4. Expand `GEmojiSharp`
5. Change *Direct activation command*

![GEmojiSharp.PowerToysRun](GEmojiSharp.PowerToysRun-Configuration.png)

## `GEmojiSharp.McpServer`

[![NuGet](https://img.shields.io/nuget/dt/GEmojiSharp.McpServer)](https://www.nuget.org/packages/GEmojiSharp.McpServer/)

> GitHub Emoji MCP Server 🤖

### all<!-- omit in toc -->

> Returns all emojis.

![GEmojiSharp.McpServer - all](GEmojiSharp.McpServer-all.png)

### get<!-- omit in toc -->

> Gets the emoji associated with the alias or raw Unicode string.

![GEmojiSharp.McpServer - get](GEmojiSharp.McpServer-get.png)

### find<!-- omit in toc -->

> Returns emojis that match the Description, Category, Aliases or Tags.

![GEmojiSharp.McpServer - find](GEmojiSharp.McpServer-find.png)

### emojify<!-- omit in toc -->

> Replaces emoji aliases with raw Unicode strings.

![GEmojiSharp.McpServer - emojify](GEmojiSharp.McpServer-emojify.png)

### demojify<!-- omit in toc -->

> Replaces raw Unicode strings with emoji aliases.

![GEmojiSharp.McpServer - demojify](GEmojiSharp.McpServer-demojify.png)

## Samples

The [`samples`](/samples) folder contains...

- `GEmojiSharp.Sample.BlazorWeb`, a Blazor Web App (InteractiveServer render mode)
- `GEmojiSharp.Sample.BlazorWebAssembly`, a Blazor WebAssembly App
- `GEmojiSharp.Sample.Web`, a ASP.NET Core Web App (Razor Pages)

The Blazor WebAssembly app is showcased here:

- https://hlaueriksson.github.io/GEmojiSharp/

[![GEmojiSharp.Sample.BlazorWebAssembly](GEmojiSharp.Sample.BlazorWebAssembly.png)](https://hlaueriksson.github.io/GEmojiSharp/)

## Attribution

Repositories consulted when building this:

- https://github.com/github/gemoji
- https://github.com/github/g-emoji-element
- https://github.com/dotnet/command-line-api
- https://github.com/microsoft/PowerToys

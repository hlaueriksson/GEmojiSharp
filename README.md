# GEmojiSharp<!-- omit in toc -->

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild)
[![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

[![GEmojiSharp](https://img.shields.io/nuget/v/GEmojiSharp.svg?label=GEmojiSharp)](https://www.nuget.org/packages/GEmojiSharp)
[![GEmojiSharp.AspNetCore](https://img.shields.io/nuget/v/GEmojiSharp.AspNetCore.svg?label=GEmojiSharp.AspNetCore)](https://www.nuget.org/packages/GEmojiSharp.AspNetCore)
[![GEmojiSharp.Blazor](https://img.shields.io/nuget/v/GEmojiSharp.Blazor.svg?label=GEmojiSharp.Blazor)](https://www.nuget.org/packages/GEmojiSharp.Blazor)

> GitHub Emoji for C#, ASP.NET Core and Blazor

```txt
🐙 :octopus:
➕ :heavy_plus_sign:
🐈 :cat2:
⩵
❤️ :heart:
```

## Content<!-- omit in toc -->

- [Introduction](#introduction)
- [`GEmojiSharp` 📦](#gemojisharp-)
- [`GEmojiSharp.AspNetCore` 📦](#gemojisharpaspnetcore-)
  - [TagHelpers](#taghelpers)
  - [HtmlHelpers](#htmlhelpers)
- [`GEmojiSharp.Blazor` 📦](#gemojisharpblazor-)
- [Samples](#samples)
- [Upgrading](#upgrading)
- [Attribution](#attribution)

## Introduction

[Using emoji](https://help.github.com/en/articles/basic-writing-and-formatting-syntax#using-emoji) on GitHub is accomplish with emoji aliases enclosed by colons:

`:+1: This PR looks great - it's ready to merge! :shipit:`

:+1: This PR looks great - it's ready to merge! :shipit:

`GEmojiSharp`, `GEmojiSharp.AspNetCore` and `GEmojiSharp.Blazor` are three libraries to make this possible in C#, Blazor and ASP.NET Core.

A list of all GitHub Emojis:

- https://github.com/hlaueriksson/github-emoji

## `GEmojiSharp` 📦

[![NuGet](https://buildstats.info/nuget/GEmojiSharp)](https://www.nuget.org/packages/GEmojiSharp/)

> GitHub Emoji for C# and .NET

Static methods:

```csharp
Emoji.Get(":tada:").Raw; // 🎉
Emoji.Get("🎉").Alias(); // :tada:
Emoji.Raw(":tada:"); // 🎉
Emoji.Alias("🎉"); // :tada:
Emoji.Emojify(":tada: initial commit"); // 🎉 initial commit
Emoji.Demojify("🎉 initial commit"); // :tada: initial commit
Emoji.Find("party popper").First().Raw; // 🎉
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

## `GEmojiSharp.AspNetCore` 📦

[![NuGet](https://buildstats.info/nuget/GEmojiSharp.AspNetCore)](https://www.nuget.org/packages/GEmojiSharp.AspNetCore/)

> GitHub Emoji for ASP.NET Core

The package includes:

- TagHelpers
- HtmlHelpers

### TagHelpers

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
      "library": "@github/g-emoji-element@1.1.5",
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

### HtmlHelpers

Update the `_ViewImports.cshtml` file, to enable HTML helpers in all Razor views:

```cshtml
@using GEmojiSharp.AspNetCore
```

Use the `Emoji` extension methods to render emojis:

```cshtml
@Html.Emoji(":tada: initial commit")
@Html.Emoji(x => x.Text)
```

## `GEmojiSharp.Blazor` 📦

[![NuGet](https://buildstats.info/nuget/GEmojiSharp.Blazor)](https://www.nuget.org/packages/GEmojiSharp.Blazor/)

> GitHub Emoji for Blazor

The package is a Razor class library (RCL) with a Razor component.

Update the `_Imports.razor` file, to enable the component in all Razor views:

```cshtml
@using GEmojiSharp.Blazor
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

## Samples

The [`samples`](/samples) folder contains...

- `GEmojiSharp.Sample.BlazorServer`, a Blazor Server app
- `GEmojiSharp.Sample.BlazorWebAssembly`, a Blazor WebAssembly app
- `GEmojiSharp.Sample.Web`, a ASP.NET Core web site

The Blazor WebAssembly app is showcased here:

- https://hlaueriksson.github.io/GEmojiSharp/ (GitHub Pages)
- https://purple-mushroom-05c6bad10.azurestaticapps.net (Azure Static Web Apps)

[![GEmojiSharp.Sample.BlazorWebAssembly](GEmojiSharp.Sample.BlazorWebAssembly.png)](https://hlaueriksson.github.io/GEmojiSharp/)

## Upgrading

> ⬆️ Upgrading from version `1.5.0` to `2.0.0`

Upgrade `GEmojiSharp.TagHelpers`:

- Uninstall `GEmojiSharp.TagHelpers`
- Install `GEmojiSharp.AspNetCore`
- Replace references to ~~`GEmojiSharp.TagHelpers`~~ with `GEmojiSharp.AspNetCore`:

  ```diff
  - @addTagHelper *, GEmojiSharp.TagHelpers
  + @addTagHelper *, GEmojiSharp.AspNetCore
  ```

  ```diff
  - @using GEmojiSharp.TagHelpers
  + @using GEmojiSharp.AspNetCore
  ```

  ```diff
  - using GEmojiSharp.TagHelpers;
  + using GEmojiSharp.AspNetCore;
  ```

Upgrade `GEmojiSharp.Blazor`:

- Remove reference to the `css` file:

  ```diff
  - <link href="_content/GEmojiSharp.Blazor/style.css" rel="stylesheet" />
  ```

- Remove reference to the `js` file:

  ```diff
  - <script src="_content/GEmojiSharp.Blazor/script.js"></script>
  ```

## Attribution

Repositories consulted when building this:

- https://github.com/github/gemoji
- https://github.com/github/g-emoji-element

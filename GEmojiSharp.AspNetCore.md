# GEmojiSharp.AspNetCore 📦

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

> GitHub Emoji for ASP.NET Core

The package includes:

- TagHelpers
- HtmlHelpers

## TagHelpers

Update the `_ViewImports.cshtml` file, to enable tag helpers in all Razor views:

```cshtml
@addTagHelper *, GEmojiSharp.AspNetCore
```

Use the `<emoji>` tag or `emoji` attribute to render emojis:

```html
<span emoji=":tada:"></span>
<emoji>:tada: initial commit</emoji>
```

Do you want to use emoji anywhere, on any tag, in the `body`? Then you can use the `BodyTagHelperComponent`.

Registration via services container:

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddTransient<ITagHelperComponent, BodyTagHelperComponent>();
```

Use any tag to render emojis:

```html
<h1>Hello, :earth_africa:</h1>
```

## HtmlHelpers

Update the `_ViewImports.cshtml` file, to enable HTML helpers in all Razor views:

```cshtml
@using GEmojiSharp.AspNetCore
```

Use the `Emoji` extension methods to render emojis:

```cshtml
@Html.Emoji(":tada: initial commit")
@Html.Emoji(x => x.Text)
```

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/GEmojiSharp](https://github.com/hlaueriksson/GEmojiSharp)

# GEmojiSharp.Blazor 📦

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

> GitHub Emoji for Blazor

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

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/GEmojiSharp](https://github.com/hlaueriksson/GEmojiSharp)

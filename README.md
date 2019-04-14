# GEmojiSharp

[![Build status](https://ci.appveyor.com/api/projects/status/0fau2qdcv54sb7k0?svg=true)](https://ci.appveyor.com/project/hlaueriksson/gemojisharp)
[![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

[![GEmojiSharp](https://img.shields.io/nuget/v/GEmojiSharp.svg?label=GEmojiSharp)](https://www.nuget.org/packages/GEmojiSharp)
[![GEmojiSharp.TagHelpers](https://img.shields.io/nuget/v/GEmojiSharp.TagHelpers.svg?label=GEmojiSharp.TagHelpers)](https://www.nuget.org/packages/GEmojiSharp.TagHelpers)

> GitHub Emoji for C# and ASP.NET Core

```
🐙 :octopus:
➕ :heavy_plus_sign:
🐈 :cat2:
⩵
❤️ :heart:
```

## Content

- [Introduction](#introduction)
- [Emoji](#emoji)
- [Tag Helpers](#tag-helpers)
- [Attribution](#attribution)

# Introduction

A list of all GitHub Emojis:

https://github.com/hlaueriksson/github-emoji

# Emoji

[![NuGet](https://buildstats.info/nuget/GEmojiSharp)](https://www.nuget.org/packages/GEmojiSharp/)

```csharp
Emoji.Get(":tada:").Raw; // 🎉
Emoji.Raw(":tada:"); // 🎉
Emoji.Emojify(":tada: initial commit"); // 🎉 initial commit
Emoji.Find("party popper").First().Raw; // 🎉
```

```csharp
":tada:".GetEmoji().Raw; // 🎉
":tada:".RawEmoji(); // 🎉
":tada: initial commit".Emojify(); // 🎉 initial commit
"party popper".FindEmojis().First().Raw; // 🎉
```

# Tag Helpers

[![NuGet](https://buildstats.info/nuget/GEmojiSharp.TagHelpers)](https://www.nuget.org/packages/GEmojiSharp.TagHelpers/)

`_ViewImports.cshtml`:

```cshtml
@using GEmojiSharp.Sample.Web
@namespace GEmojiSharp.Sample.Web.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, GEmojiSharp.TagHelpers
```

```html
<span emoji=":tada:"></span> <!-- <span><g-emoji class="g-emoji" alias="tada" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f389.png">🎉</g-emoji></span> -->
<emoji>:tada: initial commit</emoji> <!-- <emoji><g-emoji class="g-emoji" alias="tada" fallback-src="https://github.githubassets.com/images/icons/emoji/unicode/1f389.png">🎉</g-emoji> initial commit</emoji> -->
```

# Attribution

https://github.com/github/gemoji

https://github.com/github/g-emoji-element

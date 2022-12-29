# GEmojiSharp 📦

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

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

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/GEmojiSharp](https://github.com/hlaueriksson/GEmojiSharp)

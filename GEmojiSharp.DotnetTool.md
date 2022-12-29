# GEmojiSharp.DotnetTool 🧰

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

> GitHub Emoji `dotnet` tool

## Raw

Get raw emojis:

> Find emojis via description, category, alias or tag

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

## Alias

Get emoji aliases:

> Find emojis via description, category, alias or tag

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

## Emojify

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

## Demojify

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

## Export

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

Formats:

- `json`
- `toml`
- `xml`
- `yaml`

## Help

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

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/GEmojiSharp](https://github.com/hlaueriksson/GEmojiSharp)

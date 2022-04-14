# GEmojiSharp.DotnetTool 🧰

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

> GitHub Emoji dotnet tool

## Raw

Get raw emojis:

> Find emojis via description, category, alias or tag

```cmd
gemojisharp raw "grinning cat"
gemojisharp raw grinning cat
gemojisharp r grinning cat
```

```cmd
😺
😸
```

Copy to clipboard:

```cmd
gemojisharp raw "grinning cat" --copy
gemojisharp r grinning cat -c
```

```txt
😺😸
```

## Alias

Get emoji aliases:

> Find emojis via description, category, alias or tag

```cmd
gemojisharp alias "grinning cat"
gemojisharp alias grinning cat
gemojisharp a grinning cat
```

```cmd
:smiley_cat:
:smile_cat:
```

Copy to clipboard:

```cmd
gemojisharp alias "grinning cat" --copy
gemojisharp a grinning cat -c
```

```txt
:smiley_cat::smile_cat:
```

## Emojify

Replace aliases in text with raw emojis:

```cmd
gemojisharp emojify ":tada: initial commit"
gemojisharp emojify :tada: initial commit
gemojisharp e :tada: initial commit
```

```cmd
🎉 initial commit
```

Copy to clipboard:

```cmd
gemojisharp emojify ":tada: initial commit" --copy
gemojisharp e :tada: initial commit -c
```

## Demojify

Replace raw emojis in text with aliases:

```cmd
gemojisharp demojify "🎉 initial commit"
gemojisharp demojify 🎉 initial commit
gemojisharp d 🎉 initial commit
```

```cmd
:tada: initial commit
```

Copy to clipboard:

```cmd
gemojisharp demojify "🎉 initial commit" --copy
gemojisharp d 🎉 initial commit -c
```

## Export

Export emoji data to `json`:

```cmd
gemojisharp export "grinning cat" --format json
gemojisharp export grinning cat --format json
gemojisharp export grinning cat -f json
gemojisharp export grinning cat
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
gemojisharp export "grinning cat" --format json --copy
gemojisharp export "grinning cat" -c
```

Formats:

- `json`
- `toml`
- `xml`
- `yaml`

## Help

```cmd
gemojisharp --help
```

```cmd
Description:
  GitHub Emoji dotnet tool

Usage:
  gemojisharp [command] [options]

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

# GEmojiSharp.McpServer 🤖

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp)

> GitHub Emoji MCP Server

## all

> Returns all emojis.

Example:

```txt
Get me a list of all GitHub emojis, then look for those that have an alias with only one character. Display the result in a table with the columns "Alias" and "Raw".
```

## get

> Gets the emoji associated with the alias or raw Unicode string.

Example:

```txt
Get the emoji for `:tada:` and display all information in a bulleted list.
```

## find

> Returns emojis that match the Description, Category, Aliases or Tags.

Example:

```txt
Find "cat" emojis.
```

## emojify

> Replaces emoji aliases with raw Unicode strings.

Example:

```txt
MCP this to emojis: ":octopus: :heavy_plus_sign: :cat2: ⩵ :heart:"
```

## demojify

> Replaces raw Unicode strings with emoji aliases.

Example:

```txt
MCP this to aliases: "🐙 ➕ 🐈 ⩵ ❤️"
```

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/GEmojiSharp](https://github.com/hlaueriksson/GEmojiSharp)

# GEmojiSharp.PowerToysRun 🗂️🔎🔌

[![Build status](https://github.com/hlaueriksson/GEmojiSharp/workflows/build/badge.svg)](https://github.com/hlaueriksson/GEmojiSharp/actions?query=workflow%3Abuild) [![CodeFactor](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp/badge)](https://www.codefactor.io/repository/github/hlaueriksson/gemojisharp) [![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/hlaueriksson/GEmojiSharp/total)](https://github.com/hlaueriksson/GEmojiSharp/releases/latest)

> GitHub Emoji [PowerToys Run](https://docs.microsoft.com/en-us/windows/powertoys/run) plugin

## Installation

The plugin is developed and tested with `PowerToys` `v0.82.0`.

Install:

0. [Install PowerToys](https://docs.microsoft.com/en-us/windows/powertoys/install)
1. Exit PowerToys
2. Download the `.zip` file from the latest [release](https://github.com/hlaueriksson/GEmojiSharp/releases/latest) and extract it to:
   - `%LocalAppData%\Microsoft\PowerToys\PowerToys Run\Plugins`
3. Start PowerToys

## Usage

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

## Configuration

Change action keyword:

1. Open PowerToys
2. Select PowerToys Run
3. Scroll down to Plugins
4. Expand `GEmojiSharp`
5. Change *Direct activation command*

## Would you like to know more? 🤔

Further documentation is available at [https://github.com/hlaueriksson/GEmojiSharp](https://github.com/hlaueriksson/GEmojiSharp)

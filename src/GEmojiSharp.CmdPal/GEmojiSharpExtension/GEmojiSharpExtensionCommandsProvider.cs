using GEmojiSharpExtension.Models;
using GEmojiSharpExtension.Pages;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension;

public partial class GEmojiSharpExtensionCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public GEmojiSharpExtensionCommandsProvider()
    {
        DisplayName = "GitHub Emoji";
        Icon = Icons.Icon;
        _commands = [
            new CommandItem(new MainPage()) { Title = DisplayName },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }
}

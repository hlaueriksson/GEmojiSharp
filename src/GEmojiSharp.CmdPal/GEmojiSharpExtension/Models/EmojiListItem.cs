using System.Collections.Generic;
using System.Linq;
using GEmojiSharp;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension.Models;

public sealed partial class EmojiListItem : ListItem
{
    public EmojiListItem(GEmoji emoji)
    {
        Icon = emoji.IsCustom ? IconHelpers.FromRelativePath($"Assets/Custom/{emoji.Filename}.png") : new IconInfo(emoji.Raw);
        Title = string.Join(" ", emoji.Aliases.Select(x => x.PadAlias()));
        Subtitle = emoji.Description!;
        Command = emoji.IsCustom ? new NoOpCommand() : new CopyTextCommand(emoji.Raw);
        MoreCommands = [.. GetMoreCommands(emoji)];
        Tags = emoji.Tags != null ? [.. emoji.Tags.Select(x => new Tag(x))] : [];
        Details = new Details
        {
            Title = "GEmoji",
            Body = GetDetailsBody(emoji),
        };
    }

    private static IEnumerable<IContextItem> GetMoreCommands(GEmoji emoji)
    {
        var title = emoji.Aliases.Length > 1 ? "Copy Aliases" : "Copy Alias";
        yield return new CommandContextItem(new CopyTextCommand(string.Concat(emoji.Aliases.Select(x => x.PadAlias()))) { Name = title })
        {
            Title = title,
        };

        if (emoji.HasSkinTones)
        {
            yield return new CommandContextItem(new CopyTextCommand(emoji.Raw + string.Concat(emoji.RawSkinToneVariants())) { Name = "Copy SkinTones" })
            {
                Title = "Copy SkinTones",
            };
        }
    }

    private static string GetDetailsBody(GEmoji emoji) =>
        (emoji.Category != null ? $"**Category:** {emoji.Category}\n\n" : "") +
        (emoji.UnicodeVersion != null ? $"**UnicodeVersion:** {emoji.UnicodeVersion}\n\n" : "") +
        (emoji.IosVersion != null ? $"**IosVersion:** {emoji.IosVersion}\n\n" : "") +
        (emoji.HasSkinTones ? $"**SkinTones:** {string.Concat(emoji.RawSkinToneVariants())}\n\n" : "") +
        (emoji.IsCustom ? $"**IsCustom:** ✅" : "");
}

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GEmojiSharp;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension;

internal sealed partial class MainPage : DynamicListPage
{
    private IListItem[] All { get; }
    private List<IListItem> Query { get; } = [];

    public MainPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "GitHub Emoji";
        Name = "Open";

        All = [.. Emoji.All.Select(ToListItem)];
    }

    public override IListItem[] GetItems()
    {
        return Query.Count > 0 ? [.. Query] : All;
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        if (oldSearch == newSearch)
        {
            return;
        }

        Query.Clear();

        if (string.IsNullOrEmpty(newSearch))
        {
            return;
        }

        IsLoading = true;

        var emojis = Emoji.Find(newSearch).ToArray();

        if (emojis.Length != 0)
        {
            Query.AddRange(emojis.Select(ToListItem));
        }

        if (HasAlias(newSearch))
        {
            var result = Emoji.Emojify(newSearch);

            Query.Add(
                new ListItem
                {
                    Title = result,
                    Subtitle = "Replace aliases in text with raw emojis",
                    Command = new CopyTextCommand(result),
                });
        }

        if (HasEmoji(newSearch))
        {
            var result = Emoji.Demojify(newSearch);

            Query.Add(
                new ListItem
                {
                    Title = result,
                    Subtitle = "Replace raw emojis in text with aliases",
                    Command = new CopyTextCommand(result),
                });
        }

        IsLoading = false;
        RaiseItemsChanged(Query.Count);

        bool HasAlias(string value)
        {
            return Regex.IsMatch(value, @":([\w+-]+):", RegexOptions.Compiled);
        }

        bool HasEmoji(string value)
        {
            return Regex.IsMatch(value, Emoji.RegexPattern, RegexOptions.Compiled);
        }
    }

    private ListItem ToListItem(GEmoji emoji) => new()
    {
        Icon = emoji.IsCustom ? IconHelpers.FromRelativePath($"Assets/Custom/{emoji.Filename}.png") : new IconInfo(emoji.Raw),
        Title = string.Join(" ", emoji.Aliases.Select(x => x.PadAlias())),
        Subtitle = emoji.Description,
        Command = emoji.IsCustom ? new NoOpCommand() : new CopyTextCommand(emoji.Raw),
        MoreCommands = [.. GetMoreCommands(emoji)],
        Tags = emoji.Tags != null ? [.. emoji.Tags.Select(x => new Tag(x))] : [],
        Details = new Details
        {
            Title = "GEmoji",
            Body = GetDetailsBody(emoji),
        },
    };
    private IEnumerable<IContextItem> GetMoreCommands(GEmoji emoji)
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

    private string GetDetailsBody(GEmoji emoji)
    {
        return
            (emoji.Category != null ? $"**Category:** {emoji.Category}\n\n" : "") +
            (emoji.UnicodeVersion != null ? $"**UnicodeVersion:** {emoji.UnicodeVersion}\n\n" : "") +
            (emoji.IosVersion != null ? $"**IosVersion:** {emoji.IosVersion}\n\n" : "") +
            (emoji.HasSkinTones ? $"**SkinTones:** {string.Concat(emoji.RawSkinToneVariants())}\n\n" : "") +
            (emoji.IsCustom ? $"**IsCustom:** ✅" : "");
    }
}

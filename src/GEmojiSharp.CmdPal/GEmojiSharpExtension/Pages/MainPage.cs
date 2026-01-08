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
        Title = emoji.Raw,
        Subtitle = emoji.Description,
        Command = new CopyTextCommand(emoji.Raw),
    };
}

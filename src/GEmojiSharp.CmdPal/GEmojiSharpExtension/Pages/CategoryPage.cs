using System.Linq;
using GEmojiSharp;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension;

public sealed partial class CategoryPage : ListPage
{
    private IListItem[] Emojis { get; }

    public CategoryPage(IGrouping<string, GEmoji> category)
    {
        Name = category.Key;
        Title = $"{category.Key}: {category.Count()} emojis";

        Emojis = [.. category.Select(x => new EmojiListItem(x))];
    }

    public override IListItem[] GetItems()
    {
        return Emojis;
    }
}

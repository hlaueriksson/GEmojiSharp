using System.Linq;
using GEmojiSharp;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension;

public sealed partial class CategoryPage : ListPage
{
    private IListItem[] Emojis { get; }

    public CategoryPage(IGrouping<string?, GEmoji> category)
    {
        var name = category.Key ?? "Custom";

        Name = name;
        Title = $"{name}: {category.Count()} emojis";

        Emojis = [.. category.Select(x => new EmojiListItem(x))];
    }

    public override IListItem[] GetItems()
    {
        return Emojis;
    }
}

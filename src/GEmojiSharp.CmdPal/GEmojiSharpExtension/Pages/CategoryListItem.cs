using System.Linq;
using GEmojiSharp;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension;

public sealed partial class CategoryListItem : ListItem
{
    public CategoryListItem(IGrouping<string, GEmoji> category)
    {
        Icon = new IconInfo(category.First().Raw);
        Title = category.Key;
        Subtitle = $"{category.Count()} emojis";
        Command = new CategoryPage(category);
    }
}

using System.Linq;
using GEmojiSharp;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension;

public sealed partial class CategoryListItem : ListItem
{
    public CategoryListItem(IGrouping<string?, GEmoji> category)
    {
        var emoji = category.First();

        Icon = emoji.IsCustom ? IconHelpers.FromRelativePath($"Assets/Custom/{emoji.Filename}.png") : new IconInfo(emoji.Raw);
        Title = category.Key ?? "Custom";
        Subtitle = $"{category.Count()} emojis";
        Command = new CategoryPage(category);
    }
}

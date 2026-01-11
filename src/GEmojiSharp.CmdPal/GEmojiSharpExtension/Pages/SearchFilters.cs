using System;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension;

public enum SearchType
{
    Emoji,
    Category,
    Transform,
}

public static class SearchTypeExtensions
{
    public static SearchType ToSearchType(this string filterId) =>
        Enum.TryParse<SearchType>(filterId, true, out var result) ? result : SearchType.Emoji;
}

internal sealed partial class SearchFilters : Filters
{
    public SearchFilters()
    {
        CurrentFilterId = SearchType.Emoji.ToString();
    }

    public override IFilterItem[] GetFilters()
    {
        return [
            new Filter() { Id = SearchType.Emoji.ToString(), Name = "Emojis", Icon = new IconInfo("\uE76E") }, // Emoji2
            new Filter() { Id = SearchType.Category.ToString(), Name = "Categories", Icon = new IconInfo("\uF168") }, // GroupList
            new Filter() { Id = SearchType.Transform.ToString(), Name = "Transform", Icon = new IconInfo("\uE8B1") }, // Shuffle
        ];
    }
}

using System;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension.Models;

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
            new Filter() { Id = SearchType.Emoji.ToString(), Name = "Emojis", Icon = Icons.Emojis },
            new Filter() { Id = SearchType.Category.ToString(), Name = "Categories", Icon = Icons.Categories },
            new Filter() { Id = SearchType.Transform.ToString(), Name = "Transform", Icon = Icons.Transform },
        ];
    }
}

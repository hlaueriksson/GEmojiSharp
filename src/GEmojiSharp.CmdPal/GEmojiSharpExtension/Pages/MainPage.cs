using System.Collections.Generic;
using System.Linq;
using GEmojiSharp;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension;

internal sealed partial class MainPage : ListPage//DynamicListPage
{
    private IListItem[] AllEmojis { get; }
    private IListItem[] AllCategories { get; }
    //private List<IListItem> Query { get; } = [];

    public MainPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "GitHub Emoji";
        Name = "Open";

        var filters = new SearchFilters();
        filters.PropChanged += Filters_PropChanged;
        Filters = filters;

        AllEmojis = [.. Emoji.All.Select(x => new EmojiListItem(x))];
        AllCategories = [.. GetAllCategories()];
    }

    private IEnumerable<IListItem> GetAllCategories()
    {
        var categories = Emoji.All.GroupBy(x => x.Category).ToArray();

        foreach (var category in categories)
        {
            yield return new CategoryListItem(category);
        }
    }

    public override IListItem[] GetItems()
    {
        var currentFilterId = Filters?.CurrentFilterId.ToSearchType();

        return /*Query.Count > 0 ? [.. Query] :*/ currentFilterId == SearchType.Category ? AllCategories : AllEmojis;
    }

    //public override void UpdateSearchText(string oldSearch, string newSearch)
    //{
    //    if (oldSearch == newSearch)
    //    {
    //        return;
    //    }

    //    Query.Clear();

    //    if (string.IsNullOrEmpty(newSearch))
    //    {
    //        return;
    //    }

    //    IsLoading = true;

    //    var emojis = Emoji.Find(newSearch).ToArray();

    //    if (emojis.Length != 0)
    //    {
    //        Query.AddRange(emojis.Select(x => new EmojiListItem(x)));
    //    }

    //    if (HasAlias(newSearch))
    //    {
    //        var result = Emoji.Emojify(newSearch);

    //        Query.Add(
    //            new ListItem
    //            {
    //                Title = result,
    //                Subtitle = "Replace aliases in text with raw emojis",
    //                Command = new CopyTextCommand(result),
    //            });
    //    }

    //    if (HasEmoji(newSearch))
    //    {
    //        var result = Emoji.Demojify(newSearch);

    //        Query.Add(
    //            new ListItem
    //            {
    //                Title = result,
    //                Subtitle = "Replace raw emojis in text with aliases",
    //                Command = new CopyTextCommand(result),
    //            });
    //    }

    //    IsLoading = false;
    //    RaiseItemsChanged(Query.Count);

    //    bool HasAlias(string value)
    //    {
    //        return Regex.IsMatch(value, @":([\w+-]+):", RegexOptions.Compiled);
    //    }

    //    bool HasEmoji(string value)
    //    {
    //        return Regex.IsMatch(value, Emoji.RegexPattern, RegexOptions.Compiled);
    //    }
    //}

    private void Filters_PropChanged(object sender, IPropChangedEventArgs args)
    {
        //UpdateSearchText(SearchText, SearchText);

        RaiseItemsChanged();
    }
}

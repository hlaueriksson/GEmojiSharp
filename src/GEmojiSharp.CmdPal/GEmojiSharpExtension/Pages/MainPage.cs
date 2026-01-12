using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using GEmojiSharp;
using GEmojiSharpExtension.Models;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension.Pages;

internal sealed partial class MainPage : DynamicListPage
{
    private IListItem[] AllEmojis { get; }
    private IListItem[] AllCategories { get; }
    private IListItem[] TransformHelp { get; }
    private List<IListItem> Results { get; } = [];

    public MainPage()
    {
        Debug.WriteLine($"MainPage");

        Icon = Icons.Icon;
        Title = "GitHub Emoji";
        Name = "Open";

        var filters = new SearchFilters();
        filters.PropChanged += Filters_PropChanged;
        Filters = filters;

        AllEmojis = [.. Emoji.All.Select(x => new EmojiListItem(x))];
        AllCategories = [.. GetAllCategories()];
        TransformHelp = [
            new ListItem
            {
                Icon = Icons.Transform,
                Title = "Paste text in the search field",
                Subtitle = "Emojify: Aliases will be replaced with raw emojis\nDemojify: Raw emojis will be replaced with aliases",
                Command = new NoOpCommand(),
            }
        ];
    }

    public override IListItem[] GetItems()
    {
        Debug.WriteLine($"GetItems");

        if (!string.IsNullOrEmpty(SearchText)) return [.. Results];

        var currentFilterId = Filters?.CurrentFilterId.ToSearchType();

        Debug.WriteLine($"currentFilterId: {currentFilterId}");

        return currentFilterId switch
        {
            SearchType.Emoji => AllEmojis,
            SearchType.Category => AllCategories,
            SearchType.Transform => TransformHelp,
            _ => AllEmojis,
        };
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        Debug.WriteLine($"UpdateSearchText: {oldSearch} => {newSearch}");

        Results.Clear();

        IsLoading = true;

        var currentFilterId = Filters?.CurrentFilterId.ToSearchType();

        switch (currentFilterId)
        {
            case SearchType.Emoji:
                {
                    SearchEmojis(newSearch);
                    break;
                }
            case SearchType.Category:
                {
                    SearchCategories(newSearch);
                    break;
                }
            case SearchType.Transform:
                {
                    Transform(newSearch);
                    break;
                }
        }

        IsLoading = false;
        RaiseItemsChanged(Results.Count);
    }

    private void Filters_PropChanged(object sender, IPropChangedEventArgs args)
    {
        Debug.WriteLine($"Filters_PropChanged: {args.PropertyName}");
        UpdateSearchText(string.Empty, SearchText);
        RaiseItemsChanged();
    }

    private static IEnumerable<IListItem> GetAllCategories()
    {
        var categories = Emoji.All.GroupBy(x => x.Category).ToArray();

        foreach (var category in categories)
        {
            yield return new CategoryListItem(category);
        }
    }

    private void SearchEmojis(string newSearch)
    {
        Debug.WriteLine($"SearchEmojis: {newSearch}");
        var emojis = Emoji.Find(newSearch).ToArray();
        Results.AddRange(emojis.Select(x => new EmojiListItem(x)));
    }

    private void SearchCategories(string newSearch)
    {
        Debug.WriteLine($"SearchCategories: {newSearch}");
        var categories = Emoji.All
            .Where(x => x.Category != null && x.Category.Contains(newSearch, System.StringComparison.OrdinalIgnoreCase))
            .GroupBy(x => x.Category)
            .ToArray();
        Results.AddRange(categories.Select(x => new CategoryListItem(x)));
    }

    private void Transform(string newSearch)
    {
        Debug.WriteLine($"Transform: {newSearch}");

        if (HasAlias(newSearch))
        {
            var result = Emoji.Emojify(newSearch);
            Results.Add(
                new ListItem
                {
                    Icon = Icons.Transform,
                    Title = result,
                    Subtitle = "Emojify: Replace aliases with raw emojis",
                    Command = new CopyTextCommand(result),
                });
        }
        if (HasEmoji(newSearch))
        {
            var result = Emoji.Demojify(newSearch);
            Results.Add(
                new ListItem
                {
                    Icon = Icons.Transform,
                    Title = result,
                    Subtitle = "Demojify: Replace raw emojis with aliases",
                    Command = new CopyTextCommand(result),
                });
        }

        bool HasAlias(string value)
        {
            return Regex.IsMatch(value, @":([\w+-]+):", RegexOptions.Compiled);
        }

        bool HasEmoji(string value)
        {
            return Regex.IsMatch(value, Emoji.RegexPattern, RegexOptions.Compiled);
        }
    }
}

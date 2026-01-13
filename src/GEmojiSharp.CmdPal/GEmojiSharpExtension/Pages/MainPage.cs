using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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

    private readonly Lock ResultsLock = new();
    private List<IListItem> Results { get; } = [];
    private const int PageSize = 20;
    private int CurrentPage { get; set; }

    public MainPage()
    {
        Debug.WriteLine($"MainPage");

        Icon = Icons.Icon;
        Title = "GitHub Emoji";
        Name = "Open";

        var filters = new SearchFilters();
        filters.PropChanged += SearchFiltersChanged;
        Filters = filters;

        AllEmojis = [.. Emoji.All.Select(x => new EmojiListItem(x))];
        AllCategories = [.. Emoji.All.GroupBy(x => x.Category).Select(x => new CategoryListItem(x))];
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

        // First
        if (CurrentPage == 0)
        {
            LoadMore();
        }

        lock (ResultsLock)
        {
            return [.. Results.Take(PageSize * CurrentPage)];
        }
    }

    public override void LoadMore()
    {
        Debug.WriteLine($"LoadMore");

        IsLoading = true;

        // Init
        if (CurrentPage == 0 && SearchText == string.Empty)
        {
            lock (ResultsLock)
            {
                Results.Clear();

                var currentFilterId = Filters?.CurrentFilterId.ToSearchType();

                Debug.WriteLine($"currentFilterId: {currentFilterId}");

                switch (currentFilterId)
                {
                    case SearchType.Emoji:
                        Results.AddRange(AllEmojis);
                        break;
                    case SearchType.Category:
                        Results.AddRange(AllCategories);
                        break;
                    case SearchType.Transform:
                        Results.AddRange(TransformHelp);
                        break;
                }
            }
        }

        CurrentPage++;
        int resultsCount;
        lock (ResultsLock)
        {
            resultsCount = Results.Count;
        }
        HasMoreItems = resultsCount > PageSize * CurrentPage;
        IsLoading = false;
        RaiseItemsChanged(Math.Min(PageSize * CurrentPage, resultsCount));
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        Debug.WriteLine($"UpdateSearchText: {newSearch}");

        // Clear
        lock (ResultsLock)
        {
            Results.Clear();
        }
        CurrentPage = 0;

        if (SearchText != string.Empty)
        {
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
        }

        LoadMore();
    }

    private void SearchFiltersChanged(object sender, IPropChangedEventArgs args)
    {
        Debug.WriteLine($"SearchFiltersChanged: {args.PropertyName}");
        UpdateSearchText(string.Empty, SearchText);
    }

    private void SearchEmojis(string newSearch)
    {
        Debug.WriteLine($"SearchEmojis: {newSearch}");
        var emojis = Emoji.Find(newSearch).ToArray();
        lock (ResultsLock)
        {
            Results.AddRange(emojis.Select(x => new EmojiListItem(x)));
        }
    }

    private void SearchCategories(string newSearch)
    {
        Debug.WriteLine($"SearchCategories: {newSearch}");
        var categories = AllCategories.Where(x => x.Title.Contains(newSearch, StringComparison.OrdinalIgnoreCase));
        lock (ResultsLock)
        {
            Results.AddRange(categories);
        }
    }

    private void Transform(string newSearch)
    {
        Debug.WriteLine($"Transform: {newSearch}");

        if (newSearch.HasAlias())
        {
            var result = Emoji.Emojify(newSearch);
            lock (ResultsLock)
            {
                Results.Add(
                    new ListItem
                    {
                        Icon = Icons.Transform,
                        Title = result,
                        Subtitle = "Emojify: Replace aliases with raw emojis",
                        Command = new CopyTextCommand(result),
                    });
            }
        }

        if (newSearch.HasEmoji())
        {
            var result = Emoji.Demojify(newSearch);
            lock (ResultsLock)
            {
                Results.Add(
                    new ListItem
                    {
                        Icon = Icons.Transform,
                        Title = result,
                        Subtitle = "Demojify: Replace raw emojis with aliases",
                        Command = new CopyTextCommand(result),
                    });
            }
        }
    }
}

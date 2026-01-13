using GEmojiSharpExtension.Models;
using GEmojiSharpExtension.Pages;

namespace GEmojiSharpExtension.Tests;

[TestClass]
public class MainPageThreadSafetyTests
{
    private MainPage _subject = null!;

    [TestInitialize]
    public void SetUp()
    {
        _subject = new MainPage();
    }

    [TestMethod]
    public void ConcurrentFilterChanges_ShouldNotCauseRaceConditions()
    {
        // Arrange
        var tasks = new List<Task>();
        var filters = new[] {
            SearchType.Emoji.ToString(),
            SearchType.Category.ToString(),
            SearchType.Transform.ToString()
        };

        // Act - Switch filters and search simultaneously
        for (int i = 0; i < 100; i++)
        {
            int index = i;

            tasks.Add(Task.Run(() =>
            {
                _subject.Filters!.CurrentFilterId = filters[index % filters.Length];
                _subject.SearchText = "test";
                var items = _subject.GetItems();
                _ = items.Length;
            }));
        }

        // Assert
        Task.WaitAll(tasks.ToArray());
    }
}

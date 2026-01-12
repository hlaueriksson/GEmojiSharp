using FluentAssertions;

namespace GEmojiSharpExtension.Tests;

[TestClass]
public class MainPageTests
{
    private MainPage _subject = null!;

    [TestInitialize]
    public void SetUp()
    {
        _subject = new MainPage();
    }

    [TestMethod]
    public void GetItems_Emoji()
    {
        _subject.GetItems().Should().NotBeEmpty();

        _subject.SearchText = "globe showing";
        _subject.GetItems().Should().HaveCount(3)
            .And.Contain(x => x.Title == ":earth_africa:")
            .And.Contain(x => x.Title == ":earth_americas:")
            .And.Contain(x => x.Title == ":earth_asia:");

        _subject.SearchText = "tada";
        _subject.GetItems().Should().ContainSingle(x => x.Title == ":tada:");
    }

    [TestMethod]
    public void GetItems_Category()
    {
        _subject.Filters.CurrentFilterId = SearchType.Category.ToString();
        _subject.GetItems().Should().NotBeEmpty();

        _subject.SearchText = "body";
        _subject.GetItems().Should().HaveCount(1)
            .And.Contain(x => x.Title == "People & Body");
    }

    [TestMethod]
    public void GetItems_Transform()
    {
        _subject.Filters.CurrentFilterId = SearchType.Transform.ToString();
        _subject.GetItems().Should().BeEmpty();

        _subject.SearchText = "Hello, :earth_africa:";
        _subject.GetItems().Should().HaveCount(1)
            .And.Contain(x => x.Title == "Hello, 🌍");

        _subject.SearchText = "Hello, 🌍";
        _subject.GetItems().Should().HaveCount(1)
            .And.Contain(x => x.Title == "Hello, :earth_africa:");
    }
}

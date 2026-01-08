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
    public void GetItems()
    {
        _subject.GetItems().Should().NotBeEmpty();

        _subject.UpdateSearchText("", "globe showing");
        _subject.GetItems().Should()
            .Contain(x => x.Title == "🌍")
            .And.Contain(x => x.Title == "🌎")
            .And.Contain(x => x.Title == "🌏");

        _subject.UpdateSearchText("", "tada");
        _subject.GetItems().Should().ContainSingle(x => x.Title == "🎉");
    }
}

using FluentAssertions;

namespace GEmojiSharpExtension.Tests;

[TestClass]
public class GEmojiSharpExtensionPageTests
{
    private GEmojiSharpExtensionPage _subject = null!;

    [TestInitialize]
    public void SetUp()
    {
        _subject = new GEmojiSharpExtensionPage();
    }

    [TestMethod]
    public void GetItems()
    {
        _subject.GetItems().Should().ContainSingle()
            .Which.Title.Should().Be("TODO: Implement your extension here");
    }
}

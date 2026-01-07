using FluentAssertions;

namespace GEmojiSharp.Tests.McpServer
{
    public class EmojiToolsTests
    {
        [Test]
        public void All()
        {
            var result = new EmojiTools().All();
            result.Length.Should().Be(Emoji.All.Length);
        }

        [Test]
        public void Get()
        {
            var result = new EmojiTools().Get(":grinning:");
            result.Raw.Should().Be(":grinning:".RawEmoji());
        }

        [Test]
        public void Find()
        {
            var result = new EmojiTools().Find("face");
            result.Length.Should().Be("face".FindEmojis().Count());
        }

        [Test]
        public void Emojify()
        {
            var result = new EmojiTools().Emojify("Hello, :earth_africa:");
            result.Should().Be("Hello, :earth_africa:".Emojify());
        }

        [Test]
        public void Demojify()
        {
            var result = new EmojiTools().Demojify("Hello, 🌍");
            result.Should().Be("Hello, 🌍".Demojify());
        }
    }
}

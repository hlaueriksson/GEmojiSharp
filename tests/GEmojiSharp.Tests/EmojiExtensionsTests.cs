using FluentAssertions;
using NUnit.Framework;

namespace GEmojiSharp.Tests
{
    public class EmojiExtensionsTests
    {
        [Test]
        public void GetEmoji()
        {
            ":grinning:".GetEmoji().Should().NotBe(GEmoji.Empty);
            ":fail:".GetEmoji().Should().Be(GEmoji.Empty);
        }

        [Test]
        public void RawEmoji()
        {
            ":grinning:".RawEmoji().Should().Be("😀");
            ":fail:".RawEmoji().Should().BeEmpty();
        }

        [Test]
        public void Emojify()
        {
            "Hello, :earth_africa:".Emojify().Should().Be("Hello, 🌍");
            "Hello, :fail:".Emojify().Should().Be("Hello, :fail:");
        }

        [Test]
        public void FindEmojis()
        {
            "face".FindEmojis().Should().NotBeEmpty();
            "fail".FindEmojis().Should().BeEmpty();
        }
    }
}
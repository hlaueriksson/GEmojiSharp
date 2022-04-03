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
            "😀".GetEmoji().Should().NotBe(GEmoji.Empty);
            "字".GetEmoji().Should().Be(GEmoji.Empty);
        }

        [Test]
        public void RawEmoji()
        {
            ":grinning:".RawEmoji().Should().Be("😀");
            ":fail:".RawEmoji().Should().BeEmpty();
        }

        [Test]
        public void EmojiAlias()
        {
            "😀".EmojiAlias().Should().Be(":grinning:");
            "字".EmojiAlias().Should().BeEmpty();
        }

        [Test]
        public void Emojify()
        {
            "Hello, :earth_africa:".Emojify().Should().Be("Hello, 🌍");
            "Hello, :fail:".Emojify().Should().Be("Hello, :fail:");
        }

        [Test]
        public void Demojify()
        {
            "Hello, 🌍".Demojify().Should().Be("Hello, :earth_africa:");
            "Hello, 字".Demojify().Should().Be("Hello, 字");
        }

        [Test]
        public void FindEmojis()
        {
            "face".FindEmojis().Should().NotBeEmpty();
            "fail".FindEmojis().Should().BeEmpty();
        }

        [Test]
        public void Alias()
        {
            "😀".GetEmoji().Alias().Should().Be(":grinning:");
            ":atom:".GetEmoji().Alias().Should().Be(":atom:");
            GEmoji.Empty.Alias().Should().BeEmpty();
        }
    }
}

using FluentAssertions;
using NUnit.Framework;
using static GEmojiSharp.Emoji;

namespace GEmojiSharp.Tests
{
    public class EmojiTests
    {
        [Test]
        public void Get()
        {
            Emoji.Get(":grinning:").Should().NotBe(GEmoji.Empty);
            Emoji.Get(":fail:").Should().Be(GEmoji.Empty);
            Emoji.Get(":grinning:").Should().Be(Emoji.Get("grinning"));
            Emoji.Get(":laughing:").Should().Be(Emoji.Get(":satisfied:"));

            var octocat = Emoji.Get(":octocat:");
            octocat.Should().NotBe(GEmoji.Empty);
            octocat.Raw.Should().BeEmpty();
        }

        [Test]
        public void Raw_should_return_the_emoji_character()
        {
            Emoji.Raw(":grinning:").Should().Be("😀");
            Emoji.Raw(":blonde_woman:").Should().Be("👱‍♀");
            Emoji.Raw(":fail:").Should().BeEmpty();
        }

        [Test]
        public void Emojify()
        {
            Emoji.Emojify("Hello, :earth_africa:").Should().Be("Hello, 🌍");
            Emoji.Emojify("Hello, :fail:").Should().Be("Hello, :fail:");
        }

        [Test]
        public void Find()
        {
            Emoji.Find("face").Should().NotBeEmpty();
            Emoji.Find("fail").Should().BeEmpty();
        }

        [Test]
        public void using_static_directive()
        {
            Raw(":grinning:").Should().Be("😀");
        }

        [Test]
        public void string_interpolation()
        {
            $"Hello, {Emoji.Raw(":earth_africa:")}".Should().Be("Hello, 🌍");
            $"Hello, {Raw(":earth_africa:")}".Should().Be("Hello, 🌍");
        }
    }
}
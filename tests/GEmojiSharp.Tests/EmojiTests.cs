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

            Emoji.Get("😀").Should().NotBe(GEmoji.Empty);
            Emoji.Get("字").Should().Be(GEmoji.Empty);
            Emoji.Get("😀").Should().Be(Emoji.Get(":grinning:"));
        }

        [Test]
        public void Raw_should_return_the_emoji_character()
        {
            Emoji.Raw(":grinning:").Should().Be("😀");
            Emoji.Raw(":blonde_woman:").Should().Be("👱‍♀️");
            Emoji.Raw(":fail:").Should().BeEmpty();

            // Regressions
            Emoji.Raw(":beetle:").Should().NotBe("🐞");
            Emoji.Raw(":man_in_tuxedo:").Should().NotBe("🤵");
            Emoji.Raw(":bride_with_veil:").Should().NotBe("👰");
        }

        [Test]
        public void Alias_should_return_the_name_uniquely_referring_to_the_emoji()
        {
            Emoji.Alias("😀").Should().Be(":grinning:");
            Emoji.Alias("👱‍♀️").Should().Be(":blond_haired_woman:");
            Emoji.Alias("字").Should().BeEmpty();
        }

        [Test]
        public void Emojify()
        {
            Emoji.Emojify("Hello, :earth_africa:").Should().Be("Hello, 🌍");
            Emoji.Emojify("Hello, :fail:").Should().Be("Hello, :fail:");
        }

        [Test]
        public void Demojify()
        {
            Emoji.Demojify("Hello, 🌍").Should().Be("Hello, :earth_africa:");
            Emoji.Demojify("Hello, 👱‍♀️").Should().Be("Hello, :blond_haired_woman:");
            Emoji.Demojify("Hello, 字").Should().Be("Hello, 字");
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

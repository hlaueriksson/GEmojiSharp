using System;
using FluentAssertions;
using NUnit.Framework;

namespace GEmojiSharp.Tests
{
    public class EmojiExtensionsTests
    {
        private const string NullString = null;
        private const GEmoji NullGEmoji = null;

        [Test]
        public void GetEmoji()
        {
            ":grinning:".GetEmoji().Should().NotBe(GEmoji.Empty);
            ":fail:".GetEmoji().Should().Be(GEmoji.Empty);
            "😀".GetEmoji().Should().NotBe(GEmoji.Empty);
            "字".GetEmoji().Should().Be(GEmoji.Empty);

            Action act = () => NullString.GetEmoji();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void RawEmoji()
        {
            ":grinning:".RawEmoji().Should().Be("😀");
            ":fail:".RawEmoji().Should().BeEmpty();

            Action act = () => NullString.RawEmoji();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void EmojiAlias()
        {
            "😀".EmojiAlias().Should().Be(":grinning:");
            "字".EmojiAlias().Should().BeEmpty();

            Action act = () => NullString.EmojiAlias();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Emojify()
        {
            "Hello, :earth_africa:".Emojify().Should().Be("Hello, 🌍");
            "Hello, :fail:".Emojify().Should().Be("Hello, :fail:");

            Action act = () => NullString.Emojify();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Demojify()
        {
            "Hello, 🌍".Demojify().Should().Be("Hello, :earth_africa:");
            "Hello, 字".Demojify().Should().Be("Hello, 字");

            Action act = () => NullString.Demojify();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FindEmojis()
        {
            "face".FindEmojis().Should().NotBeEmpty();
            "fail".FindEmojis().Should().BeEmpty();

            Action act = () => NullString.FindEmojis();
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Alias()
        {
            "😀".GetEmoji().Alias().Should().Be(":grinning:");
            ":atom:".GetEmoji().Alias().Should().Be(":atom:");
            GEmoji.Empty.Alias().Should().BeEmpty();
            NullGEmoji.Alias().Should().BeEmpty();
        }

        [Test]
        public void TrimAlias()
        {
            ":foo:".TrimAlias().Should().Be("foo");
            ":foo:bar:".TrimAlias().Should().Be("foo:bar");
        }

        [Test]
        public void PadAlias()
        {
            "foo".PadAlias().Should().Be(":foo:");
            ":bar:".PadAlias().Should().Be(":bar:");
        }
    }
}

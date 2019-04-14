using FluentAssertions;
using NUnit.Framework;

namespace GEmojiSharp.Tests
{
    public class GEmojiTests
    {
        [Test]
        public void HasEmoji()
        {
            Emoji.Get(":grinning:").IsCustom.Should().Be(false);
            Emoji.Get(":octocat:").IsCustom.Should().Be(true);
            Emoji.Get(":fail:").IsCustom.Should().Be(true);
            GEmoji.Empty.IsCustom.Should().Be(true);
        }
    }
}
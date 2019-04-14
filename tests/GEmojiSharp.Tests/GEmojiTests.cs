using FluentAssertions;
using NUnit.Framework;

namespace GEmojiSharp.Tests
{
    public class GEmojiTests
    {
        [Test]
        public void HasEmoji()
        {
            Emoji.Get(":grinning:").HasEmoji.Should().Be(true);
            Emoji.Get(":octocat:").HasEmoji.Should().Be(false);
            Emoji.Get(":fail:").HasEmoji.Should().Be(false);
            GEmoji.Empty.HasEmoji.Should().Be(false);
        }
    }
}
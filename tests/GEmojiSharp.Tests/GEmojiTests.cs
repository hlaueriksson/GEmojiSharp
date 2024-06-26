using FluentAssertions;

namespace GEmojiSharp.Tests
{
    public class GEmojiTests
    {
        [Test]
        public void IsCustom()
        {
            Emoji.Get(":grinning:").IsCustom.Should().Be(false);
            Emoji.Get(":octocat:").IsCustom.Should().Be(true);
            Emoji.Get(":fail:").IsCustom.Should().Be(true);
            GEmoji.Empty.IsCustom.Should().Be(true);
        }
    }
}

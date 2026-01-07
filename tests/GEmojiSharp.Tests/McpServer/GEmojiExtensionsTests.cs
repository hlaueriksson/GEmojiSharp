using FluentAssertions;

namespace GEmojiSharp.Tests.McpServer
{
    public class GEmojiExtensionsTests
    {
        [Test]
        public void ToResult()
        {
            var emoji = Emoji.Get(":grinning:");
            var result = emoji.ToResult();
            result.Raw.Should().Be(emoji.Raw);
            result.Description.Should().Be(emoji.Description);
            result.Category.Should().Be(emoji.Category);
            result.Aliases.Should().BeEquivalentTo(emoji.Aliases);
            result.Tags.Should().BeEquivalentTo(emoji.Tags);
            result.SkinTones.Should().BeNull();
            result.IsCustom.Should().Be(emoji.IsCustom);

            emoji = Emoji.Get(":v:");
            result = emoji.ToResult();
            result.SkinTones.Should().BeEquivalentTo(emoji.RawSkinToneVariants());
        }
    }
}

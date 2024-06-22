using FluentAssertions;
using GEmojiSharp.AspNetCore;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GEmojiSharp.Tests.AspNetCore.TagHelpers
{
    public class EmojiAttributeTagHelperTests
    {
        [Test]
        public void ProcessAsync()
        {
            var subject = new EmojiAttributeTagHelper();

            var output = GetTagHelperOutput(":grinning:");
            subject.Process(null!, output);
            output.Content.GetContent().Should().Be(":grinning:".Markup());

            output = GetTagHelperOutput(":fail:");
            subject.Process(null!, output);
            output.Content.GetContent().Should().Be(":fail:");

            static TagHelperOutput GetTagHelperOutput(string attribute)
            {
                return new TagHelperOutput("span", new TagHelperAttributeList { { "emoji", attribute } }, (flag, encoder) =>
                 {
                     var tagHelperContent = new DefaultTagHelperContent();
                     return Task.FromResult<TagHelperContent>(tagHelperContent);
                 });
            }
        }
    }
}

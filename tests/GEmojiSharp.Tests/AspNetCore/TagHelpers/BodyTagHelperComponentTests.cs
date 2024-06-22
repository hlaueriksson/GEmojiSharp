using FluentAssertions;
using GEmojiSharp.AspNetCore;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GEmojiSharp.Tests.AspNetCore.TagHelpers
{
    public class BodyTagHelperComponentTests
    {
        [Test]
        public async Task ProcessAsync()
        {
            var subject = new BodyTagHelperComponent();

            var output = GetTagHelperOutput(":grinning:");
            await subject.ProcessAsync(null!, output);
            output.Content.GetContent().Should().Be(":grinning:".Markup());

            output = GetTagHelperOutput(":fail:");
            await subject.ProcessAsync(null!, output);
            output.Content.GetContent().Should().Be(":fail:");

            TagHelperOutput GetTagHelperOutput(string content)
            {
                return new TagHelperOutput("body", new TagHelperAttributeList(), (flag, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetContent(content);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
            }
        }
    }
}

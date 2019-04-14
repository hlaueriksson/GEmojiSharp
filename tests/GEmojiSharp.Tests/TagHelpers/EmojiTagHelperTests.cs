using System.Threading.Tasks;
using FluentAssertions;
using GEmojiSharp.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NUnit.Framework;

namespace GEmojiSharp.Tests.TagHelpers
{
    public class EmojiTagHelperTests
    {
        [Test]
        public async Task ProcessAsync()
        {
            var subject = new EmojiTagHelper();

            var output = GetTagHelperOutput(":grinning:");
            await subject.ProcessAsync(null, output);
            output.Content.GetContent().Should().Be(":grinning:".Markup());

            output = GetTagHelperOutput(":fail:");
            await subject.ProcessAsync(null, output);
            output.Content.GetContent().Should().Be(":fail:");

            TagHelperOutput GetTagHelperOutput(string content)
            {
                return new TagHelperOutput("emoji", new TagHelperAttributeList(), (flag, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetContent(content);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
            }
        }
    }
}
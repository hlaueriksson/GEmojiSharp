using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GEmojiSharp.TagHelpers
{
    public class EmojiTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();

            output.Content.SetHtmlContent(content.GetContent().MarkupContent());
        }
    }
}
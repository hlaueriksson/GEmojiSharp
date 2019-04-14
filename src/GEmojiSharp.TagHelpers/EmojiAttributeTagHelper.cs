using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GEmojiSharp.TagHelpers
{
    [HtmlTargetElement(Attributes = "emoji")]
    public class EmojiAttributeTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var alias = output.Attributes["emoji"]?.Value?.ToString();

            output.Attributes.RemoveAll("emoji");
            output.Content.SetHtmlContent(alias.Markup());
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
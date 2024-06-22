using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GEmojiSharp.AspNetCore
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting elements with an <c>emoji</c> attribute.
    /// </summary>
    [HtmlTargetElement(Attributes = "emoji")]
    public class EmojiAttributeTagHelper : TagHelper
    {
        /// <summary>
        /// Synchronously executes the <see cref="TagHelper"/> with the given <c>context</c> and <c>output</c>.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var alias = output.Attributes["emoji"]?.Value?.ToString();

            output.Attributes.RemoveAll("emoji");
            output.Content.SetHtmlContent(alias?.Markup());
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}

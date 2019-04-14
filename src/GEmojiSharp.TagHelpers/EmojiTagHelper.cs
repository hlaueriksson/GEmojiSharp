using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GEmojiSharp.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting <![CDATA[<emoji>]]> elements.
    /// </summary>
    public class EmojiTagHelper : TagHelper
    {
        /// <summary>
        /// Asynchronously executes the <see cref="TagHelper"/> with the given <c>context</c> and <c>output</c>.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>A <see cref="Task"/> that on completion updates the <c>output</c>.</returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();

            output.Content.SetHtmlContent(content.GetContent().MarkupContent());
        }
    }
}
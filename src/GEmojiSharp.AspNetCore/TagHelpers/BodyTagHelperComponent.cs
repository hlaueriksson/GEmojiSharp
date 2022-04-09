using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GEmojiSharp.AspNetCore.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting <![CDATA[<body>]]> elements.
    /// </summary>
    public class BodyTagHelperComponent : TagHelperComponent
    {
        /// <summary>
        /// Asynchronously executes the <see cref="TagHelper"/> with the given <c>context</c> and <c>output</c>.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>A <see cref="Task"/> that on completion updates the <c>output</c>.</returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (output is null) throw new ArgumentNullException(nameof(output));

            var content = await output.GetChildContentAsync().ConfigureAwait(false);

            output.Content.SetHtmlContent(content.GetContent().MarkupContent());
        }
    }
}

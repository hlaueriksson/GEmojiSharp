using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GEmojiSharp.AspNetCore
{
    /// <summary>
    /// Emoji extensions for <see cref="IHtmlHelper"/> and <see cref="IHtmlHelper{TModel}"/>.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns emojified HTML markup for the content.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="IHtmlHelper"/> instance this method extends.</param>
        /// <param name="content">The content.</param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the created HTML.</returns>
        public static IHtmlContent Emoji(this IHtmlHelper htmlHelper, string content) =>
            new HtmlString(content.MarkupContent());

        /// <summary>
        /// Returns emojified HTML markup for the <paramref name="expression"/>.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="htmlHelper">The <see cref="IHtmlHelper{TModel}"/> instance this method extends.</param>
        /// <param name="expression">An expression to be evaluated against the current model.</param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the created HTML.</returns>
        public static IHtmlContent Emoji<TModel>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Func<TModel, string> func = expression.Compile();

            return new HtmlString(func(htmlHelper.ViewData.Model).MarkupContent());
        }
    }
}

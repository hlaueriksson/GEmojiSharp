using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GEmojiSharp.TagHelpers
{
    /// <summary>
    /// Extension methods for working with emoji markup.
    /// </summary>
    public static class EmojiExtensions
    {
        private static readonly Regex EmojiRegex = new Regex(@"(:[\w+-]+:)", RegexOptions.Compiled);
        private static readonly Regex TagRegex = new Regex("<[^>]*>?", RegexOptions.RightToLeft | RegexOptions.Compiled);

        /// <summary>
        /// Gets the markup for the emoji associated with the alias.
        /// </summary>
        /// <param name="alias">The name uniquely referring to an emoji.</param>
        /// <returns>An HTML <c>string</c>.</returns>
        public static string Markup(this string alias)
        {
            var emoji = Emoji.Get(alias);

            return emoji != GEmoji.Empty ? emoji.Markup() : alias;
        }

        /// <summary>
        /// Gets the markup for the emoji.
        /// </summary>
        /// <param name="emoji">The emoji.</param>
        /// <returns>An HTML <c>string</c>.</returns>
        public static string Markup(this GEmoji emoji)
        {
            if (emoji is null || emoji == GEmoji.Empty) return string.Empty;

            return emoji.IsCustom ?
                $@"<img class=""emoji"" title="":{emoji.Alias()}:"" alt="":{emoji.Alias()}:"" src=""https://github.githubassets.com/images/icons/emoji/{emoji.Filename}.png"" height=""20"" width=""20"" align=""absmiddle"">" :
                $@"<g-emoji class=""g-emoji"" alias=""{emoji.Alias()}"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/{emoji.Filename}.png"">{emoji.Raw}</g-emoji>";
        }

        /// <summary>
        /// Gets emojified markup.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>An HTML <c>string</c>.</returns>
        public static string MarkupContent(this string content)
        {
            MatchEvaluator evaluator = EmojiMatchEvaluator;

            return EmojiRegex.Replace(content, evaluator);

            string EmojiMatchEvaluator(Match match)
            {
                var tagMatch = TagRegex.Match(content, 0, match.Index);

                if (!tagMatch.Success) return match.Value.Markup();

                var tag = tagMatch.Value;

                if (tag.StartsWith("<textarea", StringComparison.OrdinalIgnoreCase) || tag.StartsWith("<input", StringComparison.OrdinalIgnoreCase)) return match.Value.Raw();

                return match.Value.Markup();
            }
        }

        private static string Raw(this string alias)
        {
            var emoji = Emoji.Get(alias);

            return emoji != GEmoji.Empty ? emoji.Raw : alias;
        }

        private static string Alias(this GEmoji emoji)
        {
            return emoji.Aliases.First();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("GEmojiSharp.Tests")]

namespace GEmojiSharp.TagHelpers
{
    /// <summary>
    /// Extension methods for working with emoji markup.
    /// </summary>
    public static class EmojiExtensions
    {
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
            if (emoji == GEmoji.Empty) return string.Empty;

            return emoji.IsCustom ?
                $@"<img class=""emoji"" title="":{emoji.Alias()}:"" alt="":{emoji.Alias()}:"" src=""https://github.githubassets.com/images/icons/emoji/{emoji.Filename()}.png"" height=""20"" width=""20"" align=""absmiddle"">" :
                $@"<g-emoji class=""g-emoji"" alias=""{emoji.Alias()}"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/{emoji.Filename()}.png"">{emoji.Raw}</g-emoji>";
        }

        /// <summary>
        /// Gets emojified markup.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>An HTML <c>string</c>.</returns>
        public static string MarkupContent(this string content)
        {
            MatchEvaluator evaluator = EmojiMatchEvaluator;

            return Regex.Replace(content, @":([\w+-]+):", evaluator, RegexOptions.Compiled);

            string EmojiMatchEvaluator(Match match)
            {
                return match.Value.Markup();
            }
        }

        internal static string Filename(this GEmoji emoji)
        {
            if (emoji == GEmoji.Empty) return null;

            return emoji.IsCustom ?
                emoji.Alias() :
                string.Join("-", emoji.Raw.ToCodePoints().Select(x => x.ToString("x4")).Where(x => x != "fe0f" && x != "200d"));
        }

        private static string Alias(this GEmoji emoji)
        {
            return emoji.Aliases.First();
        }

        private static IEnumerable<int> ToCodePoints(this string emoji)
        {
            var utf32Bytes = Encoding.UTF32.GetBytes(emoji);
            var bytesPerCharInUtf32 = 4;
            for (var i = 0; i < utf32Bytes.Length; i += bytesPerCharInUtf32)
            {
                yield return BitConverter.ToInt32(utf32Bytes, i);
            }
        }
    }
}
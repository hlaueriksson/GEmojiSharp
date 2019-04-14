using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GEmojiSharp.TagHelpers
{
    public static class EmojiExtensions
    {
        public static string Markup(this string alias)
        {
            var emoji = Emoji.Get(alias);

            return emoji != GEmoji.Empty ? emoji.Markup() : alias;
        }

        public static string Markup(this GEmoji emoji)
        {
            if (emoji == GEmoji.Empty) return string.Empty;

            return emoji.HasEmoji ?
                $@"<g-emoji class=""g-emoji"" alias=""{emoji.Alias()}"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/{emoji.Filename()}.png"">{emoji.Emoji}</g-emoji>" :
                $@"<img class=""emoji"" title="":{emoji.Alias()}:"" alt="":{emoji.Alias()}:"" src=""https://github.githubassets.com/images/icons/emoji/{emoji.Filename()}.png"" height=""20"" width=""20"" align=""absmiddle"">";
        }

        public static string MarkupContent(this string content)
        {
            MatchEvaluator evaluator = EmojiMatchEvaluator;

            return Regex.Replace(content, @":([\w+-]+):", evaluator, RegexOptions.Compiled);

            string EmojiMatchEvaluator(Match match)
            {
                return match.Value.Markup();
            }
        }

        public static string Filename(this GEmoji emoji)
        {
            if (emoji == GEmoji.Empty) return null;

            return emoji.HasEmoji ?
                string.Join("-", emoji.Emoji.ToCodePoints().Select(x => x.ToString("x4")).Where(x => x != "fe0f" && x != "200d")) :
                emoji.Aliases.First();
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
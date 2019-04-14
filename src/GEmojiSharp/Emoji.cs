using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GEmojiSharp
{
    public static partial class Emoji
    {
        private static readonly Dictionary<string, GEmoji> Dictionary = new Dictionary<string, GEmoji>();

        static Emoji()
        {
            foreach (var emoji in All)
            {
                foreach (var alias in emoji.Aliases)
                {
                    Dictionary.Add(alias, emoji);
                }
            }
        }

        public static GEmoji Get(string alias)
        {
            var key = alias.TrimAlias();

            return Dictionary.ContainsKey(key) ? Dictionary[key] : GEmoji.Empty;
        }

        public static string Raw(string alias)
        {
            return Get(alias).Emoji;
        }

        public static string Emojify(string text)
        {
            MatchEvaluator evaluator = EmojiMatchEvaluator;

            return Regex.Replace(text, @":([\w+-]+):", evaluator, RegexOptions.Compiled);

            string EmojiMatchEvaluator(Match match)
            {
                var emoji = Get(match.Value);

                return emoji.HasEmoji ? emoji.Emoji : match.Value;
            }
        }

        public static IEnumerable<GEmoji> Find(string value)
        {
            foreach (var emoji in All)
            {
                if (emoji.Description?.Contains(value) == true ||
                    emoji.Category?.Contains(value) == true ||
                    emoji.Aliases?.Any(x => x.Contains(value)) == true ||
                    emoji.Tags?.Any(x => x.Contains(value)) == true)
                {
                    yield return emoji;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GEmojiSharp
{
    /// <summary>
    /// Provides static methods for working with emojis.
    /// </summary>
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

        /// <summary>
        /// Gets the emoji associated with the alias, or <see cref="GEmoji.Empty"/> if the alias is not found.
        /// </summary>
        /// <param name="alias">The name uniquely referring to an emoji.</param>
        /// <returns>The emoji.</returns>
        public static GEmoji Get(string alias)
        {
            if (alias is null) throw new ArgumentNullException(nameof(alias));

            var key = alias.TrimAlias();

            return Dictionary.ContainsKey(key) ? Dictionary[key] : GEmoji.Empty;
        }

        /// <summary>
        /// Gets the raw Unicode <c>string</c> of the emoji associated with the alias.
        /// </summary>
        /// <param name="alias">The name uniquely referring to an emoji.</param>
        /// <returns>The raw Unicode <c>string</c>.</returns>
        public static string Raw(string alias)
        {
            return Get(alias).Raw;
        }

        /// <summary>
        /// Replaces emoji aliases with raw Unicode strings.
        /// </summary>
        /// <param name="text">A text with emoji aliases.</param>
        /// <returns>The emojified text.</returns>
        /// <example>
        /// <code>
        /// Emoji.Emojify("it's raining :cat:s and :dog:s!"); // "it's raining 🐱s and 🐶s!"
        /// </code>
        /// </example>
        public static string Emojify(string text)
        {
            MatchEvaluator evaluator = EmojiMatchEvaluator;

            return Regex.Replace(text, @":([\w+-]+):", evaluator, RegexOptions.Compiled);

            string EmojiMatchEvaluator(Match match)
            {
                var emoji = Get(match.Value);

                return emoji.IsCustom ? match.Value : emoji.Raw;
            }
        }

        /// <summary>
        /// Returns emojis that match the <see cref="GEmoji.Description"/>, <see cref="GEmoji.Category"/>, <see cref="GEmoji.Aliases"/> or <see cref="GEmoji.Tags"/>.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>A list of emojis.</returns>
        public static IEnumerable<GEmoji> Find(string value)
        {
            return All.Where(emoji => emoji.Description?.Contains(value) == true ||
                                      emoji.Category?.Contains(value) == true ||
                                      emoji.Aliases?.Any(x => x.Contains(value)) == true ||
                                      emoji.Tags?.Any(x => x.Contains(value)) == true);
        }
    }
}

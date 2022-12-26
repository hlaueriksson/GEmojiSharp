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
        private static readonly Dictionary<string, GEmoji> AliasToGEmoji = new();
        private static readonly Dictionary<string, GEmoji> RawToGEmoji = new();

        static Emoji()
        {
            foreach (var emoji in All)
            {
                foreach (var alias in emoji.Aliases)
                {
                    AliasToGEmoji.Add(alias, emoji);
                }

                if (!emoji.IsCustom)
                {
                    RawToGEmoji.Add(emoji.Raw, emoji);
                }
            }
        }

        /// <summary>
        /// Gets the emoji associated with the alias or raw Unicode <c>string</c>.
        /// If no match is found, then <see cref="GEmoji.Empty"/> is returned.
        /// </summary>
        /// <param name="value">The emoji alias or raw Unicode <c>string</c>.</param>
        /// <returns>The emoji.</returns>
        public static GEmoji Get(string value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));

            var key = value.TrimAlias().TrimSkinToneVariants();

            return
                AliasToGEmoji.ContainsKey(key) ? AliasToGEmoji[key] :
                RawToGEmoji.ContainsKey(key) ? RawToGEmoji[key] :
                GEmoji.Empty;
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
        /// Gets the alias of the emoji represented by the raw Unicode <c>string</c>.
        /// </summary>
        /// <param name="raw">The raw Unicode <c>string</c> of the emoji.</param>
        /// <returns>The name uniquely referring to the emoji.</returns>
        public static string Alias(string raw)
        {
            return Get(raw).Alias();
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

            static string EmojiMatchEvaluator(Match match)
            {
                var emoji = Get(match.Value);

                return emoji.IsCustom ? match.Value : emoji.Raw;
            }
        }

        /// <summary>
        /// Replaces raw Unicode strings with emoji aliases.
        /// </summary>
        /// <param name="text">A text with raw Unicode strings.</param>
        /// <returns>The demojified text.</returns>
        /// <example>
        /// <code>
        /// Emoji.Demojify("it's raining 🐱s and 🐶s!"); // "it's raining :cat:s and :dog:s!"
        /// </code>
        /// </example>
        public static string Demojify(string text)
        {
            MatchEvaluator evaluator = EmojiMatchEvaluator;

            return Regex.Replace(text, RegexPattern, evaluator, RegexOptions.Compiled);

            static string EmojiMatchEvaluator(Match match)
            {
                var emoji = Get(match.Value);

                return emoji.IsCustom ? match.Value : emoji.Alias();
            }
        }

        /// <summary>
        /// Returns emojis that match the <see cref="GEmoji.Description"/>, <see cref="GEmoji.Category"/>, <see cref="GEmoji.Aliases"/> or <see cref="GEmoji.Tags"/>.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>A list of emojis.</returns>
        public static IEnumerable<GEmoji> Find(string value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));

            var text = value.TrimAlias();

            return All.Where(emoji => emoji.Description?.Contains(text) == true ||
                                      emoji.Category?.Contains(text) == true ||
                                      emoji.Aliases?.Any(x => x.Contains(text)) == true ||
                                      emoji.Tags?.Any(x => x.Contains(text)) == true).ToArray();
        }
    }
}

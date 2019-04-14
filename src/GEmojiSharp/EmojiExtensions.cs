using System.Collections.Generic;

namespace GEmojiSharp
{
    /// <summary>
    /// Extension methods for working with emojis.
    /// </summary>
    public static class EmojiExtensions
    {
        /// <summary>
        /// Gets the emoji associated with the alias, or <see cref="GEmoji.Empty"/> if the alias is not found.
        /// </summary>
        /// <param name="alias">The name uniquely referring to an emoji.</param>
        /// <returns>The emoji.</returns>
        public static GEmoji GetEmoji(this string alias)
        {
            return Emoji.Get(alias);
        }

        /// <summary>
        /// Gets the raw Unicode <c>string</c> of the emoji associated with the alias.
        /// </summary>
        /// <param name="alias">The name uniquely referring to an emoji.</param>
        /// <returns>The raw Unicode <c>string</c>.</returns>
        public static string RawEmoji(this string alias)
        {
            return Emoji.Raw(alias);
        }

        /// <summary>
        /// Replaces emoji aliases with raw Unicode strings.
        /// </summary>
        /// <param name="text">A text with emoji aliases.</param>
        /// <returns>The emojified text.</returns>
        /// <example>
        /// <code>
        /// "it's raining :cat:s and :dog:s!".Emojify(); // "it's raining 🐱s and 🐶s!"
        /// </code>
        /// </example>
        public static string Emojify(this string text)
        {
            return Emoji.Emojify(text);
        }

        /// <summary>
        /// Returns emojis that match the <see cref="GEmoji.Description"/>, <see cref="GEmoji.Category"/>, <see cref="GEmoji.Aliases"/> or <see cref="GEmoji.Tags"/>.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>A list of emojis.</returns>
        public static IEnumerable<GEmoji> FindEmojis(this string value)
        {
            return Emoji.Find(value);
        }

        internal static string TrimAlias(this string alias)
        {
            const string colon = ":";

            return alias.Replace(colon, string.Empty);
        }
    }
}
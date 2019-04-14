using System.Collections.Generic;

namespace GEmojiSharp
{
    public static class EmojiExtensions
    {
        public static GEmoji GetEmoji(this string alias)
        {
            return Emoji.Get(alias);
        }

        public static string RawEmoji(this string alias)
        {
            return Emoji.Raw(alias);
        }

        public static string Emojify(this string text)
        {
            return Emoji.Emojify(text);
        }

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
namespace GEmojiSharp
{
    public class GEmoji
    {
        public static readonly GEmoji Empty = new GEmoji { Emoji = string.Empty };

        public string Emoji { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string[] Aliases { get; set; }
        public string[] Tags { get; set; }
        public string UnicodeVersion { get; set; }
        public string IosVersion { get; set; }

        public bool HasEmoji => Emoji != Empty.Emoji;
    }
}
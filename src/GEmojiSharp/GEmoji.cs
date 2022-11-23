namespace GEmojiSharp
{
    /// <summary>
    /// Represents an emoji.
    /// </summary>
    public class GEmoji
    {
        /// <summary>Represents an empty emoji. This field is read-only.</summary>
        public static readonly GEmoji Empty = new();

        /// <summary>Raw Unicode <c>string</c> of the emoji. An empty <c>string</c> if the emoji is non-standard.</summary>
        public string Raw { get; set; } = string.Empty;

        /// <summary>The description text.</summary>
        public string? Description { get; set; }

        /// <summary>The category for the emoji as per Apple's character palette.</summary>
        public string? Category { get; set; }

        /// <summary>A list of names uniquely referring to the emoji.</summary>
        public string[] Aliases { get; set; } = System.Array.Empty<string>();

        /// <summary>A list of tags associated with the emoji. Multiple emojis can share the same tags.</summary>
        public string[]? Tags { get; set; }

        /// <summary>The Unicode spec version where the emoji first debuted.</summary>
        public string? UnicodeVersion { get; set; }

        /// <summary>The iOS version where the emoji first debuted.</summary>
        public string? IosVersion { get; set; }

        /// <summary>True if the emoji supports skin tone modifiers.</summary>
        public bool HasSkinTones { get; set; }

        /// <summary>GitHub fallback image filename.</summary>
        public string Filename { get; set; } = string.Empty;

        /// <summary>True if the emoji is not a standard emoji character.</summary>
        public bool IsCustom => Raw == Empty.Raw;
    }
}

using Microsoft.CommandPalette.Extensions.Toolkit;

namespace GEmojiSharpExtension.Models;

internal static class Icons
{
    public static IconInfo Icon { get; } = IconHelpers.FromRelativePath("Assets/icon.png");
    public static IconInfo Emojis { get; } = new IconInfo("\uE76E"); // Emoji2
    public static IconInfo Categories { get; } = new IconInfo("\uF168"); // GroupList
    public static IconInfo Transform { get; } = new IconInfo("\uE8B1"); // Shuffle
}

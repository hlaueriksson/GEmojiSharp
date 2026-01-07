using System.ComponentModel;
using GEmojiSharp;
using ModelContextProtocol.Server;

internal class EmojiTools
{
    [McpServerTool(UseStructuredContent = true)]
    [Description("Gets all emojis.")]
    public GEmojiResult[] GetAllEmojis() =>
        [.. Emoji.All.Select(x => x.ToResult())];

    [McpServerTool(UseStructuredContent = true)]
    [Description("Gets the emoji associated with the alias or raw Unicode string.")]
    public GEmojiResult GetEmoji([Description("The emoji alias or raw Unicode string.")] string value) =>
        Emoji.Get(value).ToResult();

    [McpServerTool(UseStructuredContent = true)]
    [Description("Returns emojis that match the Description, Category, Aliases or Tags.")]
    public GEmojiResult[] Find([Description("The value to search for.")] string value) =>
    [.. Emoji.Find(value).Select(x => x.ToResult())];

    [McpServerTool]
    [Description("Replaces emoji aliases with raw Unicode strings.")]
    public string Emojify([Description("A text with emoji aliases.")] string text) =>
        Emoji.Emojify(text);

    [McpServerTool]
    [Description("Replaces raw Unicode strings with emoji aliases.")]
    public string Demojify([Description("A text with raw Unicode strings.")] string text) =>
        Emoji.Demojify(text);
}

internal record GEmojiResult(string Raw, string? Description, string? Category, string[] Aliases, string[]? Tags, string[]? SkinTones, bool IsCustom);

internal static class GEmojiExtensions
{
    public static GEmojiResult ToResult(this GEmoji emoji)
    {
        return new GEmojiResult(
            emoji.Raw,
            emoji.Description,
            emoji.Category,
            emoji.Aliases,
            emoji.Tags,
            emoji.HasSkinTones ? [.. emoji.RawSkinToneVariants()] : null,
            emoji.IsCustom
        );
    }
}

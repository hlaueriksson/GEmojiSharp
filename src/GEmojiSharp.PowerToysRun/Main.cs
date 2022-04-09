using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ManagedCommon;
using Wox.Plugin;

namespace GEmojiSharp.PowerToysRun
{
    public class Main : IPlugin, IContextMenu
    {
        private string IconPath { get; set; }
        private PluginInitContext Context { get; set; }

        public string Name => "GEmoji";
        public string Description => "GitHub Emoji";

        public List<Result> Query(Query query)
        {
            if (query?.Search is null)
            {
                return new List<Result>(0);
            }

            var value = query.Search;

            if (string.IsNullOrEmpty(value))
            {
                return Emoji.All.Select(GetResult).ToList();
            }

            var emojis = Emoji.Find(value);

            if (emojis.Any())
            {
                return emojis.Select(GetResult).ToList();
            }

            var results = new List<Result>();

            if (HasAlias(value))
            {
                var result = Emoji.Emojify(value);

                results.Add(
                    new Result
                    {
                        QueryTextDisplay = value,
                        IcoPath = IconPath,
                        Title = result,
                        SubTitle = "Replace aliases in text with raw emojis",
                        ToolTipData = new ToolTipData("Emojify", $"{value}:\n{result}"),
                        Action = _ => CopyToClipboard(result),
                        ContextData = new EmojifiedString(result),
                    });
            }
            if (HasEmoji(value))
            {
                var result = Emoji.Demojify(value);

                results.Add(
                    new Result
                    {
                        QueryTextDisplay = value,
                        IcoPath = IconPath,
                        Title = result,
                        SubTitle = "Replace raw emojis in text with aliases",
                        ToolTipData = new ToolTipData("Demojify", $"{value}:\n{result}"),
                        Action = _ => CopyToClipboard(result),
                        ContextData = new DemojifiedString(result),
                    });
            }

            return results;

            Result GetResult(GEmoji emoji) => new()
            {
                QueryTextDisplay = value,
                IcoPath = IconPath,
                Title = emoji.Raw,
                SubTitle = string.Join(" ", emoji.Aliases.Select(x => x.PadAlias())),
                ToolTipData = new ToolTipData("GEmoji", $"Description: {emoji.Description}\nCategory: {emoji.Category}\nTags: {string.Join(", ", emoji.Tags ?? Enumerable.Empty<string>())}\n"),
                Action = _ => CopyToClipboard(emoji.Raw),
                ContextData = emoji,
            };

            bool HasAlias(string value)
            {
                return Regex.IsMatch(value, @":([\w+-]+):", RegexOptions.Compiled);
            }

            bool HasEmoji(string value)
            {
                return Regex.IsMatch(value, Emoji.RegexPattern, RegexOptions.Compiled);
            }
        }

        public void Init(PluginInitContext context)
        {
            Context = context;
            Context.API.ThemeChanged += OnThemeChanged;
            UpdateIconPath(Context.API.GetCurrentTheme());
        }

        public List<ContextMenuResult> LoadContextMenus(Result selectedResult)
        {
            if (selectedResult?.ContextData is GEmoji emoji)
            {
                return new List<ContextMenuResult>
                {
                    new ContextMenuResult
                    {
                        PluginName = Name,
                        Title = "Copy raw emoji (Enter)",
                        FontFamily = "Segoe MDL2 Assets",
                        Glyph = "\xE8C8", // E8C8 => Symbol: Copy
                        AcceleratorKey = Key.Enter,
                        Action = _ => CopyToClipboard(emoji.Raw),
                    },
                    new ContextMenuResult
                    {
                        PluginName = Name,
                        Title = "Copy emoji aliases (Ctrl+C)",
                        FontFamily = "Segoe MDL2 Assets",
                        Glyph = "\xF413", // F413 => Symbol: CopyTo
                        AcceleratorKey = Key.C,
                        AcceleratorModifiers = ModifierKeys.Control,
                        Action = _ => CopyToClipboard(string.Join(string.Empty, emoji.Aliases.Select(x => x.PadAlias()))),
                    },
                };
            }

            if (selectedResult?.ContextData is EmojifiedString emojified)
            {
                return new List<ContextMenuResult>
                {
                    new ContextMenuResult
                    {
                        PluginName = Name,
                        Title = "Copy emojified text (Enter)",
                        FontFamily = "Segoe MDL2 Assets",
                        Glyph = "\xE8C8", // E8C8 => Symbol: Copy
                        AcceleratorKey = Key.Enter,
                        Action = _ => CopyToClipboard(emojified.Value),
                    },
                };
            }

            if (selectedResult?.ContextData is DemojifiedString demojified)
            {
                return new List<ContextMenuResult>
                {
                    new ContextMenuResult
                    {
                        PluginName = Name,
                        Title = "Copy demojified text (Enter)",
                        FontFamily = "Segoe MDL2 Assets",
                        Glyph = "\xE8C8", // E8C8 => Symbol: Copy
                        AcceleratorKey = Key.Enter,
                        Action = _ => CopyToClipboard(demojified.Value),
                    },
                };
            }

            return new List<ContextMenuResult>(0);
        }

        private static bool CopyToClipboard(string value)
        {
            Clipboard.SetText(value);
            return true;
        }

        private void UpdateIconPath(Theme theme)
        {
            if (theme == Theme.Light || theme == Theme.HighContrastWhite)
            {
                IconPath = "images/gemoji.light.png";
            }
            else
            {
                IconPath = "images/gemoji.dark.png";
            }
        }

        private void OnThemeChanged(Theme currentTheme, Theme newTheme)
        {
            UpdateIconPath(newTheme);
        }
    }

    internal record EmojifiedString(string Value);

    internal record DemojifiedString(string Value);
}

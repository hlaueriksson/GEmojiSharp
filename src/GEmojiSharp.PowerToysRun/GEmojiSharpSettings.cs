using Community.PowerToys.Run.Plugin.Update;
using Microsoft.PowerToys.Settings.UI.Library;

namespace GEmojiSharp.PowerToysRun
{
    /// <summary>
    /// Plugin settings.
    /// </summary>
    public class GEmojiSharpSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GEmojiSharpSettings"/> class.
        /// </summary>
        public GEmojiSharpSettings()
        {
            Update = new PluginUpdateSettings
            {
                ResultScore = 100,
            };
        }

        /// <summary>
        /// Plugin update settings.
        /// </summary>
        public PluginUpdateSettings Update { get; set; }

        internal IEnumerable<PluginAdditionalOption> GetAdditionalOptions() => Update.GetAdditionalOptions();

        internal void SetAdditionalOptions(IEnumerable<PluginAdditionalOption> additionalOptions) => Update.SetAdditionalOptions(additionalOptions);
    }
}

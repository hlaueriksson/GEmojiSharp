using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace GEmojiSharp.Tests
{
    public class GenerateTests
    {
        [Test, Explicit]
        public async Task Write()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/github/gemoji/master/db/emoji.json");
            var json = await response.Content.ReadAsStringAsync();

            var emojis = JArray.Parse(json);

            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "hlaueriksson");
            response = await client.GetAsync("https://api.github.com/emojis");
            json = await response.Content.ReadAsStringAsync();

            var supportedEmojis = JObject.Parse(json);

            var result = new StringBuilder();

            foreach (var emoji in emojis)
            {
                var e = emoji.Value<string>("emoji") ?? string.Empty;
                var d = emoji.Value<string>("description");
                var c = emoji.Value<string>("category");
                var a = emoji["aliases"].Values<string>();
                var t = emoji["tags"].Values<string>();
                var uv = emoji.Value<string>("unicode_version");
                var iv = emoji.Value<string>("ios_version");

                a = a.Where(x => supportedEmojis[x] != null);
                if (!a.Any()) continue;

                var url = supportedEmojis[a.First()].Value<string>();
                var filename = url
                    .Replace("https://github.githubassets.com/images/icons/emoji/unicode/", string.Empty)
                    .Replace(".png?v8", string.Empty);

                result.Append("            ");
                result.Append("new GEmoji { ");
                result.Append($"Raw = \"{e}\"");
                if (d != null) result.Append($", Description = \"{d}\"");
                if (c != null) result.Append($", Category = \"{c}\"");
                if (a.Any()) result.Append($", Aliases = new[] {{ {string.Join(", ", a.Select(x => "\"" + x + "\""))} }}");
                if (t.Any()) result.Append($", Tags = new[] {{ {string.Join(", ", t.Select(x => "\"" + x + "\""))} }}");
                if (uv != null) result.Append($", UnicodeVersion = \"{uv}\"");
                if (iv != null) result.Append($", IosVersion = \"{iv}\"");
                result.Append($", Filename = \"{filename}\"");
                result.AppendLine(" },");
            }

            Console.WriteLine(result.ToString());
        }

        [Test, Explicit]
        public void Versions()
        {
            var versions = Emoji.All.Select(x => x.UnicodeVersion).Distinct().Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToDouble(x, CultureInfo.InvariantCulture)).OrderBy(x => x);

            foreach (var version in versions)
            {
                Console.WriteLine(version.ToString("##.0", CultureInfo.InvariantCulture));
            }
        }

        [Test, Explicit]
        public async Task Emoji_All_vs_Api()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "hlaueriksson");
            var response = await client.GetAsync("https://api.github.com/emojis");
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            foreach (var emoji in Emoji.All)
            {
                foreach (var alias in emoji.Aliases)
                {
                    var token = json[alias];

                    token.Should().NotBeNull($":{alias}:");
                }
            }
        }

        [Test, Explicit]
        public async Task Api_vs_Emoji_All()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "hlaueriksson");
            var response = await client.GetAsync("https://api.github.com/emojis");
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            foreach (var value in json.PropertyValues())
            {
                var emoji = Emoji.Get(value.Path);

                if (emoji == GEmoji.Empty) Console.WriteLine($":{value.Path}:");
            }
        }

        [Test, Explicit]
        public async Task Filename_vs_Api()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "hlaueriksson");
            var response = await client.GetAsync("https://api.github.com/emojis");
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            foreach (var emoji in Emoji.All)
            {
                var token = json[emoji.Aliases.First()];
                var filename = token.Value<string>()
                    .Replace("https://github.githubassets.com/images/icons/emoji/unicode/", string.Empty)
                    .Replace("https://github.githubassets.com/images/icons/emoji/", string.Empty)
                    .Replace(".png?v8", string.Empty);

                emoji.Filename.Should().Be(filename);
            }
        }
    }
}
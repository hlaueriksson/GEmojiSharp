using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using GEmojiSharp.TagHelpers;
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

                result.Append("            ");
                result.Append("new GEmoji { ");
                result.Append($"Markup = \"{e}\"");
                if (d != null) result.Append($", Description = \"{d}\"");
                if (c != null) result.Append($", Category = \"{c}\"");
                if (a.Any()) result.Append($", Aliases = new[] {{ {string.Join(", ", a.Select(x => "\"" + x + "\""))} }}");
                if (t.Any()) result.Append($", Tags = new[] {{ {string.Join(", ", t.Select(x => "\"" + x + "\""))} }}");
                if (uv != null) result.Append($", UnicodeVersion = \"{uv}\"");
                if (iv != null) result.Append($", IosVersion = \"{iv}\"");
                result.AppendLine(" },");
            }

            Console.WriteLine(result.ToString());
        }

        [Test, Explicit]
        public async Task Filename()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://github.com/hlaueriksson/github-emoji/blob/master/README.md");
            var html = await response.Content.ReadAsStringAsync();

            var regex = new Regex(@"<g-emoji class=""g-emoji"" alias=""(.*)"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/(.*).png"">.*</g-emoji>", RegexOptions.Compiled);
            var matches = regex.Matches(html);
            foreach (Match match in matches)
            {
                var alias = match.Groups[1].Value;
                var filename = match.Groups[2].Value;

                var emoji = Emoji.Get(alias);

                emoji.Filename().Should().Be(filename);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace GEmojiSharp.Tests
{
    [Explicit]
    public class GenerateTests
    {
        [Test, Category("Generate")]
        public async Task Generate_All()
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
                var e = emoji.Value<string>("emoji");
                var d = emoji.Value<string>("description");
                var c = emoji.Value<string>("category");
                var a = emoji["aliases"]!.Values<string>();
                var t = emoji["tags"]!.Values<string>();
                var uv = emoji.Value<string>("unicode_version");
                var iv = emoji.Value<string>("ios_version");
                var st = emoji.Value<bool?>("skin_tones");

                a = a.Where(x => supportedEmojis[x!] != null).ToList();
                if (!a.Any()) continue;

                var url = supportedEmojis[a.First()!]!.Value<string>();
                var filename = url!
                    .Replace("https://github.githubassets.com/images/icons/emoji/unicode/", string.Empty)
                    .Replace(".png?v8", string.Empty);

                result.Append("            ");
                result.Append("new GEmoji { ");
                result.Append($"Raw = \"{e}\"");
                if (d != null) result.Append($", Description = \"{d}\"");
                if (c != null) result.Append($", Category = \"{c}\"");
                if (a.Any()) result.Append($", Aliases = new[] {{ {string.Join(", ", a.Select(x => "\"" + x + "\""))} }}");
                if (t.Any()) result.Append($", Tags = new[] {{ {string.Join(", ", t.Select(x => "\"" + x + "\""))} }}");
                if (!string.IsNullOrEmpty(uv)) result.Append($", UnicodeVersion = \"{uv}\"");
                if (iv != null) result.Append($", IosVersion = \"{iv}\"");
                if (st == true) result.Append($", HasSkinTones = true");
                result.Append($", Filename = \"{filename}\"");
                result.AppendLine(" },");
            }

            var path = Directory.GetCurrentDirectory() + @"..\..\..\..\..\..\src\GEmojiSharp\Emoji.g.All.cs";
            var contents = File
                .ReadAllLines(path)
                .Where(x => !x.Contains("new GEmoji { Raw"))
                .ToList();
            var index = contents.FindIndex(x => x.Contains("new GEmoji { Aliases"));
            contents.InsertRange(index, result.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
            File.WriteAllLines(path, contents);
        }

        [Test, Category("Generate")]
        public void Generate_RegexPattern()
        {
            var codes = new List<string>();

            foreach (var emoji in Emoji.All)
            {
                if (emoji.IsCustom) continue;
                var chars = emoji.Raw.Select(c => $@"\u{(ushort)c:x4}");
                codes.Add(string.Join(string.Empty, chars));
            }

            var result = new StringBuilder();

            foreach (var code in codes.OrderByDescending(x => x.Length))
            {
                if (result.Length > 0) result.Append("|");
                result.Append(code);
            }
            result.Insert(0, "(");
            result.Append(")");

            var path = Directory.GetCurrentDirectory() + @"..\..\..\..\..\..\src\GEmojiSharp\Emoji.g.RegexPattern.cs";
            var contents = File.ReadAllLines(path);
            var index = Array.FindIndex(contents, x => x.Contains("public const string RegexPattern"));
            contents[index] = $"        public const string RegexPattern = @\"{result}\";";
            File.WriteAllLines(path, contents);
        }

        [Test]
        public void Versions()
        {
            var versions = Emoji.All.Select(x => x.UnicodeVersion).Distinct().Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToDouble(x, CultureInfo.InvariantCulture)).OrderBy(x => x);

            foreach (var version in versions)
            {
                Console.WriteLine(version.ToString("##.0", CultureInfo.InvariantCulture));
            }
        }

        [Test, Category("Integration")]
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

        [Test, Category("Integration")]
        public async Task Api_vs_Emoji_All()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "hlaueriksson");
            var response = await client.GetAsync("https://api.github.com/emojis");
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            foreach (var token in json.PropertyValues())
            {
                var emoji = Emoji.Get(token.Path);

                emoji.Should().NotBe(GEmoji.Empty, $":{token.Path}:");
                //if (emoji == GEmoji.Empty) Console.WriteLine($":{token.Path}:");
            }
        }

        [Test, Category("Integration")]
        public async Task Filename_vs_Api()
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
                    var filename = token!.Value<string>()!
                        .Replace("https://github.githubassets.com/images/icons/emoji/unicode/", string.Empty)
                        .Replace("https://github.githubassets.com/images/icons/emoji/", string.Empty)
                        .Replace(".png?v8", string.Empty);

                    emoji.Filename.Should().Be(filename);
                }
            }
        }

        [Test, Category("Integration")]
        public async Task Raw_vs_Available()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://github.com/hlaueriksson/github-emoji/blob/master/available.md");
            var content = await page.ContentAsync();

            foreach (var emoji in Emoji.All.Where(x => !x.IsCustom))
            {
                foreach (var alias in emoji.Aliases)
                {
                    var regex = new Regex($"<tr>\n<td>(.*)</td>\n<td><code>:{Regex.Escape(alias)}:</code></td>\n</tr>");
                    var match = regex.Match(content);

                    match.Success.Should().BeTrue($":{alias}:");

                    var raw = match.Groups[1].Value;
                    //<g-emoji class="g-emoji" alias="relaxed">☺️</g-emoji>
                    raw = Regex.Replace(raw, $"<g-emoji class=\"g-emoji\" alias=\".*\">", string.Empty);
                    raw = raw.Replace("</g-emoji>", string.Empty);

                    emoji.Raw.Should().Be(raw, $":{alias}:");
                    //if (emoji.Raw != raw) Console.WriteLine($":{alias}:");
                }
            }
        }

        [Test, Category("Integration")]
        public void RegexPattern_vs_Emoji_All()
        {
            var regex = new Regex(Emoji.RegexPattern, RegexOptions.Compiled);

            foreach (var emoji in Emoji.All.Where(x => !x.IsCustom))
            {
                var match = regex.Match(emoji.Raw);

                match.Success.Should().BeTrue($":{emoji.Aliases.First()}:");

                var raw = match.Groups[1].Value;

                emoji.Raw.Should().Be(raw, $":{emoji.Aliases.First()}:");
            }
        }

        [Test, Category("Integration")]
        public void Demojify_vs_Emoji_All()
        {
            foreach (var emoji in Emoji.All.Where(x => !x.IsCustom))
            {
                emoji.Raw.Demojify().Should().Be($":{emoji.Aliases.First()}:");
            }

            var shuffledEmojis = Emoji.All.Where(x => !x.IsCustom).OrderBy(a => Guid.NewGuid()).ToList();
            var text = string.Join(string.Empty, shuffledEmojis.Select(x => x.Raw));
            var result = text.Demojify();
            var expected = string.Join(string.Empty, shuffledEmojis.Select(x => $":{x.Aliases.First()}:"));
            result.Should().Be(expected);
        }

        [Test, Category("Integration")]
        public async Task Verify_db()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/github/gemoji/master/db/emoji.json");
            var json = await response.Content.ReadAsStringAsync();

            await VerifyJson(json);
        }

        [Test, Category("Integration")]
        public async Task Verify_api()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "hlaueriksson");
            var response = await client.GetAsync("https://api.github.com/emojis");
            var json = await response.Content.ReadAsStringAsync();

            await VerifyJson(json);
        }

        [Test, Category("Integration")]
        public async Task Verify_versions()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://pages.github.com/versions.json");
            var json = await response.Content.ReadAsStringAsync();

            await VerifyJson(json);
        }
    }
}

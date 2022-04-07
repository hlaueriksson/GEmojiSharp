using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using static System.Console;

namespace GEmojiSharp.Tests
{
    public class Documentation
    {
        [Test]
        public void Emoji_()
        {
            WriteLine(Emoji.Get(":tada:").Raw); // 🎉
            WriteLine(Emoji.Get("🎉").Alias()); // :tada:
            WriteLine(Emoji.Raw(":tada:")); // 🎉
            WriteLine(Emoji.Emojify(":tada: initial commit")); // 🎉 initial commit
            WriteLine(Emoji.Demojify("🎉 initial commit")); // :tada: initial commit
            WriteLine(Emoji.Find("party popper").First().Raw); // 🎉
        }

        [Test]
        public void Extensions_()
        {
            WriteLine(":tada:".GetEmoji().Raw); // 🎉
            WriteLine("🎉".GetEmoji().Alias()); // :tada:
            WriteLine(":tada:".RawEmoji()); // 🎉
            WriteLine("🎉".EmojiAlias()); // :tada:
            WriteLine(":tada: initial commit".Emojify()); // 🎉 initial commit
            WriteLine("🎉 initial commit".Demojify()); // :tada: initial commit
            WriteLine("party popper".FindEmojis().First().Raw); // 🎉
        }

        // Skin tones not supported; https://github.com/github/gemoji/pull/165
        private const string LoremIpsum = "Lorem 😂😂 ipsum 🕵️‍♂️dolor sit✍️ amet, consectetur adipiscing😇😇🤙 elit, sed do eiusmod🥰 tempor 😤😤🏳️‍🌈incididunt ut 👏labore 👏et👏 dolore 👏magna👏 aliqua. Ut enim ad minim 🐵✊🏿veniam,❤️😤😫😩💦💦 quis nostrud 👿🤮exercitation ullamco 🧠👮🏿‍♀️🅱️laboris nisi ut aliquip❗️ ex ea commodo consequat. 💯Duis aute💦😂😂😂 irure dolor 👳🏻‍♂️🗿in reprehenderit 🤖👻👎in voluptate velit esse cillum dolore 🙏🙏eu fugiat🤔 nulla pariatur. 🙅‍♀️🙅‍♀️Excepteur sint occaecat🤷‍♀️🤦‍♀️ cupidatat💅 non💃 proident,👨‍👧 sunt🤗 in culpa😥😰😨 qui officia🤩🤩 deserunt mollit 🧐anim id est laborum.🤔🤔";

        [Test]
        public void Get_all_emojis_from_string_with_regex()
        {
            var matches = Regex.Matches(LoremIpsum, Emoji.RegexPattern, RegexOptions.Compiled);
            var result = string.Join(string.Empty, matches.Select(x => x.Value));
            WriteLine(result);
        }

        [Test]
        public void Remove_all_emojis_from_string_with_regex()
        {
            var result = Regex.Replace(LoremIpsum, Emoji.RegexPattern, string.Empty, RegexOptions.Compiled);
            WriteLine(result);
        }
    }
}

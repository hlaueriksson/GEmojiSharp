using System.Linq;
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
            WriteLine(Emoji.Raw(":tada:")); // 🎉
            WriteLine(Emoji.Emojify(":tada: initial commit")); // 🎉 initial commit
            WriteLine(Emoji.Find("party popper").First().Raw); // 🎉
        }

        [Test]
        public void Extensions_()
        {
            WriteLine(":tada:".GetEmoji().Raw); // 🎉
            WriteLine(":tada:".RawEmoji()); // 🎉
            WriteLine(":tada: initial commit".Emojify()); // 🎉 initial commit
            WriteLine("party popper".FindEmojis().First().Raw); // 🎉
        }
    }
}
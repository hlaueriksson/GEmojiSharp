using FluentAssertions;
using GEmojiSharp.PowerToysRun;
using NUnit.Framework;
using Wox.Plugin;

namespace GEmojiSharp.Tests.PowerToysRun
{
    public class MainTests
    {
        [Test]
        public void Query_emojis()
        {
            var subject = new Main();

            subject.Query(null!).Should().BeEmpty();

            subject.Query(new Query("")).Should().NotBeEmpty();

            subject.Query(new Query("globe showing")).Should()
                .Contain(x => x.Title == "🌍")
                .And.Contain(x => x.Title == "🌎")
                .And.Contain(x => x.Title == "🌏");

            subject.Query(new Query("tada")).Should().ContainSingle(x => x.Title == "🎉");
        }

        [Test]
        public void Query_Emojify()
        {
            var subject = new Main();

            subject.Query(new Query("Hello, :earth_africa:")).Should().ContainSingle(x => x.Title == "Hello, 🌍");
        }

        [Test]
        public void Query_Demojify()
        {
            var subject = new Main();

            subject.Query(new Query("Hello, 🌍")).Should().ContainSingle(x => x.Title == "Hello, :earth_africa:");
        }

        [Test]
        public void LoadContextMenus_GEmoji()
        {
            var subject = new Main();

            subject.LoadContextMenus(new Result()).Should().BeEmpty();

            var result = new Result { ContextData = Emoji.Get("tada") };
            subject.LoadContextMenus(result).Should()
                .Contain(x => x.Title == "Copy raw emoji (Enter)")
                .And.Contain(x => x.Title == "Copy emoji aliases (Ctrl+C)");
        }

        [Test]
        public void LoadContextMenus_EmojifiedString()
        {
            var subject = new Main();
            var result = new Result { ContextData = new EmojifiedString("Hello, 🌍") };

            subject.LoadContextMenus(result).Should().Contain(x => x.Title == "Copy emojified text (Enter)");
        }

        [Test]
        public void LoadContextMenus_DemojifiedString()
        {
            var subject = new Main();
            var result = new Result { ContextData = new DemojifiedString("Hello, :earth_africa:") };

            subject.LoadContextMenus(result).Should().Contain(x => x.Title == "Copy demojified text (Enter)");
        }
    }
}

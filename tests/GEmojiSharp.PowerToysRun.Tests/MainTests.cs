using FluentAssertions;
using GEmojiSharp.PowerToysRun;
using NUnit.Framework;
using Wox.Plugin;

namespace GEmojiSharp.Tests.PowerToysRun
{
    public class MainTests
    {
        private Main _subject = null!;

        [SetUp]
        public void SetUp()
        {
            _subject = new Main(new GEmojiSharpSettings());
        }

        [Test]
        public void Query_emojis()
        {
            _subject.Query(null!).Should().BeEmpty();

            _subject.Query(new Query("")).Should().NotBeEmpty();

            _subject.Query(new Query("globe showing")).Should()
                .Contain(x => x.Title == "🌍")
                .And.Contain(x => x.Title == "🌎")
                .And.Contain(x => x.Title == "🌏");

            _subject.Query(new Query("tada")).Should().ContainSingle(x => x.Title == "🎉");
        }

        [Test]
        public void Query_Emojify()
        {
            _subject.Query(new Query("Hello, :earth_africa:")).Should().ContainSingle(x => x.Title == "Hello, 🌍");
        }

        [Test]
        public void Query_Demojify()
        {
            _subject.Query(new Query("Hello, 🌍")).Should().ContainSingle(x => x.Title == "Hello, :earth_africa:");
        }

        [Test]
        public void LoadContextMenus_GEmoji()
        {
            _subject.LoadContextMenus(new Result()).Should().BeEmpty();

            var result = new Result { ContextData = Emoji.Get("tada") };
            _subject.LoadContextMenus(result).Should()
                .Contain(x => x.Title == "Copy raw emoji (Enter)")
                .And.Contain(x => x.Title == "Copy emoji aliases (Ctrl+C)");

            result = new Result { ContextData = Emoji.Get("wave") };
            _subject.LoadContextMenus(result).Should()
                .Contain(x => x.Title == "Copy raw emoji (Enter)")
                .And.Contain(x => x.Title == "Copy emoji aliases (Ctrl+C)")
                .And.Contain(x => x.Title == "Copy raw emoji skin tone variants (Ctrl+Enter)");
        }

        [Test]
        public void LoadContextMenus_EmojifiedString()
        {
            var result = new Result { ContextData = new EmojifiedString("Hello, 🌍") };
            _subject.LoadContextMenus(result).Should().Contain(x => x.Title == "Copy emojified text (Enter)");
        }

        [Test]
        public void LoadContextMenus_DemojifiedString()
        {
            var result = new Result { ContextData = new DemojifiedString("Hello, :earth_africa:") };
            _subject.LoadContextMenus(result).Should().Contain(x => x.Title == "Copy demojified text (Enter)");
        }
    }
}

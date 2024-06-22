using FluentAssertions;
using GEmojiSharp.AspNetCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using NSubstitute;

namespace GEmojiSharp.Tests.AspNetCore.HtmlHelpers
{
    public class HtmlHelperExtensionsTests
    {
        [Test]
        public void Emoji()
        {
            var subject = Substitute.For<IHtmlHelper>();

            subject.Emoji("<p>Hello, :earth_africa:</p>").ToString().Should().Be(@"<p>Hello, <g-emoji class=""g-emoji"" alias=""earth_africa"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/1f30d.png"">🌍</g-emoji></p>");
            subject.Emoji("<p>Hello, :fail:</p>").ToString().Should().Be(@"<p>Hello, :fail:</p>");

            subject.Emoji("<textarea>Hello, :earth_africa:</textarea>").ToString().Should().Be(@"<textarea>Hello, 🌍</textarea>");
            subject.Emoji(@"<input type=""text"" value=""Hello, :earth_africa:"">").ToString().Should().Be(@"<input type=""text"" value=""Hello, 🌍"">");

            subject.Emoji("<body><form><div>:book: :pencil2:<br /><textarea>:heart: :+1:</textarea></div></form></body>").ToString().Should().Be(@"<body><form><div><g-emoji class=""g-emoji"" alias=""book"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/1f4d6.png"">📖</g-emoji> <g-emoji class=""g-emoji"" alias=""pencil2"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/270f.png"">✏️</g-emoji><br /><textarea>❤️ 👍</textarea></div></form></body>");
        }
    }
}

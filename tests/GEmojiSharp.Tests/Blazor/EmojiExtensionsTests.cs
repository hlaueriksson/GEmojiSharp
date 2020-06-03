using FluentAssertions;
using GEmojiSharp.Blazor;
using NUnit.Framework;

namespace GEmojiSharp.Tests.Blazor
{
    public class EmojiExtensionsTests
    {
        [Test]
        public void Markup()
        {
            ":grinning:".Markup().Should().Be(@"<g-emoji class=""g-emoji"" alias=""grinning"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/1f600.png"">😀</g-emoji>");
            ":octocat:".Markup().Should().Be(@"<img class=""emoji"" title="":octocat:"" alt="":octocat:"" src=""https://github.githubassets.com/images/icons/emoji/octocat.png"" height=""20"" width=""20"" align=""absmiddle"">");
            ":fail:".Markup().Should().Be(":fail:");
        }

        [Test]
        public void Markup_GEmoji()
        {
            ":satisfied:".GetEmoji().Markup().Should().Be(@"<g-emoji class=""g-emoji"" alias=""laughing"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/1f606.png"">😆</g-emoji>");
            ":fail:".GetEmoji().Markup().Should().BeEmpty();
        }

        [Test]
        public void MarkupContent()
        {
            "<p>Hello, :earth_africa:</p>".MarkupContent().Should().Be(@"<p>Hello, <g-emoji class=""g-emoji"" alias=""earth_africa"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/1f30d.png"">🌍</g-emoji></p>");
            "<p>Hello, :fail:</p>".MarkupContent().Should().Be(@"<p>Hello, :fail:</p>");

            "<textarea>Hello, :earth_africa:</textarea>".MarkupContent().Should().Be(@"<textarea>Hello, 🌍</textarea>");
            @"<input type=""text"" value=""Hello, :earth_africa:"">".MarkupContent().Should().Be(@"<input type=""text"" value=""Hello, 🌍"">");

            "<body><form><div>:book: :pencil2:<br /><textarea>:heart: :+1:</textarea></div></form></body>".MarkupContent().Should().Be(@"<body><form><div><g-emoji class=""g-emoji"" alias=""book"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/1f4d6.png"">📖</g-emoji> <g-emoji class=""g-emoji"" alias=""pencil2"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/270f.png"">✏️</g-emoji><br /><textarea>❤️ 👍</textarea></div></form></body>");
        }
    }
}
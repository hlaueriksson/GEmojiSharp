using FluentAssertions;
using GEmojiSharp.TagHelpers;
using NUnit.Framework;

namespace GEmojiSharp.Tests.TagHelpers
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
            "Hello, :earth_africa:".MarkupContent().Should().Be(@"Hello, <g-emoji class=""g-emoji"" alias=""earth_africa"" fallback-src=""https://github.githubassets.com/images/icons/emoji/unicode/1f30d.png"">🌍</g-emoji>");
            "Hello, :fail:".MarkupContent().Should().Be("Hello, :fail:");
        }

        [Test]
        public void Filename()
        {
            ":grinning:".GetEmoji().Filename().Should().Be("1f600");
            ":octocat:".GetEmoji().Filename().Should().Be("octocat");
            ":one:".GetEmoji().Filename().Should().Be("0031-20e3");
            ":fail:".GetEmoji().Filename().Should().BeNull();
        }
    }
}
using System.Diagnostics;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TextCopy;
using static System.Environment;

namespace GEmojiSharp.Tests.DotnetTool
{
    [Explicit, Category("Integration")]
    public class IntegrationTests
    {
        [Test]
        public void Help()
        {
            var (ExitCode, StandardOutput, StandardError) = Run("--help");
            ExitCode.Should().Be(0);
            StandardError.Should().BeEmpty();
            StandardOutput.Should().NotBeEmpty();
        }

        [Test]
        public void Version()
        {
            var (ExitCode, StandardOutput, StandardError) = Run("--version");
            ExitCode.Should().Be(0);
            StandardError.Should().BeEmpty();
            StandardOutput.Should().NotBeEmpty();
        }

        [Test]
        public void Raw()
        {
            Run("raw earth").ShouldHaveOutput($"🌍{NewLine}🌎{NewLine}🌏{NewLine}");
            Run("r earth").ShouldHaveOutput($"🌍{NewLine}🌎{NewLine}🌏{NewLine}");
            Run("r globe showing").ShouldHaveOutput($"🌍{NewLine}🌎{NewLine}🌏{NewLine}");
            Run("r \"globe showing\"").ShouldHaveOutput($"🌍{NewLine}🌎{NewLine}🌏{NewLine}");

            Run("r earth --copy");
            ClipboardService.GetText().Should().Be("🌍🌎🌏");
            Run("r tada -c");
            ClipboardService.GetText().Should().Be("🎉");

            Run("r -h").StandardOutput.Should()
                .Contain("Get raw emojis")
                .And.Contain("Find emojis via description, category, alias or tag");
        }

        [Test]
        public void Alias()
        {
            Run("alias earth").ShouldHaveOutput($":earth_africa:{NewLine}:earth_americas:{NewLine}:earth_asia:{NewLine}");
            Run("a earth").ShouldHaveOutput($":earth_africa:{NewLine}:earth_americas:{NewLine}:earth_asia:{NewLine}");
            Run("a globe showing").ShouldHaveOutput($":earth_africa:{NewLine}:earth_americas:{NewLine}:earth_asia:{NewLine}");
            Run("a \"globe showing\"").ShouldHaveOutput($":earth_africa:{NewLine}:earth_americas:{NewLine}:earth_asia:{NewLine}");

            Run("a earth --copy");
            ClipboardService.GetText().Should().Be(":earth_africa::earth_americas::earth_asia:");
            Run("a tada -c");
            ClipboardService.GetText().Should().Be(":tada:");

            Run("a -h").StandardOutput.Should()
                .Contain("Get emoji aliases")
                .And.Contain("Find emojis via description, category, alias or tag");
        }

        [Test]
        public void Emojify()
        {
            Run("emojify Hello, :earth_africa:!").ShouldHaveOutput($"Hello, 🌍!{NewLine}");
            Run("e Hello, :earth_africa:!").ShouldHaveOutput($"Hello, 🌍!{NewLine}");
            Run("e \"Hello, :earth_africa:!\"").ShouldHaveOutput($"Hello, 🌍!{NewLine}");

            Run("e Hello, :earth_africa:! --copy");
            ClipboardService.GetText().Should().Be("Hello, 🌍!");
            Run("e :tada: -c");
            ClipboardService.GetText().Should().Be("🎉");

            Run("e -h").StandardOutput.Should()
                .Contain("Replace aliases in text with raw emojis")
                .And.Contain("A text with emoji aliases");
        }

        [Test]
        public void Demojify()
        {
            Run("demojify Hello, 🌍!").ShouldHaveOutput($"Hello, :earth_africa:!{NewLine}");
            Run("d Hello, 🌍!").ShouldHaveOutput($"Hello, :earth_africa:!{NewLine}");
            Run("d \"Hello, 🌍!\"").ShouldHaveOutput($"Hello, :earth_africa:!{NewLine}");

            Run("d Hello, 🌍! --copy");
            ClipboardService.GetText().Should().Be("Hello, :earth_africa:!");
            Run("d 🎉 -c");
            ClipboardService.GetText().Should().Be(":tada:");

            Run("d -h").StandardOutput.Should()
                .Contain("Replace raw emojis in text with aliases")
                .And.Contain("A text with raw emojis");
        }

        [Test]
        public void Export()
        {
            Run("export tada").StandardOutput.Should().Contain("\"Raw\": \"🎉\"");
            Run("export tada --format json").StandardOutput.Should().Contain("\"Raw\": \"🎉\"");
            Run("export tada -f json").StandardOutput.Should().Contain("\"Raw\": \"🎉\"");
            Run("export tada -f JSON").StandardOutput.Should().Contain("\"Raw\": \"🎉\"");

            Run("export tada --format toml").StandardOutput.Should().Contain("raw = \"🎉\"");
            Run("export tada -f toml").StandardOutput.Should().Contain("raw = \"🎉\"");
            Run("export tada -f TOML").StandardOutput.Should().Contain("raw = \"🎉\"");

            Run("export tada --format xml").StandardOutput.Should().Contain("<Raw>🎉</Raw>");
            Run("export tada -f xml").StandardOutput.Should().Contain("<Raw>🎉</Raw>");
            Run("export tada -f XML").StandardOutput.Should().Contain("<Raw>🎉</Raw>");

            Run("export tada --format yaml").StandardOutput.Should().Contain("- Raw: \"\\U0001F389\"");
            Run("export tada -f yaml").StandardOutput.Should().Contain("- Raw: \"\\U0001F389\"");
            Run("export tada -f YAML").StandardOutput.Should().Contain("- Raw: \"\\U0001F389\"");

            Run("export tada --copy");
            ClipboardService.GetText().Should().Contain("\"Raw\": \"🎉\"");
            Run("export tada -c");
            ClipboardService.GetText().Should().Contain("\"Raw\": \"🎉\"");

            Run("export -h").StandardOutput.Should()
                .Contain("Export emoji data")
                .And.Contain("Find emojis via description, category, alias or tag")
                .And.Contain("Format the data as <json|toml|xml|yaml>");
        }

        static (int ExitCode, string StandardOutput, string StandardError) Run(string args)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "GEmojiSharp.DotnetTool.exe",
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };

            using (var exeProcess = Process.Start(startInfo))
            {
                exeProcess!.WaitForExit();
                return new(exeProcess.ExitCode, exeProcess.StandardOutput.ReadToEnd(), exeProcess.StandardError.ReadToEnd());
            }
        }
    }

    static class ProcessShouldExtensions
    {
        public static void ShouldHaveOutput(this (int ExitCode, string StandardOutput, string StandardError) result, string output)
        {
            result.ExitCode.Should().Be(0);
            result.StandardError.Should().BeEmpty();
            result.StandardOutput.Should().Be(output);
        }
    }
}

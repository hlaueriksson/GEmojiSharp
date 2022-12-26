using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace GEmojiSharp.Benchmark
{
    public class RegexBenchmark
    {
        private Regex _regex = null!;
        private string _value = null!;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _regex = new Regex(Emoji.RegexPattern, RegexOptions.Compiled);
            _value = "🎉";
        }

        [Benchmark]
        public bool Regex_IsMatch_instance()
        {
            return _regex.IsMatch(_value);
        }

        [Benchmark]
        public bool Regex_IsMatch_static()
        {
            return Regex.IsMatch(_value, Emoji.RegexPattern, RegexOptions.Compiled);
        }
    }
}

using System.CommandLine;
using System.Text;
using GEmojiSharp;
using TextCopy;

Console.OutputEncoding = Encoding.UTF8;

var argument = new Argument<string[]>("args", "Find emojis via description, category, alias or tag");

var copyOption = new Option<bool>(new[] { "-c", "--copy" }, "Copy to clipboard");

// raw
var skinTonesOption = new Option<bool>(new[] { "-st", "--skin-tones" }, "Include skin tone variants");

var rawCommand = new Command("raw", "Get raw emojis")
{
    argument,
    skinTonesOption,
    copyOption,
};

rawCommand.AddAlias("r");

rawCommand.SetHandler(
    (string[] args, bool skinTones, bool copy) =>
    {
        var value = string.Join(" ", args);
        var emojis = Emoji.Find(value);

        foreach (var e in emojis)
        {
            Console.WriteLine(e.Raw);
            if (skinTones && e.HasSkinTones)
            {
                foreach (var tone in e.RawSkinToneVariants())
                {
                    Console.WriteLine(tone);
                }
            }
        }

        if (copy)
            ClipboardService.SetText(string.Join(string.Empty, emojis.Select(e => skinTones && e.HasSkinTones ? e.Raw + string.Join(string.Empty, e.RawSkinToneVariants()) : e.Raw)));
    },
    argument,
    skinTonesOption,
    copyOption);

// alias
var aliasCommand = new Command("alias", "Get emoji aliases")
{
    argument,
    copyOption,
};

aliasCommand.AddAlias("a");

aliasCommand.SetHandler(
    (string[] args, bool copy) =>
    {
        var value = string.Join(" ", args);
        var emojis = Emoji.Find(value);

        foreach (var emoji in emojis)
        {
            foreach (var a in emoji.Aliases)
            {
                Console.WriteLine(a.PadAlias());
            }
        }

        if (copy)
            ClipboardService.SetText(string.Join(string.Empty, emojis.SelectMany(x => x.Aliases).Select(x => x.PadAlias())));
    },
    argument,
    copyOption);

// emojify
var emojifyArgument = new Argument<string[]>("args", "A text with emoji aliases");

var emojifyCommand = new Command("emojify", "Replace aliases in text with raw emojis")
{
    emojifyArgument,
    copyOption,
};

emojifyCommand.AddAlias("e");

emojifyCommand.SetHandler(
    (string[] args, bool copy) =>
    {
        var value = string.Join(" ", args);
        var result = Emoji.Emojify(value);

        Console.WriteLine(result);
        if (copy)
            ClipboardService.SetText(result);
    },
    emojifyArgument,
    copyOption);

// demojify
var demojifyArgument = new Argument<string[]>("args", "A text with raw emojis");

var demojifyCommand = new Command("demojify", "Replace raw emojis in text with aliases")
{
    demojifyArgument,
    copyOption,
};

demojifyCommand.AddAlias("d");

demojifyCommand.SetHandler(
    (string[] args, bool copy) =>
    {
        var value = string.Join(" ", args);
        var result = Emoji.Demojify(value);

        Console.WriteLine(result);
        if (copy)
            ClipboardService.SetText(result);
    },
    demojifyArgument,
    copyOption);

// export
var formatOption = new Option<string>(new[] { "-f", "--format" }, "Format the data as <json|toml|xml|yaml>");

var exportCommand = new Command("export", "Export emoji data to <json|toml|xml|yaml>")
{
    argument,
    formatOption,
    copyOption,
};

exportCommand.SetHandler(
    (string[] args, string format, bool copy) =>
    {
        var value = string.Join(" ", args);
        var emojis = Emoji.Find(value);
        string result;

        if (!string.IsNullOrEmpty(format) && string.Equals(format, "YAML", StringComparison.OrdinalIgnoreCase))
        {
            var serializer = new YamlDotNet.Serialization.SerializerBuilder().Build();
            result = serializer.Serialize(emojis);
        }
        else if (!string.IsNullOrEmpty(format) && string.Equals(format, "XML", StringComparison.OrdinalIgnoreCase))
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(emojis.GetType());
            var writer = new StringWriter();
            serializer.Serialize(System.Xml.XmlWriter.Create(writer, new System.Xml.XmlWriterSettings { Indent = true }), emojis);
            result = writer.ToString();
        }
        else if (!string.IsNullOrEmpty(format) && string.Equals(format, "TOML", StringComparison.OrdinalIgnoreCase))
        {
            result = Tomlyn.Toml.FromModel(emojis.ToDictionary(x => x.Alias()));
        }
        else
        {
            result = Newtonsoft.Json.JsonConvert.SerializeObject(emojis, Newtonsoft.Json.Formatting.Indented);
        }

        Console.WriteLine(result);
        if (copy)
            ClipboardService.SetText(result);
    },
    argument,
    formatOption,
    copyOption);

// root
var rootCommand = new RootCommand()
{
    rawCommand,
    aliasCommand,
    emojifyCommand,
    demojifyCommand,
    exportCommand,
};

rootCommand.Name = "emoji";

rootCommand.Description = "GitHub Emoji dotnet tool";

return rootCommand.Invoke(args);

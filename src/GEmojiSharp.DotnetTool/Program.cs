using System.CommandLine;
using System.Text;
using GEmojiSharp;
using TextCopy;

Console.OutputEncoding = Encoding.UTF8;

Argument<string[]> argument = new("args") { Description = "Find emojis via description, category, alias or tag" };

Option<bool> copyOption = new("--copy", "-c") { Description = "Copy to clipboard" };

// raw
Option<bool> skinTonesOption = new("--skin-tones", "-st") { Description = "Include skin tone variants" };

Command rawCommand = new("raw", "Get raw emojis")
{
    argument,
    skinTonesOption,
    copyOption,
};

rawCommand.Aliases.Add("r");

rawCommand.SetAction(parseResult =>
{
    var args = parseResult.GetValue(argument);
    var skinTones = parseResult.GetValue(skinTonesOption);
    var copy = parseResult.GetValue(copyOption);

    var value = string.Join(" ", args!);
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
        ClipboardService.SetText(string.Concat(emojis.Select(e => skinTones && e.HasSkinTones ? e.Raw + string.Concat(e.RawSkinToneVariants()) : e.Raw)));
});

// alias
Command aliasCommand = new("alias", "Get emoji aliases")
{
    argument,
    copyOption,
};

aliasCommand.Aliases.Add("a");

aliasCommand.SetAction(parseResult =>
{
    var args = parseResult.GetValue(argument);
    var copy = parseResult.GetValue(copyOption);

    var value = string.Join(" ", args!);
    var emojis = Emoji.Find(value);

    foreach (var emoji in emojis)
    {
        foreach (var a in emoji.Aliases)
        {
            Console.WriteLine(a.PadAlias());
        }
    }

    if (copy)
        ClipboardService.SetText(string.Concat(emojis.SelectMany(x => x.Aliases).Select(x => x.PadAlias())));
});

// emojify
Argument<string[]> emojifyArgument = new("args") { Description = "A text with emoji aliases" };

Command emojifyCommand = new("emojify", "Replace aliases in text with raw emojis")
{
    emojifyArgument,
    copyOption,
};

emojifyCommand.Aliases.Add("e");

emojifyCommand.SetAction(parseResult =>
{
    var args = parseResult.GetValue(emojifyArgument);
    var copy = parseResult.GetValue(copyOption);

    var value = string.Join(" ", args!);
    var result = Emoji.Emojify(value);

    Console.WriteLine(result);
    if (copy)
        ClipboardService.SetText(result);
});

// demojify
Argument<string[]> demojifyArgument = new("args") { Description = "A text with raw emojis" };

Command demojifyCommand = new("demojify", "Replace raw emojis in text with aliases")
{
    demojifyArgument,
    copyOption,
};

demojifyCommand.Aliases.Add("d");

demojifyCommand.SetAction(parseResult =>
{
    var args = parseResult.GetValue(demojifyArgument);
    var copy = parseResult.GetValue(copyOption);

    var value = string.Join(" ", args!);
    var result = Emoji.Demojify(value);

    Console.WriteLine(result);
    if (copy)
        ClipboardService.SetText(result);
});

// export
Option<string> formatOption = new("--format", "-f") { Description = "Format the data as <json|toml|xml|yaml>" };

Command exportCommand = new("export", "Export emoji data to <json|toml|xml|yaml>")
{
    argument,
    formatOption,
    copyOption,
};

exportCommand.SetAction(parseResult =>
{
    var args = parseResult.GetValue(argument);
    var format = parseResult.GetValue(formatOption);
    var copy = parseResult.GetValue(copyOption);

    var value = string.Join(" ", args!);
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
});

// root
RootCommand rootCommand = new("GitHub Emoji dotnet tool")
{
    rawCommand,
    aliasCommand,
    emojifyCommand,
    demojifyCommand,
    exportCommand,
};

var parseResult = rootCommand.Parse(args);
return parseResult.Invoke();

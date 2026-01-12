using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;

namespace GEmojiSharpExtension;

[Guid("0caf71f0-3ded-4475-b598-ad9304f89889")]
public sealed partial class GEmojiSharpExtension(ManualResetEvent ExtensionDisposedEvent) : IExtension, IDisposable
{
    private readonly GEmojiSharpExtensionCommandsProvider _provider = new();

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => _provider,
            _ => null,
        };
    }

    public void Dispose() => ExtensionDisposedEvent.Set();
}

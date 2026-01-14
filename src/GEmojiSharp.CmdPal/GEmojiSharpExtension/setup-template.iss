#define AppVersion "0.1.0.0"

[Setup]
AppId={{0caf71f0-3ded-4475-b598-ad9304f89889}}
AppName=GitHub Emoji
AppVersion={#AppVersion}
AppPublisher=Henrik Lau Eriksson
DefaultDirName={autopf}\GEmojiSharpExtension
OutputDir=bin\Release\installer
OutputBaseFilename=GEmojiSharpExtension-Setup-{#AppVersion}
Compression=lzma
SolidCompression=yes
MinVersion=10.0.19041

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "bin\Release\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\GitHub Emoji"; Filename: "{app}\GEmojiSharpExtension.exe"

[Registry]
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{0caf71f0-3ded-4475-b598-ad9304f89889}}"; ValueData: "GEmojiSharpExtension"
Root: HKCU; Subkey: "SOFTWARE\Classes\CLSID\{{0caf71f0-3ded-4475-b598-ad9304f89889}}\LocalServer32"; ValueData: "{app}\GEmojiSharpExtension.exe -RegisterProcessAsComServer"
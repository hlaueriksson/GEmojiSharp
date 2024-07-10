<#PSScriptInfo
.VERSION 0.0.0
.GUID 01b1a89c-3a39-40e5-ae5b-d85bce48815f
.AUTHOR Henrik Lau Eriksson
.COMPANYNAME
.COPYRIGHT
.TAGS PowerToys Run Plugins Release
.LICENSEURI
.PROJECTURI https://github.com/hlaueriksson/GEmojiSharp
.ICONURI
.EXTERNALMODULEDEPENDENCIES
.REQUIREDSCRIPTS
.EXTERNALSCRIPTDEPENDENCIES
.RELEASENOTES
#>

<#
    .Synopsis
    Generate release notes snippets for the plugin.

    .Description
    Gathers information about the plugin and generates a markdown file with release notes snippets.

    .Example
    .\release.ps1

    .Link
    https://github.com/hlaueriksson/GEmojiSharp
#>

# Version
[xml]$props = Get-Content -Path "*.csproj"
$version = "$($props.Project.PropertyGroup.Version)".Trim()

# Platforms
$platforms = "$($props.Project.PropertyGroup.Platforms)".Trim() -split ";"

# Output
$result = "release-$version.md"

$projectUri = "https://github.com/hlaueriksson/GEmojiSharp"
$name = Split-Path -Path $PWD -Leaf
$files = Get-ChildItem -Path . -File -Include "$name-$version*.zip" -Recurse

function Write-Line {
    param (
        [string]$line
    )

    $line | Add-Content -Path $result
}

function Get-Platform {
    param (
        [string]$filename
    )

    if ($filename -Match $platforms[0]) {
        $platforms[0]
    }
    else {
        $platforms[1]
    }
}

Set-Content -Path $result -Value ""

Write-Line "## $name"
Write-Line ""
Write-Line "| Platform | Filename | Downloads"
Write-Line "| --- | --- | ---"
foreach ($file in $files) {
    $zip = $file.Name
    $platform = Get-Platform $zip
    $url = "$projectUri/releases/download/v$version/$zip"
    $badge = "https://img.shields.io/github/downloads/$($projectUri.Replace('https://github.com/', ''))/v$version/$zip"

    Write-Line "| ``$platform`` | [$zip]($url) | [![$zip]($badge)]($url)"
}
Write-Line ""

Write-Line "### Installer Hashes"
Write-Line ""
Write-Line "| Filename | SHA256 Hash"
Write-Line "| --- | ---"
foreach ($file in $files) {
    $zip = $file.Name
    $hash = Get-FileHash $file -Algorithm SHA256 | Select-Object -ExpandProperty Hash

    Write-Line "| ``$zip`` | ``$hash``"
}

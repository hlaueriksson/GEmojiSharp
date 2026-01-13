<#PSScriptInfo
.VERSION 0.0.0
.GUID a5a44179-e1e5-4ddd-9e0a-d6653eb69d9f
.AUTHOR Henrik Lau Eriksson
.COMPANYNAME
.COPYRIGHT
.TAGS PowerToys Run Plugins Pack
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
    Packs the plugin into release archive.

    .Description
    Builds the project in Release configuration,
    copies the output files into plugin folder,
    packs the plugin folder into release archive.

    .Example
    .\pack.ps1

    .Link
    https://github.com/hlaueriksson/GEmojiSharp
#>

# Clean
Get-ChildItem -Path "." -Directory -Include "bin", "obj" -Recurse | Remove-Item -Recurse -Force

$dependencies = @("PowerToys.Common.UI.*", "PowerToys.ManagedCommon.*", "PowerToys.Settings.UI.Lib.*", "Wox.Infrastructure.*", "Wox.Plugin.*")

# Version
[xml]$props = Get-Content -Path "*.csproj"
$version = "$($props.Project.PropertyGroup.Version)".Trim()
Write-Output "Version: $version"

# Platforms
$platforms = "$($props.Project.PropertyGroup.Platforms)".Trim() -split ";"

# TargetFramework
$targetFramework = $props.Project.PropertyGroup.TargetFramework

foreach ($platform in $platforms)
{
    Write-Output "Platform: $platform"

    # Build
    dotnet build -c Release /p:TF_BUILD=true /p:Platform=$platform

    if (!$?) {
        # Build FAILED.
        Exit $LastExitCode
    }

    $name = Split-Path -Path $PWD -Leaf
    $folder = $name.Split(".")[0] # -1 last

    Write-Output "Pack: $name"

    $output = ".\bin\$platform\Release\$targetFramework\"
    $destination = ".\bin\$platform\$folder"
    $zip = ".\bin\$platform\$name-$version-$($platform.ToLower()).zip"

    Copy-Item -Path $output -Destination $destination -Recurse -Exclude $dependencies
    Compress-Archive -Path $destination -DestinationPath $zip
}

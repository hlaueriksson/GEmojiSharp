<#PSScriptInfo
.VERSION 0.0.0
.GUID 04ee4032-b6da-48fa-a89f-553b6dd0b611
.AUTHOR Henrik Lau Eriksson
.COMPANYNAME
.COPYRIGHT
.TAGS PowerToys Run Plugins Deploy
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
    Deploys the plugin to PowerToys Run.

    .Description
    Copies the plugin to the PowerToys Run Plugins folder:
    - %LocalAppData%\Microsoft\PowerToys\PowerToys Run\Plugins

    .Parameter Platform
    Platform: ARM64 | x64

    .Example
    .\deploy.ps1

    .Link
    https://github.com/hlaueriksson/GEmojiSharp
#>
param (
    [ValidateSet("ARM64", "x64")]
    [string]$platform = "x64"
)

#Requires -RunAsAdministrator

Stop-Process -Name "PowerToys" -Force -ErrorAction SilentlyContinue

dotnet build -c Release /p:TF_BUILD=true /p:Platform=$platform

Write-Output "Platform: $platform"

$libs = Get-ChildItem -Path .\libs -File -Recurse

$name = ((Split-Path -Path $PWD -Leaf).Split(".")[0]) # -1 last

Write-Output "Deploy: $name"

Remove-Item -LiteralPath "$env:LOCALAPPDATA\Microsoft\PowerToys\PowerToys Run\Plugins\$name" -Recurse -Force -ErrorAction SilentlyContinue
Copy-Item -Path ".\bin\$platform\Release\net8.0-windows" -Destination "$env:LOCALAPPDATA\Microsoft\PowerToys\PowerToys Run\Plugins\$name" -Recurse -Force -Exclude $libs

$machinePath = "C:\Program Files\PowerToys\PowerToys.exe"
$userPath = "$env:LOCALAPPDATA\PowerToys\PowerToys.exe"

if (Test-Path $machinePath) {
    Start-Process -FilePath $machinePath
}
elseif (Test-Path $userPath) {
    Start-Process -FilePath $userPath
}
else {
    Write-Error "Unable to start PowerToys"
}

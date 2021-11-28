# Script: build.ps1
# Builds selected EFrt projects and creates local nuget packages from all packable projects.
# License: MIT
# Authors: Premysl Fara 
# (C) 2020 - 2021
# Requires: "PowerShell", "mono" (on Linux) and "nuget.exe".
# Note: Make sure, that the $toolsPath, the $nugetPath and the $nugetCommand variables are correctly set.
# Run as "powershell.exe -ExecutionPolicy Unrestricted ./build.ps1 -Configuration Release" in the folder with build.ps1.

param(
	[Parameter(Mandatory=$True)]$Configuration
)

$ErrorActionPreference = 'Stop'

$baseDir = Get-Location

# Temporary place for storing generated nugets.
$outDir = "$baseDir/out"

# The local nugets repository path.
$nugetDir = "$baseDir/nuget"

# Choose or set a path, with build tools:
#$toolsPath = "~/Devel/bin"   # Linux
$toolsPath = "C:\Devel\bin"   # Windows

# Choose or set a path to the nuget.exe:
$nugetPath = "$toolsPath/nuget/nuget.exe"

# Choose, how is the nuget.exe called:
#$nugetCommand = "mono $nugetPath"   # On Linux you need mono to run it.
$nugetCommand = "$nugetPath"         # On Windows it runs as it is.

. "$baseDir/include.ps1"

Clean
#Build

BuildProject "EFrt.Core"
PackProject "EFrt.Core"

BuildProject "EFrt.Libs.Core"
PackProject "EFrt.Libs.Core"

BuildProject "EFrt.Libs.CoreExt"
PackProject "EFrt.Libs.CoreExt"

BuildProject "EFrt.Libs.Double"
PackProject "EFrt.Libs.Double"

BuildProject "EFrt.Libs.DoubleExt"
PackProject "EFrt.Libs.DoubleExt"

BuildProject "EFrt.Libs.Exception"
PackProject "EFrt.Libs.Exception"

BuildProject "EFrt.Libs.Floating"
PackProject "EFrt.Libs.Floating"

BuildProject "EFrt.Libs.FloatingExt"
PackProject "EFrt.Libs.FloatingExt"

BuildProject "EFrt.Libs.Object"
PackProject "EFrt.Libs.Object"

BuildProject "EFrt.Libs.String"
PackProject "EFrt.Libs.String"

BuildProject "EFrt.Libs.Tools"
PackProject "EFrt.Libs.Tools"

BuildProject "EFrt.Libs.ToolsExt"
PackProject "EFrt.Libs.ToolsExt"

BuildProject "EFrt"
PackProject "EFrt"

# https://4sysops.com/archives/use-powershell-to-execute-an-exe/
# https://powershellexplained.com/2017-01-13-powershell-variable-substitution-in-strings/

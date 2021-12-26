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

# --- SETTINGS START HERE ---

$baseDir = Get-Location

# Temporary place for storing generated nugets.
$outDir = "$baseDir/out"

# The local nugets repository path.
$nugetDir = "$baseDir/nuget"

# If true, it tryes to remove an existing nuget version from the target nuget repository.
$cleanUp = $True

# Choose or set a path, with build tools:
$toolsPath = "~/Devel/bin"    # Linux
#$toolsPath = "C:\Devel\bin"   # Windows

# Choose or set a path to the nuget.exe:
$nugetPath = "$toolsPath/nuget/nuget.exe"

# Choose, how is the nuget.exe called:
#   On Linux you need mono to run it.
#   On Windows it runs as it is.
$useMonoToRunNuget = $True                   

# --- SETTINGS END HERE ---

. "$baseDir/include.ps1"

Clean
#Build

BuildProject "EFrt.Core"
PackProject "EFrt.Core" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.Core"
PackProject "EFrt.Libs.Core" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.CoreExt"
PackProject "EFrt.Libs.CoreExt" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.Double"
PackProject "EFrt.Libs.Double" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.DoubleExt"
PackProject "EFrt.Libs.DoubleExt" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.Exception"
PackProject "EFrt.Libs.Exception" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.Floating"
PackProject "EFrt.Libs.Floating" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.FloatingExt"
PackProject "EFrt.Libs.FloatingExt" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.Object"
PackProject "EFrt.Libs.Object" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.String"
PackProject "EFrt.Libs.String" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.Tools"
PackProject "EFrt.Libs.Tools" $cleanUp $useMonoToRunNuget

BuildProject "EFrt.Libs.ToolsExt"
PackProject "EFrt.Libs.ToolsExt" $cleanUp $useMonoToRunNuget

BuildProject "EFrt"
PackProject "EFrt" $cleanUp $useMonoToRunNuget

# https://4sysops.com/archives/use-powershell-to-execute-an-exe/
# https://powershellexplained.com/2017-01-13-powershell-variable-substitution-in-strings/

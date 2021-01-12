# Script: build.ps1
# Builds the EFrt solution and creates local nuget packages from all packable EFrt projects.
# License: MIT
# Authors: Premysl Fara 
# (C) 2020 - 2021
# Run as "powershell.exe -ExecutionPolicy Unrestricted .\build.ps1 -Configuration Release" in the solution folder.

param(
	[Parameter(Mandatory=$True)]$Configuration
)

$ErrorActionPreference = 'Stop'

$baseDir = Get-Location
$outDir = "$baseDir\out"
$nugetDir = "$baseDir\nuget"
$toolsPath = "D:\Devel\bin"

. "$baseDir\include.ps1"

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
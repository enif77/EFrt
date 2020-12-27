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
Build

PackProject "EFrt.Core"

PackProject "EFrt.Libs.Core"
PackProject "EFrt.Libs.CoreExt"
PackProject "EFrt.Libs.DoubleCellInteger"
PackProject "EFrt.Libs.Float"
PackProject "EFrt.Libs.IO"
PackProject "EFrt.Libs.Object"
PackProject "EFrt.Libs.SingleCellInteger"
PackProject "EFrt.Libs.String"

PackProject "EFrt"

# https://4sysops.com/archives/use-powershell-to-execute-an-exe/
# https://powershellexplained.com/2017-01-13-powershell-variable-substitution-in-strings/
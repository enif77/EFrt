# Script: build-package.ps1
# Builds the EFrt solution and creates a local nuget package from a specified EFrt project.
# License: MIT
# Authors: Premysl Fara 
# (C) 2020 - 2021
# Run as "powershell.exe -ExecutionPolicy Unrestricted .\build.ps1 -Configuration Release -ProjectName <project-name>" in the solution folder.

param(
	[Parameter(Mandatory=$True)]$Configuration,
	[Parameter(Mandatory=$True)]$ProjectName
)

$ErrorActionPreference = 'Stop'

$baseDir = Get-Location
$outDir = "$baseDir\out"
$nugetDir = "$baseDir\nuget"
$toolsPath = "D:\Devel\bin"

. "$baseDir\include.ps1"

Clean
Build
PackProject $ProjectName

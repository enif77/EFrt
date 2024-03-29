function Check-ExitCode() {
	$exitCode = $LASTEXITCODE
	if ($exitCode -ne 0) {
		Write-Output "Non-zero ($exitCode) exit code. Exiting..."
		exit $exitCode
	}
}

function GetStringBetweenTwoStrings($firstString, $secondString, $importPath) {

    $file = Get-Content $importPath
    $pattern = "$firstString(.*?)$secondString"
    $result = [regex]::Match($file, $pattern).Groups[1].Value

    return $result
}

function Clean() {
	if (Test-Path $outDir) {
		Remove-Item -Force -Recurse $outDir
	}

	mkdir $outDir | Out-Null
}


function Build() {
	dotnet clean "$baseDir/EFrt.sln" --configuration $Configuration
	Check-ExitCode

	dotnet build "$baseDir/EFrt.sln" --configuration $Configuration --configfile "$baseDir/NuGet.Config" --force --no-cache --verbosity minimal
	Check-ExitCode
}

function BuildProject($projectName, $cleanUp=$False) {
    Set-Location $projectName

	if ($cleanUp) {
            Write-Host "Cleaning..."

	    dotnet clean --configuration $Configuration --verbosity normal
	    Check-ExitCode
	}

	dotnet build --configuration $Configuration --configfile "$baseDir/NuGet.Config" --force --no-cache --verbosity normal
	Check-ExitCode

	Set-Location $baseDir
}

# Note: Call the Build function before this!
function PackProject($projectName, $cleanUp=$True, $useMonoToRunNuget=$False) {
    Write-Host "Packing project:" $projectName

    Set-Location $projectName

    $packageVersion = GetStringBetweenTwoStrings -firstString "<Version>" -secondString "</Version>" -importPath "$baseDir/$projectName/$projectName.csproj"
	Check-ExitCode
	
	Write-Host "Package version: " + $packageVersion

	dotnet pack --configuration $Configuration --no-restore --force -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg --output $outDir --verbosity minimal
	Check-ExitCode

	if ($cleanUp) {
	    Write-Host "Removing the previous build of this package version..."

	    # This can fail.
		if ($useMonoToRunNuget)
		{
			& mono $nugetPath delete $projectName $packageVersion -noninteractive -source $nugetDir	
		}
		else
		{
			& $nugetPath delete $projectName $packageVersion -noninteractive -source $nugetDir
		}
	    		
		Write-Host "Previous package build removed."
	}

	# If the cleanup fails, it sets the exit code to 1.
	# This will reset the exit code to 0, so the next Check-ExitCode
	# command won't detect this fail, that is not related to
	# the nuget add command below, as its failure.
	$LASTEXITCODE = 0
	
	Write-Host "Adding the package to the local NuGet repository..."

	if ($useMonoToRunNuget)
	{
		& mono $nugetPath add "$outDir/$projectName.$packageVersion.nupkg" -source $nugetDir
	}
	else
	{
		& $nugetPath add "$outDir/$projectName.$packageVersion.nupkg" -source $nugetDir
	}
	Check-ExitCode

	Write-Host "Package added."

	Set-Location $baseDir
}

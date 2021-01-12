function Check-ExitCode() {
	$exitCode = $LASTEXITCODE
	if ($exitCode -ne 0) {
		echo "Non-zero ($exitCode) exit code. Exiting..."
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
		rm -Force -Recurse $outDir
	}

	mkdir $outDir | Out-Null
}


function Build() {
	dotnet clean "$baseDir\EFrt.sln" --configuration $Configuration
	Check-ExitCode

	dotnet build "$baseDir\EFrt.sln" --configuration $Configuration --configfile "$baseDir\NuGet.Config" --force --no-cache --verbosity minimal
	Check-ExitCode
}


function BuildProject($projectName, $cleanUp=$False) {
    cd $projectName

	if ($cleanUp) {
		Write-Host "Cleaning..."

	    dotnet clean --configuration $Configuration --verbosity normal
	    Check-ExitCode
	}

	dotnet build --configuration $Configuration --configfile "$baseDir\NuGet.Config" --force --no-cache --verbosity normal
	Check-ExitCode

	cd $baseDir
}


# Note: Call the Build function before this!
function PackProject($projectName, $cleanUp=$True) {
    Write-Host "Packing project:" $projectName

    cd $projectName

    $packageVersion = GetStringBetweenTwoStrings -firstString "<Version>" -secondString "</Version>" -importPath "$baseDir\$projectName\$projectName.csproj"
	Check-ExitCode
	
	Write-Host "Package version: " + $packageVersion

	dotnet pack --configuration $Configuration --no-restore --force --include-source --output $outDir --verbosity minimal
	Check-ExitCode

	if ($cleanUp) {
	    Write-Host "Removing the previous build of this package version..."

	    # This can fail.
	    & $toolsPath\nuget\nuget.exe delete $projectName $packageVersion -noninteractive -source $nugetDir

		Write-Host "Previous package build removed."
	}

	Write-Host "Adding the package to the local NuGet repository..."

	& $toolsPath\nuget\nuget.exe add "$outDir\$projectName.$packageVersion.nupkg" -source $nugetDir
	Check-ExitCode

	Write-Host "Package added."

	cd $baseDir
}

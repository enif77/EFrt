@REM Script: release.bat
@REM Creates nuget package from an EFrt project.
@REM Authors: Premysl Fara 
@REM (C) 2020 - 2021

@REM Parameters: release.bat project-name

@REM Package name. (EFrt)
@SET PACKAGE_NAME=%1

@REM If this is 0, it is a normal release.
@REM If this is not 0, it is a rerelease. Existing packages with the same versions are deleted from the source first, then readded.
@SET RERELEASE=1

@REM ---

 @REM Build configuration selection.

@SET BUILD_CONFIGURATION=Release

@REM --- DIRECTORIES ---

@SET TOOLS_PATH=D:\Devel\bin
@SET BUILD_START_DIR=%CD%
@SET NUGET_DIR=%BUILD_START_DIR%\nuget

@REM --- PACKAGE ---

@CD %PACKAGE_NAME%

@REM A version of each package. (1.0.0)
%TOOLS_PATH%\GetVersion\GetVersion.exe %PACKAGE_NAME%.csproj > version.txt
@SET /p PACKAGE_VERSION=<version.txt

@REM https://docs.microsoft.com/cs-cz/dotnet/core/tools/dotnet-pack
@REM https://docs.microsoft.com/cs-cz/nuget/reference/msbuild-targets#pack-target
@REM https://docs.microsoft.com/cs-cz/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli
@REM https://stackoverflow.com/questions/45897271/dotnet-restore-vs-nuget-restore-with-teamcity

dotnet restore --configfile %BUILD_START_DIR%\NuGet.Config --force --no-cache --verbosity normal
dotnet build --configuration %BUILD_CONFIGURATION% --no-restore --verbosity minimal
dotnet pack --configuration %BUILD_CONFIGURATION% --no-restore --force --include-source --output nupkgs --verbosity minimal

@REM --- CLEANUP ---

@IF %RERELEASE%==0 GOTO skip_cleanup

%TOOLS_PATH%\nuget\nuget.exe delete %PACKAGE_NAME% %PACKAGE_VERSION% -noninteractive -source %NUGET_DIR%

:skip_cleanup

@REM --- PUBLISH ---

%TOOLS_PATH%\nuget\nuget.exe add nupkgs\%PACKAGE_NAME%.%PACKAGE_VERSION%.nupkg -source %NUGET_DIR%


@CD %BUILD_START_DIR%
@REM Script: release-local.bat
@REM Creates local nuget packages from the EFrt project.
@REM Authors: Premysl Fara 
@REM (C) 2020 - 2021

@SET BUILD_CONFIGURATION=Release

dotnet build EFrt.sln --configuration %BUILD_CONFIGURATION% --configfile %CD%\NuGet.Config --force --no-cache --verbosity normal
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Core
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Libs.Core
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Libs.CoreExt
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Libs.DoubleCellInteger
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Libs.Float
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Libs.IO
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Libs.Object
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Libs.SingleCellInteger
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt.Libs.String
@if %ERRORLEVEL% GEQ 1 GOTO error

@CALL release.bat %BUILD_CONFIGURATION% EFrt
@if %ERRORLEVEL% GEQ 1 GOTO error


@ECHO OK
@GOTO exitus

:error

@ECHO ERROR %ERRORLEVEL%
EXIT /B 1

:exitus

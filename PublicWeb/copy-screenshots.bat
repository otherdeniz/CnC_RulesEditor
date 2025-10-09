@echo off
REM Script to copy screenshots to PublicWeb/images folder

echo Copying screenshots to PublicWeb/images...

REM Check if Screenshots folder exists
if not exist "Screenshots" (
    echo ERROR: Screenshots folder not found!
    echo Please run this script from the CnC_RulesEditor root directory.
    pause
    exit /b 1
)

REM Check if PublicWeb/images folder exists
if not exist "PublicWeb\images" (
    echo Creating PublicWeb\images folder...
    mkdir "PublicWeb\images"
)

REM Copy all PNG files from Screenshots to PublicWeb/images
echo Copying PNG files...
copy "Screenshots\*.png" "PublicWeb\images\" /Y

if %errorlevel% equ 0 (
    echo.
    echo SUCCESS: Screenshots copied successfully!
    echo.
    dir "PublicWeb\images\*.png"
) else (
    echo.
    echo ERROR: Failed to copy screenshots.
)

echo.
pause

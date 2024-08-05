@echo off
REM Parameter 1 is the source folder
REM Parameter 2 is the target folder
REM Parameter 3 is the build configuration
xcopy "%1Bitmaps" "%2Bitmaps" /E /Y /I
xcopy "%1Snippets" "%2Snippets" /E /Y /I
xcopy "%1..\Deniz.Updater\bin\%3\net6.0-windows" "%2Updater" /E /Y /I

rd "%2runtimes\linux-arm64" /S /Q
rd "%2runtimes\linux-musl-x64" /S /Q
rd "%2runtimes\linux-x64" /S /Q
rd "%2runtimes\osx-arm64" /S /Q
rd "%2runtimes\osx-x64" /S /Q

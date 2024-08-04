@echo off
REM Parameter 1 is the source folder
REM Parameter 2 is the target folder
xcopy "%1Bitmaps" "%2Bitmaps" /E /Y /I
xcopy "%1Snippets" "%2Snippets" /E /Y /I

rd "%2runtimes\linux-arm64" /S /Q
rd "%2runtimes\linux-musl-x64" /S /Q
rd "%2runtimes\linux-x64" /S /Q
rd "%2runtimes\osx-arm64" /S /Q
rd "%2runtimes\osx-x64" /S /Q

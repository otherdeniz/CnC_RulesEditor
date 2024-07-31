@echo off
REM Parameter 1 is the source folder
REM Parameter 2 is the target folder
xcopy "%1Bitmaps" "%2Bitmaps" /E /Y /I
xcopy "%1Snippets" "%2Snippets" /E /Y /I

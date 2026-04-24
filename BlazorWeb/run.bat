@echo off
REM Build and run script for C&C Rules Editor Blazor Web App

echo ========================================
echo C&C Rules Editor - Blazor Web App
echo ========================================
echo.

:menu
echo Choose an option:
echo 1. Run locally (dotnet run)
echo 2. Build Docker image
echo 3. Run Docker container
echo 4. Run with Docker Compose
echo 5. Stop Docker Compose
echo 6. View Docker logs
echo 7. Clean and rebuild
echo 8. Exit
echo.

set /p choice="Enter your choice (1-8): "

if "%choice%"=="1" goto run_local
if "%choice%"=="2" goto build_docker
if "%choice%"=="3" goto run_docker
if "%choice%"=="4" goto docker_compose_up
if "%choice%"=="5" goto docker_compose_down
if "%choice%"=="6" goto docker_logs
if "%choice%"=="7" goto clean_rebuild
if "%choice%"=="8" goto end

echo Invalid choice. Please try again.
echo.
goto menu

:run_local
echo.
echo Running locally with dotnet...
cd BlazorWeb
dotnet run
goto end

:build_docker
echo.
echo Building Docker image...
docker build -t cnc-rules-editor-web -f BlazorWeb/Dockerfile .
echo.
echo Docker image built successfully!
echo.
pause
goto menu

:run_docker
echo.
echo Running Docker container...
docker run -d -p 8080:8080 --name cnc-web cnc-rules-editor-web
echo.
echo Container started! Access at http://localhost:8080
echo.
pause
goto menu

:docker_compose_up
echo.
echo Starting with Docker Compose...
docker-compose up -d
echo.
echo Application started! Access at http://localhost:8080
echo.
pause
goto menu

:docker_compose_down
echo.
echo Stopping Docker Compose...
docker-compose down
echo.
echo Application stopped!
echo.
pause
goto menu

:docker_logs
echo.
echo Viewing Docker logs (Ctrl+C to exit)...
docker-compose logs -f
goto menu

:clean_rebuild
echo.
echo Cleaning and rebuilding...
cd BlazorWeb
dotnet clean
dotnet build -c Release
echo.
echo Build complete!
echo.
pause
goto menu

:end
echo.
echo Goodbye!

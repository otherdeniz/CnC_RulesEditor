#!/bin/bash

# Build and run script for C&C Rules Editor Blazor Web App

echo "========================================"
echo "C&C Rules Editor - Blazor Web App"
echo "========================================"
echo ""

show_menu() {
    echo "Choose an option:"
    echo "1. Run locally (dotnet run)"
    echo "2. Build Docker image"
    echo "3. Run Docker container"
    echo "4. Run with Docker Compose"
    echo "5. Stop Docker Compose"
    echo "6. View Docker logs"
    echo "7. Clean and rebuild"
    echo "8. Exit"
    echo ""
}

run_local() {
    echo ""
    echo "Running locally with dotnet..."
    cd BlazorWeb
    dotnet run
}

build_docker() {
    echo ""
    echo "Building Docker image..."
    docker build -t cnc-rules-editor-web -f BlazorWeb/Dockerfile .
    echo ""
    echo "Docker image built successfully!"
    echo ""
    read -p "Press Enter to continue..."
}

run_docker() {
    echo ""
    echo "Running Docker container..."
    docker run -d -p 8080:8080 --name cnc-web cnc-rules-editor-web
    echo ""
    echo "Container started! Access at http://localhost:8080"
    echo ""
    read -p "Press Enter to continue..."
}

docker_compose_up() {
    echo ""
    echo "Starting with Docker Compose..."
    docker-compose up -d
    echo ""
    echo "Application started! Access at http://localhost:8080"
    echo ""
    read -p "Press Enter to continue..."
}

docker_compose_down() {
    echo ""
    echo "Stopping Docker Compose..."
    docker-compose down
    echo ""
    echo "Application stopped!"
    echo ""
    read -p "Press Enter to continue..."
}

docker_logs() {
    echo ""
    echo "Viewing Docker logs (Ctrl+C to exit)..."
    docker-compose logs -f
}

clean_rebuild() {
    echo ""
    echo "Cleaning and rebuilding..."
    cd BlazorWeb
    dotnet clean
    dotnet build -c Release
    echo ""
    echo "Build complete!"
    echo ""
    read -p "Press Enter to continue..."
}

while true; do
    show_menu
    read -p "Enter your choice (1-8): " choice
    
    case $choice in
        1) run_local ;;
        2) build_docker ;;
        3) run_docker ;;
        4) docker_compose_up ;;
        5) docker_compose_down ;;
        6) docker_logs ;;
        7) clean_rebuild ;;
        8) echo ""; echo "Goodbye!"; exit 0 ;;
        *) echo "Invalid choice. Please try again."; echo "" ;;
    esac
done

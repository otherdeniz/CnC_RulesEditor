# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY BlazorWeb/BlazorWeb.csproj BlazorWeb/
RUN dotnet restore "BlazorWeb/BlazorWeb.csproj"

# Copy everything else and build
COPY BlazorWeb/ BlazorWeb/
WORKDIR "/src/BlazorWeb"
RUN dotnet build "BlazorWeb.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "BlazorWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Run the app
ENTRYPOINT ["dotnet", "BlazorWeb.dll"]

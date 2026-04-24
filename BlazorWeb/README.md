# C&C Rules Editor - Blazor Web Application

A modern Blazor Server web application for the C&C Rules Editor project, featuring dynamic GitHub release fetching and Docker containerization.

## Features

- **Blazor Server**: Fast, interactive web application using .NET 8
- **Dynamic GitHub Integration**: Automatically fetches latest release information from GitHub API
- **Responsive Design**: Mobile-friendly interface with modern CSS
- **Docker Support**: Fully containerized for easy deployment
- **Caching**: Smart caching of GitHub API responses to reduce API calls
- **SEO Optimized**: Proper meta tags and semantic HTML

## Project Structure

```
BlazorWeb/
├── Components/
│   ├── Layout/
│   │   └── MainLayout.razor      # Main layout with header and footer
│   ├── Pages/
│   │   ├── Home.razor            # Homepage
│   │   ├── Features.razor        # Features page
│   │   ├── Tutorial.razor        # Tutorial page
│   │   └── Download.razor        # Download page with dynamic GitHub data
│   ├── App.razor                 # Root component
│   └── Routes.razor              # Routing configuration
├── Models/
│   └── GitHubRelease.cs          # GitHub release model
├── Services/
│   ├── IGitHubService.cs         # GitHub service interface
│   └── GitHubService.cs          # GitHub API service implementation
├── wwwroot/
│   └── css/
│       └── style.css             # Main stylesheet
├── appsettings.json              # Configuration
├── Program.cs                    # Application entry point
├── BlazorWeb.csproj              # Project file
├── Dockerfile                    # Docker configuration
└── .dockerignore                 # Docker ignore file
```

## Prerequisites

- .NET 8.0 SDK or later
- Docker (optional, for containerization)
- Docker Compose (optional, for easy deployment)

## Local Development

### Running with .NET CLI

1. Navigate to the BlazorWeb directory:
   ```bash
   cd BlazorWeb
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Open your browser and navigate to:
   - HTTP: `http://localhost:5000`
   - HTTPS: `https://localhost:5001`

### Running with Visual Studio

1. Open `BlazorWeb.csproj` in Visual Studio 2022 or later
2. Press F5 to run with debugging or Ctrl+F5 to run without debugging

## Docker Deployment

### Build and Run with Docker

1. Build the Docker image:
   ```bash
   docker build -t cnc-rules-editor-web -f BlazorWeb/Dockerfile .
   ```

2. Run the container:
   ```bash
   docker run -d -p 8080:8080 --name cnc-web cnc-rules-editor-web
   ```

3. Access the application at `http://localhost:8080`

### Using Docker Compose

1. Build and start the container:
   ```bash
   docker-compose up -d
   ```

2. View logs:
   ```bash
   docker-compose logs -f
   ```

3. Stop the container:
   ```bash
   docker-compose down
   ```

4. Rebuild after changes:
   ```bash
   docker-compose up -d --build
   ```

## Configuration

### appsettings.json

```json
{
  "GitHub": {
    "Owner": "otherdeniz",
    "Repository": "CnC_RulesEditor"
  }
}
```

### Environment Variables

You can override configuration using environment variables:

- `GitHub__Owner`: GitHub repository owner
- `GitHub__Repository`: GitHub repository name
- `ASPNETCORE_ENVIRONMENT`: Environment (Development/Production)
- `ASPNETCORE_URLS`: URLs to listen on

Example:
```bash
docker run -d -p 8080:8080 \
  -e GitHub__Owner=myusername \
  -e GitHub__Repository=myrepo \
  cnc-rules-editor-web
```

## GitHub API Integration

The application uses the GitHub REST API to fetch release information:

- **Endpoint**: `https://api.github.com/repos/{owner}/{repo}/releases/latest`
- **Caching**: Responses are cached for 10 minutes to reduce API calls
- **Rate Limiting**: GitHub API allows 60 requests/hour for unauthenticated requests
- **Fallback**: If API fails, displays static download links

### Features

- Displays latest release version and date
- Shows all available download assets
- Displays file sizes and download counts
- Automatic retry with cached data on API failure

## Deployment Options

### 1. Azure App Service

```bash
# Login to Azure
az login

# Create resource group
az group create --name cnc-web-rg --location eastus

# Create App Service plan
az appservice plan create --name cnc-web-plan --resource-group cnc-web-rg --sku B1 --is-linux

# Create web app
az webapp create --resource-group cnc-web-rg --plan cnc-web-plan --name cnc-rules-editor --runtime "DOTNETCORE:8.0"

# Deploy
dotnet publish -c Release
cd bin/Release/net8.0/publish
az webapp deployment source config-zip --resource-group cnc-web-rg --name cnc-rules-editor --src publish.zip
```

### 2. Docker Hub

```bash
# Build and tag
docker build -t yourusername/cnc-rules-editor-web:latest -f BlazorWeb/Dockerfile .

# Push to Docker Hub
docker push yourusername/cnc-rules-editor-web:latest

# Pull and run on any server
docker pull yourusername/cnc-rules-editor-web:latest
docker run -d -p 8080:8080 yourusername/cnc-rules-editor-web:latest
```

### 3. Kubernetes

Create a deployment file `k8s-deployment.yaml`:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: cnc-web
spec:
  replicas: 2
  selector:
    matchLabels:
      app: cnc-web
  template:
    metadata:
      labels:
        app: cnc-web
    spec:
      containers:
      - name: cnc-web
        image: yourusername/cnc-rules-editor-web:latest
        ports:
        - containerPort: 8080
---
apiVersion: v1
kind: Service
metadata:
  name: cnc-web-service
spec:
  selector:
    app: cnc-web
  ports:
  - protocol: TCP
    port: 80
    targetPort: 8080
  type: LoadBalancer
```

Deploy:
```bash
kubectl apply -f k8s-deployment.yaml
```

### 4. Linux Server with Nginx

1. Install .NET 8 Runtime:
   ```bash
   wget https://dot.net/v1/dotnet-install.sh
   chmod +x dotnet-install.sh
   ./dotnet-install.sh --channel 8.0 --runtime aspnetcore
   ```

2. Publish the application:
   ```bash
   dotnet publish -c Release -o /var/www/cnc-web
   ```

3. Create systemd service `/etc/systemd/system/cnc-web.service`:
   ```ini
   [Unit]
   Description=C&C Rules Editor Web

   [Service]
   WorkingDirectory=/var/www/cnc-web
   ExecStart=/usr/bin/dotnet /var/www/cnc-web/BlazorWeb.dll
   Restart=always
   RestartSec=10
   SyslogIdentifier=cnc-web
   User=www-data
   Environment=ASPNETCORE_ENVIRONMENT=Production
   Environment=ASPNETCORE_URLS=http://localhost:5000

   [Install]
   WantedBy=multi-user.target
   ```

4. Configure Nginx `/etc/nginx/sites-available/cnc-web`:
   ```nginx
   server {
       listen 80;
       server_name your-domain.com;

       location / {
           proxy_pass http://localhost:5000;
           proxy_http_version 1.1;
           proxy_set_header Upgrade $http_upgrade;
           proxy_set_header Connection keep-alive;
           proxy_set_header Host $host;
           proxy_cache_bypass $http_upgrade;
           proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
           proxy_set_header X-Forwarded-Proto $scheme;
       }
   }
   ```

5. Enable and start:
   ```bash
   sudo systemctl enable cnc-web
   sudo systemctl start cnc-web
   sudo ln -s /etc/nginx/sites-available/cnc-web /etc/nginx/sites-enabled/
   sudo nginx -t
   sudo systemctl reload nginx
   ```

## Performance Optimization

### Caching Strategy

The application implements a smart caching strategy for GitHub API calls:

- **Cache Duration**: 10 minutes
- **Cache Key**: Latest release data
- **Fallback**: Returns cached data if API fails
- **Benefits**: Reduces API calls, improves response time, handles rate limiting

### Production Optimizations

1. **Enable Response Compression**:
   Add to `Program.cs`:
   ```csharp
   builder.Services.AddResponseCompression();
   app.UseResponseCompression();
   ```

2. **Enable Response Caching**:
   ```csharp
   builder.Services.AddResponseCaching();
   app.UseResponseCaching();
   ```

3. **Use CDN for Static Assets**:
   - Upload CSS/images to CDN
   - Update references in components

## Monitoring and Logging

### Application Insights (Azure)

```csharp
builder.Services.AddApplicationInsightsTelemetry();
```

### Serilog

```csharp
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
```

### Docker Logs

```bash
# View logs
docker logs cnc-web

# Follow logs
docker logs -f cnc-web

# With docker-compose
docker-compose logs -f
```

## Troubleshooting

### Port Already in Use

```bash
# Find process using port 8080
netstat -ano | findstr :8080  # Windows
lsof -i :8080                  # Linux/Mac

# Kill the process or use a different port
docker run -d -p 8081:8080 cnc-rules-editor-web
```

### GitHub API Rate Limiting

- Unauthenticated: 60 requests/hour
- Authenticated: 5000 requests/hour

To use authentication, add to `appsettings.json`:
```json
{
  "GitHub": {
    "Token": "your-github-token"
  }
}
```

### CSS Not Loading

1. Check that `wwwroot/css/style.css` exists
2. Verify the path in `App.razor` is correct
3. Clear browser cache
4. Check browser console for 404 errors

## Development Tips

### Hot Reload

```bash
dotnet watch run
```

### Debug in VS Code

Create `.vscode/launch.json`:
```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/BlazorWeb/bin/Debug/net8.0/BlazorWeb.dll",
      "args": [],
      "cwd": "${workspaceFolder}/BlazorWeb",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  ]
}
```

## License

This project is licensed under the GNU General Public License v3.0 - see the LICENSE file for details.

## Credits

- **Original Website**: https://ruleseditor.denizesen.ch/
- **Author**: Deniz 'otherdeniz' Esen ([WH]Otherdeniz)
- **GitHub**: https://github.com/otherdeniz/CnC_RulesEditor

## Support

- **GitHub Issues**: https://github.com/otherdeniz/CnC_RulesEditor/issues
- **Discord**: https://discord.gg/cncmodding
- **Forum**: https://forums.cncnet.org/topic/12869-tiberian-sun-rules-editor-version-2024/

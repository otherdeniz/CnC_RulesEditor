# Quick Start Guide

## 🚀 Get Started in 3 Steps

### Option 1: Run Locally (Fastest)

```bash
cd BlazorWeb
dotnet run
```

Open browser: `http://localhost:5000`

### Option 2: Docker (Recommended)

```bash
docker-compose up -d
```

Open browser: `http://localhost:8080`

### Option 3: Build Scripts (Easiest)

**Windows:**
```cmd
cd BlazorWeb
run.bat
```

**Linux/Mac:**
```bash
cd BlazorWeb
chmod +x run.sh
./run.sh
```

## 📋 Prerequisites

- **.NET 8.0 SDK** (for local development)
- **Docker** (for containerized deployment)
- **Docker Compose** (optional, for easy deployment)

## 🔧 Configuration

Edit `BlazorWeb/appsettings.json`:

```json
{
  "GitHub": {
    "Owner": "otherdeniz",
    "Repository": "CnC_RulesEditor"
  }
}
```

## 🐳 Docker Commands

### Build
```bash
docker build -t cnc-web -f BlazorWeb/Dockerfile .
```

### Run
```bash
docker run -d -p 8080:8080 --name cnc-web cnc-web
```

### Stop
```bash
docker stop cnc-web
docker rm cnc-web
```

### Logs
```bash
docker logs -f cnc-web
```

## 🔄 Docker Compose Commands

### Start
```bash
docker-compose up -d
```

### Stop
```bash
docker-compose down
```

### Restart
```bash
docker-compose restart
```

### Rebuild
```bash
docker-compose up -d --build
```

### Logs
```bash
docker-compose logs -f
```

## 🌐 Access the Application

- **Local Development**: http://localhost:5000
- **Docker**: http://localhost:8080
- **Docker Compose**: http://localhost:8080

## 📚 Pages

- **Home**: `/` - Project overview
- **Features**: `/features` - Comprehensive feature list
- **Tutorial**: `/tutorial` - Beginner guide
- **Download**: `/download` - Dynamic download page with GitHub releases

## 🛠️ Development

### Watch Mode (Hot Reload)
```bash
cd BlazorWeb
dotnet watch run
```

### Build Release
```bash
dotnet build -c Release
```

### Publish
```bash
dotnet publish -c Release -o ./publish
```

## 🔍 Troubleshooting

### Port Already in Use
```bash
# Use different port
docker run -p 8081:8080 cnc-web
```

### CSS Not Loading
1. Check `wwwroot/css/style.css` exists
2. Clear browser cache (Ctrl+F5)
3. Check browser console for errors

### GitHub API Rate Limit
- Limit: 60 requests/hour (unauthenticated)
- Solution: Add GitHub token to configuration
- Fallback: App uses cached data

### Docker Build Fails
```bash
# Clean Docker cache
docker system prune -a

# Rebuild
docker-compose up -d --build
```

## 📖 Documentation

- **README.md**: Full documentation
- **PROJECT_SUMMARY.md**: Project overview
- **Dockerfile**: Container configuration
- **docker-compose.yml**: Orchestration setup

## 🎯 Next Steps

1. **Customize**: Edit Razor components in `Components/Pages/`
2. **Style**: Modify `wwwroot/css/style.css`
3. **Configure**: Update `appsettings.json`
4. **Deploy**: Use Docker or publish to Azure/AWS

## 💡 Tips

- Use `dotnet watch run` for development
- Use Docker Compose for production
- Check logs with `docker-compose logs -f`
- Update GitHub settings in `appsettings.json`

## 🆘 Need Help?

- Check **README.md** for detailed documentation
- View **PROJECT_SUMMARY.md** for architecture overview
- Check GitHub issues: https://github.com/otherdeniz/CnC_RulesEditor/issues

## ✅ Verification

Test the application:

1. **Homepage**: Should load with hero section
2. **Features**: Should display feature grid
3. **Tutorial**: Should show step-by-step guide
4. **Download**: Should fetch latest GitHub release

If all pages load correctly, you're ready to go! 🎉

---

**Quick Links:**
- [Full README](README.md)
- [Project Summary](PROJECT_SUMMARY.md)
- [GitHub Repository](https://github.com/otherdeniz/CnC_RulesEditor)

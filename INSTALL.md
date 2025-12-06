# 🚀 Installation Guide

## 📦 Universal Installation (Works Everywhere)

### Quick Installation
```bash
# 1. Clone the repository
git clone https://github.com/shewart/dotnet-backend-template.git
cd dotnet-backend-template

# 2. Install the template
# Windows
./install-template.ps1

# Linux/Mac
chmod +x install-template.sh
./install-template.sh

# 3. Create your project
dotnet new dotnet-backend -n MyApi
cd MyApi
dotnet run --project MyApi.Api
```

### Alternative: Direct Installation
```bash
# From template directory
dotnet new install . --force

# Create project
dotnet new dotnet-backend -n MyApi
```

## 🧪 Test Installation

```bash
# Check if template is installed
dotnet new list | grep dotnet-backend

# Create test project
dotnet new dotnet-backend -n TestAPI
cd TestAPI
dotnet build
dotnet run --project TestAPI.Api
```

## 🛠️ Troubleshooting

### Template already exists
```bash
# Uninstall existing
dotnet new uninstall Dotnet.Backend.Template

# Reinstall
dotnet new install . --force
```

### Permission denied (Linux/Mac)
```bash
chmod +x install-template.sh
./install-template.sh
```

### WSL issues on Windows
```bash
# Use PowerShell instead
powershell -ExecutionPolicy Bypass -File install-template.ps1

# Or use direct command
dotnet new install . --force
```

---

**✅ Once installed, you can create unlimited projects with:**
```bash
dotnet new dotnet-backend -n YourProjectName
```

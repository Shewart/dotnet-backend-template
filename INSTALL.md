# 🚀 Installation Guide

## 📦 Universal Installation (Works Everywhere)

### Method 1: Direct dotnet command (Recommended)
```bash
# From template directory
dotnet new install . --force

# Create your project  
dotnet new dotnet-backend -n MyAwesomeAPI
cd MyAwesomeAPI
dotnet run --project MyAwesomeAPI.Api
```

### Method 2: Platform-specific scripts
```bash
# Windows PowerShell/CMD
.\install-template.ps1

# Linux/Mac/Git Bash  
./install-template.sh

# If permission denied on Linux/Mac
chmod +x install-template.sh
./install-template.sh
```

### Method 3: Manual step-by-step
```bash
# 1. Clone template
git clone https://github.com/safalabs/dotnet-backend-template.git
cd dotnet-backend-template

# 2. Install template
dotnet new install . --force

# 3. Create new project
mkdir my-new-api
cd my-new-api
dotnet new dotnet-backend -n MyAPI

# 4. Run your API
cd MyAPI
dotnet run --project MyAPI.Api
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
dotnet new uninstall SafaLabs.Dotnet.Backend.Template

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

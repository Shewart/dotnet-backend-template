# 📋 Template Usage Guide

## 🚀 Installation Methods

### Method 1: Local Installation (Development)
```bash
# Clone the repository
git clone https://github.com/shewart/dotnet-backend-template.git
cd dotnet-backend-template

# Install locally (Windows)
./install-template.ps1

# Install locally (Linux/Mac)  
./install-template.sh
```

### Method 2: NuGet Package (Coming Soon)
```bash
# Install from NuGet Gallery
dotnet new install Dotnet.Backend.Template

# Create new project
dotnet new dotnet-backend -n MyAwesomeAPI
```

## 🎯 Creating Projects

### Basic Usage
```bash
# Create a new API project
dotnet new dotnet-backend -n MyAPI
cd MyAPI
dotnet run --project MyAPI.Api
```

### Advanced Usage with Parameters
```bash
# Create with specific database and framework
dotnet new dotnet-backend -n MyAPI \\
  --Framework net8.0 \\
  --DatabaseProvider MongoDB \\
  --DatabaseName MyAppDatabase \\
  --UseSwagger true \\
  --IncludeTests false
```

## 📋 Available Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `--Framework` | choice | net9.0 | Target framework (net9.0, net8.0) |
| `--DatabaseProvider` | choice | MongoDB | Database provider (MongoDB, PostgreSQL*, SqlServer*) |
| `--UseSwagger` | bool | true | Include Swagger/OpenAPI documentation |
| `--IncludeTests` | bool | false | Include test projects |
| `--DatabaseName` | string | MyAppDb | Default database name |

*Coming soon

## 🔧 Post-Creation Steps

### 1. Configure Database
Update `appsettings.json` with your database connection:

**MongoDB:**
```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017"
  },
  "MongoDbSettings": {
    "DatabaseName": "YourAppDb",
    "Collections": {
      "Products": "products"
    }
  }
}
```

### 2. Run the Application
```bash
dotnet run --project YourApp.Api
```

### 3. Test the API
```bash
# Get all products
curl http://localhost:5087/api/products

# Create a product
curl -X POST http://localhost:5087/api/products \\
  -H "Content-Type: application/json" \\
  -d '{"name":"Test Product","price":99.99,"category":"Test"}'
```

### 4. View API Documentation
Navigate to: http://localhost:5087/swagger

## 🏗️ Extending the Template

### Adding New Entities

1. **Create Entity** (`YourApp.Core/Entities/`)
2. **Register Service** (`Program.cs`)
3. **Add Endpoints** (`Program.cs`)

See [main README](README.md#-adding-new-entities) for detailed examples.

## 🧪 Testing

Use the included `test-api.http` file with VS Code REST Client extension:

```http
### Test your endpoints
GET http://localhost:5087/api/products
```

## 🐛 Troubleshooting

### Template Installation Issues
```bash
# Uninstall existing template
dotnet new uninstall Dotnet.Backend.Template

# Reinstall
dotnet new install path/to/template
```

### Runtime Issues
```bash
# Restore packages
dotnet restore

# Clean and rebuild
dotnet clean
dotnet build

# Check for port conflicts
netstat -an | findstr 5087
```

## 📚 Next Steps

1. **Read the documentation**: [README.md](README.md)
2. **Database-specific guides**: [MongoDB README](MongoDB-README.md)
3. **Join discussions**: [GitHub Discussions](https://github.com/shewart/dotnet-backend-template/discussions)
4. **Contribute**: See [Contributing Guidelines](README.md#-contributing)

---

**Happy coding!** 🚀

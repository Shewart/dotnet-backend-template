# 🚀 .NET Backend Template

[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![MongoDB](https://img.shields.io/badge/MongoDB-Ready-green.svg)](MongoDB-README.md)

> **Production-ready .NET backend template with Clean Architecture, Generic CRUD, and Minimal APIs**

A modern, scalable .NET backend template that gets you from zero to production-ready API in minutes. Built with Clean Architecture principles, featuring database-agnostic design and minimal boilerplate.

## ✨ Features

🏗️ **Clean Architecture** - Proper separation of concerns across layers  
⚡ **Minimal APIs** - Lightweight, fast HTTP endpoints  
🗃️ **Multi-Database** - MongoDB and PostgreSQL with EF Core support  
📦 **Template Package** - Install via `dotnet new`  
🔧 **Easy Extension** - Add new entities in minutes  
🧪 **Fully Testable** - Interface-based design for easy mocking  
📚 **OpenAPI/Swagger** - Auto-generated API documentation  
🐳 **Docker Ready** - Containerization support (coming soon)  

## 🚀 Quick Start

### 📦 Installation

**Option 1: Install as dotnet template (Recommended)**
```bash
# Install the template globally
dotnet new install Dotnet.Backend.Template

# Create new project from template
dotnet new dotnet-backend -n MyAwesomeAPI
cd MyAwesomeAPI
```

**Option 2: Clone & install locally**
```bash
git clone https://github.com/shewart/dotnet-backend-template.git
cd dotnet-backend-template

# Windows (PowerShell/CMD)
./install-template.ps1

# Linux/Mac/Git Bash
./install-template.sh

# Then create your project
dotnet new dotnet-backend -n MyAwesomeAPI
```

### ⚙️ Quick Setup

1. **Configure your database** (see [Database Providers](#-database-providers))
2. **Run the API**
   ```bash
   dotnet run --project Dotnet.Api
   ```
3. **Test endpoints**
   ```bash
   curl http://localhost:5087/api/products
   ```
4. **View API docs**: http://localhost:5087/swagger

## 🏗️ Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Dotnet.Api    │────│ Dotnet.Application │────│  Dotnet.Core   │
│  (Controllers)  │    │   (Interfaces)    │    │   (Entities)   │
│   Minimal APIs  │    │     Services      │    │    Models      │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                        │                        │
         └────────────────────────┼────────────────────────┘
                                  │
                    ┌─────────────────┐
                    │ Dotnet.Infrastructure │
                    │  (Data Access)   │
                    │   Repositories   │
                    └─────────────────┘
```

### 📂 Project Structure

```
├── Dotnet.Api/                 # 🌐 Presentation Layer
│   ├── Program.cs              # Startup & DI configuration
│   ├── appsettings.json        # Configuration
│   └── Properties/
├── Dotnet.Application/         # 📋 Application Layer  
│   └── Interfaces/             # Service contracts
├── Dotnet.Core/               # 💎 Domain Layer
│   └── Entities/              # Domain entities
├── Dotnet.Infrastructure/     # ⚙️ Infrastructure Layer
│   ├── Services/              # Data access implementations
│   └── Configuration/         # Settings & config
└── .template.config/          # Template configuration
```

## 🗃️ Database Providers

This template supports multiple databases through a generic service pattern:

| Database | Status | Guide |
|----------|--------|-------|
| **MongoDB** | ✅ Ready | [MongoDB Setup](MongoDB-README.md) |
| **PostgreSQL** | ✅ Ready | [PostgreSQL Setup](PostgreSQL-README.md) |
| **SQL Server** | 🚧 Coming Soon | [SQL Server Setup](SqlServer-README.md) |
| **SQLite** | 🚧 Planned | [SQLite Setup](SQLite-README.md) |

**Default**: MongoDB (ready out of the box)

## 📋 API Endpoints

All endpoints follow RESTful conventions with full CRUD operations:

```http
GET    /api/products        # Get all products
GET    /api/products/{id}   # Get product by ID
POST   /api/products        # Create new product  
PUT    /api/products/{id}   # Update product
DELETE /api/products/{id}   # Delete product
GET    /api/products/count  # Get total count
GET    /api/products/paged  # Get paginated results
```

**Example Request:**
```bash
curl -X POST http://localhost:5087/api/products \\
  -H "Content-Type: application/json" \\
  -d '{
    "name": "MacBook Pro",
    "description": "Apple MacBook Pro 16-inch",
    "price": 2499.99,
    "category": "Electronics",
    "inStock": true
  }'
```

## 🔧 Adding New Entities

The template makes it incredibly easy to add new entities:

### 1. Create Entity
```csharp
// Dotnet.Core/Entities/Customer.cs
public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public required string Name { get; set; }
    public required string Email { get; set; }
}
```

### 2. Register Service
```csharp
// Program.cs
builder.Services.AddScoped<IMongoService<Customer>>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoService<Customer>(database, settings!.Collections.Customers);
});
```

### 3. Add API Endpoints
```csharp
// Program.cs
var customersApi = app.MapGroup("/api/customers").WithTags("Customers");

customersApi.MapGet("/", async (IMongoService<Customer> service) =>
    Results.Ok(await service.GetAllAsync()));
// ... other CRUD endpoints
```

**That's it!** 🎉 You now have a fully functional Customer API.

## 🧪 Testing

The template includes comprehensive testing setup:

```bash
# Run included API tests
# Use test-api.http with REST Client in VS Code

# Unit tests (coming soon)
dotnet test

# Integration tests (coming soon)  
dotnet test --filter Category=Integration
```

## 🐳 Deployment

### Docker (Coming Soon)
```bash
docker build -t my-api .
docker run -p 5000:8080 my-api
```

### Azure (Coming Soon)
```bash
az webapp create --resource-group myRG --plan myPlan --name my-api
```

## 🛣️ Roadmap

### 🎯 Current Version (v1.1)
- ✅ MongoDB support with generic CRUD
- ✅ PostgreSQL support with Entity Framework Core
- ✅ Minimal APIs with full CRUD operations
- ✅ Clean Architecture foundation
- ✅ OpenAPI/Swagger documentation
- ✅ Template installation system
- ✅ CQRS-ready service pattern

### 🚀 Upcoming Features

**v1.2 - Multi-Database Universe** 🗃️
- **SQL Server Provider** - Enterprise-ready with advanced features
- **SQLite Provider** - Lightweight option for development/testing
- **Cosmos DB Provider** - Azure cloud-native database support

**v1.2 - Communication Hub** 📡
- **Resend Email Service** - Modern email API with .NET SDK
- **SMS Integration** - Twilio/MessageBird support  
- **Push Notifications** - Firebase/APNs integration
- **WebSocket Real-time** - Live updates and chat functionality
- **Webhook Management** - Secure webhook handling and verification

**v1.3 - File & Document Powerhouse** 📁
- **Multi-Storage Upload** - Local, Azure Blob, AWS S3, Cloudinary
- **Image Processing** - Resize, crop, watermark, format conversion
- **PDF Generation** - Advanced reporting with templates and charts
- **Document Conversion** - Word/Excel to PDF, HTML to PDF
- **QR Code Generation** - Dynamic QR codes with analytics
- **Digital Signatures** - Document signing with certificates

**v1.4 - Security & Identity Fortress** 🔐
- **JWT Authentication** - Access/refresh token management
- **OAuth2 Providers** - Google, GitHub, Microsoft, Apple integration
- **Role-Based Authorization** - Flexible permission system
- **API Key Management** - Scoped keys with rate limiting
- **Two-Factor Authentication** - TOTP, SMS, Email verification
- **Password Security** - Breach detection, strength validation

**v1.5 - AI & Smart Integrations** 🤖
- **OpenAI Integration** - GPT-4, embeddings, text analysis
- **Azure Cognitive Services** - Translation, sentiment, OCR
- **Smart Image Analysis** - Auto-tagging, content moderation
- **Chatbot Framework** - Conversational AI with context
- **Content Generation** - Auto-generate descriptions, summaries
- **Recommendation Engine** - ML-powered suggestions

**v1.6 - Performance & Monitoring Beast** ⚡
- **Redis Caching** - Distributed caching with invalidation
- **Rate Limiting** - Advanced throttling with Redis
- **Health Checks** - Comprehensive system monitoring
- **Structured Logging** - Serilog with enrichers and sinks
- **Metrics & Telemetry** - Prometheus, Application Insights
- **Background Jobs** - Hangfire integration with dashboards

**v1.7 - DevOps & Cloud Ready** 🐳
- **Docker Multi-Stage** - Optimized containerization
- **Kubernetes Manifests** - Production-ready K8s deployment
- **Helm Charts** - Parameterized K8s deployments
- **CI/CD Templates** - GitHub Actions, Azure DevOps pipelines
- **Infrastructure as Code** - Terraform, ARM templates
- **Environment Management** - Config per environment

**v2.0 - Microservices & Enterprise** 🏢
- **Service Mesh Ready** - Istio/Linkerd integration
- **Event-Driven Architecture** - RabbitMQ, Azure Service Bus
- **CQRS & Event Sourcing** - Advanced patterns
- **Multi-Tenant Support** - SaaS-ready architecture
- **Distributed Tracing** - OpenTelemetry integration
- **GraphQL Gateway** - Federated APIs
- **Advanced Security** - Zero-trust architecture, mTLS

## 🤝 Contributing

We welcome contributions! Here's how to get started:

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Make your changes** following our coding standards
4. **Add tests** for new functionality
5. **Submit a pull request**

### 💬 Join the Discussion

Have questions or ideas? Join our [GitHub Discussions](https://github.com/shewart/dotnet-backend-template/discussions)!

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Built with ❤️ by [shewart](https://github.com/shewart)
- Inspired by Clean Architecture principles
- Community-driven development

---

**⭐ Star this repo if it helped you build something awesome!**

**🚀 Ready to build your next API?** [Get started now](#-quick-start)
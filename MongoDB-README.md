# 🚀 .NET Backend Template with MongoDB + Minimal APIs

This template provides a clean architecture foundation with MongoDB integration and Minimal APIs for rapid API development.

## 🏗️ Architecture

```
├── Dotnet.Api/                 # 🌐 API Layer (Minimal APIs)
├── Dotnet.Application/         # 📋 Application Layer (Interfaces)
├── Dotnet.Core/               # 💎 Domain Layer (Entities)
├── Dotnet.Infrastructure/     # ⚙️  Infrastructure Layer (MongoDB)
```

## ✨ Features

- **Clean Architecture** - Separation of concerns with clear layer boundaries
- **MongoDB Integration** - Ready-to-use generic MongoDB service
- **Minimal APIs** - Fast, lightweight API endpoints
- **Generic CRUD Service** - Reusable `IMongoService<T>` for any entity
- **Dependency Injection** - Properly configured DI container
- **Pagination Support** - Built-in pagination for large datasets
- **OpenAPI/Swagger** - Auto-generated API documentation

## 🚀 Quick Start

### 1. Prerequisites
- .NET 9.0 SDK
- MongoDB (local or remote)

### 2. Clone & Setup
```bash
git clone <your-repo>
cd dotnet-backend-template
dotnet restore
```

### 3. Configure MongoDB
Update `appsettings.json`:
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

### 4. Run the API
```bash
dotnet run --project Dotnet.Api
```

## 📋 API Endpoints

Base URL: `https://localhost:7029/api`

### Products
- `GET /products` - Get all products
- `GET /products/{id}` - Get product by ID
- `POST /products` - Create new product
- `PUT /products/{id}` - Update product
- `DELETE /products/{id}` - Delete product
- `GET /products/count` - Get total count
- `GET /products/paged?page=1&pageSize=10` - Get paginated products

## 🧪 Testing

Use the included `test-api.http` file with REST Client extension in VS Code:

```http
### Create a Product
POST https://localhost:7029/api/products
Content-Type: application/json

{
  "name": "MacBook Pro",
  "description": "Apple MacBook Pro 16-inch",
  "price": 2499.99,
  "category": "Electronics",
  "inStock": true
}
```

## 🔧 Adding New Entities

### 1. Create Entity
```csharp
// Dotnet.Core/Entities/Customer.cs
public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public required string Name { get; set; }
    // ... other properties
}
```

### 2. Update Settings
```json
// appsettings.json
"Collections": {
  "Products": "products",
  "Customers": "customers"  // Add this
}
```

### 3. Register Service
```csharp
// Program.cs
builder.Services.AddScoped<IMongoService<Customer>>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoService<Customer>(database, settings!.Collections.Customers);
});
```

### 4. Add API Endpoints
```csharp
// Program.cs
var customersApi = app.MapGroup("/api/customers").WithTags("Customers");

customersApi.MapGet("/", async (IMongoService<Customer> service) =>
    Results.Ok(await service.GetAllAsync()));
// ... other endpoints
```

## 🎯 Benefits

✅ **Type-Safe** - Strongly typed entities and services  
✅ **Scalable** - Generic service pattern for rapid development  
✅ **Testable** - Interface-based design for easy mocking  
✅ **Production-Ready** - Proper error handling and async operations  
✅ **Clean** - Follows SOLID principles and Clean Architecture  
✅ **Flexible** - Easy to extend or swap persistence layers  

## 🔄 Future Enhancements

- Add validation with FluentValidation
- Implement authentication/authorization
- Add logging with Serilog
- Include health checks
- Add unit tests
- Implement caching
- Add rate limiting

---

**Happy Coding!** 🎉 This template gets you from zero to production-ready API in minutes!

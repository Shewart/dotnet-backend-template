# 🚀 .NET Backend Template with PostgreSQL + Entity Framework Core

This template provides a clean architecture foundation with PostgreSQL integration and Minimal APIs for rapid API development using CQRS patterns.

## 🏗️ Architecture

```
├── Dotnet.Api/                 # 🌐 API Layer (Minimal APIs)
├── Dotnet.Application/         # 📋 Application Layer (Interfaces)
├── Dotnet.Core/               # 💎 Domain Layer (Entities)
├── Dotnet.Infrastructure/     # ⚙️  Infrastructure Layer (EF Core + PostgreSQL)
```

## ✨ Features

- **Clean Architecture** - Separation of concerns with clear layer boundaries
- **PostgreSQL Integration** - Entity Framework Core with Npgsql provider
- **CQRS Pattern** - Command/Query separation for scalable operations
- **Generic Service Pattern** - Reusable `IService<T>` for any entity
- **Dependency Injection** - Properly configured DI container
- **Migration Support** - EF Core code-first migrations
- **OpenAPI/Swagger** - Auto-generated API documentation

## 🚀 Quick Start

### 1. Prerequisites
- .NET 9.0 SDK
- PostgreSQL (local or remote)

### 2. Configure PostgreSQL
Update `appsettings.json`:
```json
{
  "DatabaseProvider": "PostgreSQL",
  "ConnectionStrings": {
    "PostgreSql": "Host=localhost;Database=DotnetTemplateDb;Username=postgres;Password=postgres"
  }
}
```

### 3. Run Migrations
```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Run migrations
dotnet ef migrations add InitialCreate --project Dotnet.Infrastructure
dotnet ef database update --project Dotnet.Infrastructure
```

### 4. Run the API
```bash
dotnet run --project Dotnet.Api
```

## 📋 API Endpoints

Base URL: `https://localhost:7029/api`

### Products
- `GET /products` - Get all products
- `GET /products/{id}` - Get product by ID (integer)
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

### 1. Create Domain Entity
```csharp
// Dotnet.Core/Entities/Customer.cs
public class Customer
{
    public string Id { get; set; } // Will be string for API compatibility
    public required string Name { get; set; }
    public required string Email { get; set; }
    // ... other properties
}
```

### 2. Create EF Core Entity
```csharp
// Dotnet.Core/Entities/PostgreSQL/CustomerEntity.cs
[Table("customers")]
public class CustomerEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    // ... other properties
}
```

### 3. Update DbContext
```csharp
// Dotnet.Infrastructure/PostgreSQL/AppDbContext.cs
public DbSet<CustomerEntity> Customers { get; set; }

// In OnModelCreating:
modelBuilder.Entity<CustomerEntity>(entity =>
{
    entity.HasKey(e => e.Id);
    // ... configuration
});
```

### 4. Create Service
```csharp
// Dotnet.Infrastructure/PostgreSQL/PostgreSqlService.cs
// Add mapping methods and update interface to support Customer
```

### 5. Register Service
```csharp
// Dotnet.Api/Program.cs
// Add Customer service registration
```

## 🎯 Benefits

✅ **ACID Compliant** - PostgreSQL's strong consistency guarantees
✅ **Relational Data** - Natural modeling of complex relationships
✅ **Rich Querying** - Advanced SQL features and indexing
✅ **Migrations** - Version-controlled schema changes
✅ **Performance** - Optimized for OLTP workloads
✅ **Scalable** - Ready for CQRS and event sourcing patterns

## 🗃️ Database Schema

The template creates the following tables:

```sql
-- Products table (example)
CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(18,2) NOT NULL,
    category VARCHAR(50) NOT NULL,
    in_stock BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Indexes for performance
CREATE INDEX idx_products_category ON products(category);
CREATE INDEX idx_products_created_at ON products(created_at DESC);
```

## 🔄 Switching Between Databases

To switch between MongoDB and PostgreSQL:

1. Update `appsettings.json`:
```json
{
  "DatabaseProvider": "PostgreSQL" // or "MongoDB"
}
```

2. For PostgreSQL, ensure migrations are run:
```bash
dotnet ef database update --project Dotnet.Infrastructure
```

3. For MongoDB, ensure MongoDB is running and configured.

## 🚀 Production Considerations

- **Connection Pooling** - Npgsql handles connection pooling automatically
- **Migrations** - Always backup before running migrations
- **Indexes** - Monitor query performance and add indexes as needed
- **Backup Strategy** - Implement regular PostgreSQL backups
- **Monitoring** - Use PostgreSQL's built-in statistics and monitoring tools

---

**Happy Coding!** 🎉 This template gives you the choice between MongoDB's flexibility and PostgreSQL's reliability!

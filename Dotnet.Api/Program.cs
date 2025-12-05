using Dotnet.Application.Interfaces;
using Dotnet.Core.Entities;
using Dotnet.Infrastructure.Configuration;
using Dotnet.Infrastructure.Services;
using Dotnet.Infrastructure.PostgreSQL;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Get database provider from configuration (default to MongoDB)
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "MongoDB";

// Configure database services based on provider
if (databaseProvider == "PostgreSQL")
{
    builder.Services.AddPostgreSql(builder.Configuration);
}
else // Default to MongoDB
{
    // Configure MongoDB Settings
    builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings"));

    // Configure MongoDB
    builder.Services.AddSingleton<IMongoClient>(sp =>
    {
        var connectionString = builder.Configuration.GetConnectionString("MongoDb");
        return new MongoClient(connectionString);
    });

    builder.Services.AddScoped<IMongoDatabase>(sp =>
    {
        var client = sp.GetRequiredService<IMongoClient>();
        var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
        return client.GetDatabase(settings!.DatabaseName);
    });

    // Register MongoService for Product
    builder.Services.AddScoped<IService<Product>>(sp =>
    {
        var database = sp.GetRequiredService<IMongoDatabase>();
        var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
        return new MongoService<Product>(database, settings!.Collections.Products);
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Product API Endpoints
var productsApi = app.MapGroup("/api/products").WithTags("Products");

// GET /api/products - Get all products
productsApi.MapGet("/", async (IService<Product> service) =>
{
    var products = await service.GetAllAsync();
    return Results.Ok(products);
})
.WithName("GetProducts")
.WithSummary("Get all products");

// GET /api/products/{id} - Get product by id
productsApi.MapGet("/{id}", async (IService<Product> service, string id) =>
{
    var product = await service.GetByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
})
.WithName("GetProduct")
.WithSummary("Get product by ID");

// POST /api/products - Create new product
productsApi.MapPost("/", async (IService<Product> service, Product product) =>
{
    var createdProduct = await service.CreateAsync(product);
    return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
})
.WithName("CreateProduct")
.WithSummary("Create a new product");

// PUT /api/products/{id} - Update product
productsApi.MapPut("/{id}", async (IService<Product> service, string id, Product product) =>
{
    var updatedProduct = await service.UpdateAsync(id, product);
    return updatedProduct is not null ? Results.Ok(updatedProduct) : Results.NotFound();
})
.WithName("UpdateProduct")
.WithSummary("Update an existing product");

// DELETE /api/products/{id} - Delete product
productsApi.MapDelete("/{id}", async (IService<Product> service, string id) =>
{
    var deleted = await service.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteProduct")
.WithSummary("Delete a product");

// GET /api/products/count - Get total count
productsApi.MapGet("/count", async (IService<Product> service) =>
{
    var count = await service.GetCountAsync();
    return Results.Ok(new { Count = count });
})
.WithName("GetProductCount")
.WithSummary("Get total product count");

// GET /api/products/paged - Get paged products
productsApi.MapGet("/paged", async (IService<Product> service, int page = 1, int pageSize = 10) =>
{
    var products = await service.GetPagedAsync(page, pageSize);
    var totalCount = await service.GetCountAsync();

    return Results.Ok(new
    {
        Products = products,
        Page = page,
        PageSize = pageSize,
        TotalCount = totalCount,
        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
    });
})
.WithName("GetProductsPaged")
.WithSummary("Get products with pagination");

app.Run();

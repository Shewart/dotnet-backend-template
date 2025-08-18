using Dotnet.Application.Interfaces;
using Dotnet.Core.Entities;
using Dotnet.Infrastructure.Configuration;
using Dotnet.Infrastructure.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
builder.Services.AddScoped<IMongoService<Product>>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoService<Product>(database, settings!.Collections.Products);
});

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
productsApi.MapGet("/", async (IMongoService<Product> service) =>
{
    var products = await service.GetAllAsync();
    return Results.Ok(products);
})
.WithName("GetProducts")
.WithSummary("Get all products");

// GET /api/products/{id} - Get product by id
productsApi.MapGet("/{id}", async (IMongoService<Product> service, string id) =>
{
    var product = await service.GetByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
})
.WithName("GetProduct")
.WithSummary("Get product by ID");

// POST /api/products - Create new product
productsApi.MapPost("/", async (IMongoService<Product> service, Product product) =>
{
    var createdProduct = await service.CreateAsync(product);
    return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
})
.WithName("CreateProduct")
.WithSummary("Create a new product");

// PUT /api/products/{id} - Update product
productsApi.MapPut("/{id}", async (IMongoService<Product> service, string id, Product product) =>
{
    var updatedProduct = await service.UpdateAsync(id, product);
    return updatedProduct is not null ? Results.Ok(updatedProduct) : Results.NotFound();
})
.WithName("UpdateProduct")
.WithSummary("Update an existing product");

// DELETE /api/products/{id} - Delete product
productsApi.MapDelete("/{id}", async (IMongoService<Product> service, string id) =>
{
    var deleted = await service.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteProduct")
.WithSummary("Delete a product");

// GET /api/products/count - Get total count
productsApi.MapGet("/count", async (IMongoService<Product> service) =>
{
    var count = await service.GetCountAsync();
    return Results.Ok(new { Count = count });
})
.WithName("GetProductCount")
.WithSummary("Get total product count");

// GET /api/products/paged - Get paged products
productsApi.MapGet("/paged", async (IMongoService<Product> service, int page = 1, int pageSize = 10) =>
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

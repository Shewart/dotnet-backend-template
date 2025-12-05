using Dotnet.Application.Interfaces;
using Dotnet.Core.Entities;
using Dotnet.Core.Entities.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Infrastructure.PostgreSQL;

public class PostgreSqlService : IService<Product>
{
    private readonly AppDbContext _context;

    public PostgreSqlService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var entities = await _context.Products
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return entities.Select(MapToDomain).ToList();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        if (!int.TryParse(id, out var intId))
            return null;

        var entity = await _context.Products.FindAsync(intId);
        return entity != null ? MapToDomain(entity) : null;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        var entity = MapFromDomain(product);
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();

        // Reload to get the generated ID
        await _context.Entry(entity).ReloadAsync();
        return MapToDomain(entity);
    }

    public async Task<Product?> UpdateAsync(string id, Product product)
    {
        if (!int.TryParse(id, out var intId))
            return null;

        var existingEntity = await _context.Products.FindAsync(intId);
        if (existingEntity == null)
            return null;

        // Update properties
        existingEntity.Name = product.Name;
        existingEntity.Description = product.Description;
        existingEntity.Price = product.Price;
        existingEntity.Category = product.Category;
        existingEntity.InStock = product.InStock;
        existingEntity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDomain(existingEntity);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (!int.TryParse(id, out var intId))
            return false;

        var entity = await _context.Products.FindAsync(intId);
        if (entity == null)
            return false;

        _context.Products.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<long> GetCountAsync()
    {
        return await _context.Products.CountAsync();
    }

    public async Task<List<Product>> GetPagedAsync(int page, int pageSize)
    {
        var entities = await _context.Products
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return entities.Select(MapToDomain).ToList();
    }

    private static Product MapToDomain(ProductEntity entity)
    {
        return new Product
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Category = entity.Category,
            InStock = entity.InStock,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    private static ProductEntity MapFromDomain(Product product)
    {
        return new ProductEntity
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category,
            InStock = product.InStock,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}

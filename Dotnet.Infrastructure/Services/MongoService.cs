using Dotnet.Application.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Dotnet.Infrastructure.Services;

public class MongoService<T> : IMongoService<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoService(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out _))
            return default;

        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<T?> UpdateAsync(string id, T entity)
    {
        if (!ObjectId.TryParse(id, out _))
            return default;

        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = await _collection.ReplaceOneAsync(filter, entity);
        
        return result.MatchedCount > 0 ? entity : default;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (!ObjectId.TryParse(id, out _))
            return false;

        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = await _collection.DeleteOneAsync(filter);
        
        return result.DeletedCount > 0;
    }

    public async Task<long> GetCountAsync()
    {
        return await _collection.CountDocumentsAsync(_ => true);
    }

    public async Task<List<T>> GetPagedAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return await _collection.Find(_ => true)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync();
    }
}

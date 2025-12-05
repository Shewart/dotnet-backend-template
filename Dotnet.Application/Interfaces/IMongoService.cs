namespace Dotnet.Application.Interfaces;

public interface IService<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(string id);
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(string id, T entity);
    Task<bool> DeleteAsync(string id);
    Task<long> GetCountAsync();
    Task<List<T>> GetPagedAsync(int page, int pageSize);
}

public interface IMongoService<T> : IService<T> { }

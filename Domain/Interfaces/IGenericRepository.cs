using Domain.Entities;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(string id);
    Task<T?> GetByCompositeKeyAsync(string id, string secondId);
    Task<IReadOnlyList<T>> GetAllAsync();
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    Task<bool> ExsistsAsync(string id);
}

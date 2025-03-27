using Domain.Entities;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(string id);
    Task<T?> GetByIdWithIgnoreFilterAsync(string id);
    Task<T?> GetByCompositeKeyAsync(string id, string secondId);
    Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);
    Task<IReadOnlyList<T>> GetEntitiesWithSpecAsync(ISpecification<T> spec);
    Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpecification<T, TResult> spec);
    Task<IReadOnlyList<TResult>> GetEntitiesWithSpecAsync<TResult>(ISpecification<T, TResult> spec);
    Task<IReadOnlyList<T>> GetAllAsync();
    void Add(T entity);
    void Remove(T entity);
    void DeleteRange(ICollection<T> entities);
    void AddRange(ICollection<T> entities);
}

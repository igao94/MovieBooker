using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity) => context.Set<T>().Add(entity);

    public async Task<IReadOnlyList<T>> GetAllAsync() => await context.Set<T>().ToListAsync();

    public async Task<T?> GetByIdAsync(string id) => await context.Set<T>().FindAsync(id);

    public void Remove(T entity) => context.Set<T>().Remove(entity);

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);

        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> ExsistsAsync(string id) => await context.Set<T>().AnyAsync(x => x.Id == id);
}

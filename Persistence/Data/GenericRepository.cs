using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Specifications;

namespace Persistence.Data;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _context = context.Set<T>();

    public void Add(T entity) => _context.Add(entity);

    public async Task<IReadOnlyList<T>> GetAllAsync() => await _context.ToListAsync();

    public async Task<T?> GetByIdAsync(string id) => await _context.FindAsync(id);

    public void Remove(T entity) => _context.Remove(entity);

    public void Update(T entity)
    {
        _context.Attach(entity);

        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> ExsistsAsync(string id) => await _context.AnyAsync(x => x.Id == id);

    public async Task<T?> GetByCompositeKeyAsync(string id, string secondId)
    {
        return await _context.FindAsync(id, secondId);
    }

    public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetEntitiesWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context, spec);
    }

    public void DeleteRange(ICollection<T> entities)
    {
        _context.RemoveRange(entities);
    }
}

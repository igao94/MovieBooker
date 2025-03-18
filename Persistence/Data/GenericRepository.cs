using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
}

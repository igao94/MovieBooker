﻿using Domain.Entities;
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

    public async Task<T?> GetByIdWithIgnoreFilterAsync(string id)
    {
        return await _context.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Remove(T entity) => _context.Remove(entity);

    public async Task<T?> GetByCompositeKeyAsync(string id, string secondId)
    {
        return await _context.FindAsync(id, secondId);
    }

    public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }    
    
    public async Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetEntitiesWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }    
    
    public async Task<IReadOnlyList<TResult>> GetEntitiesWithSpecAsync<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public void DeleteRange(ICollection<T> entities) => _context.RemoveRange(entities);

    public void AddRange(ICollection<T> entities) => _context.AddRange(entities);

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context, spec);
    }    
    
    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context, spec);
    }
}

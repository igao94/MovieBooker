using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Persistence.Data;

public class UnitOfWork(AppDbContext context,
    IAccountRepository accountRepository) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();

    public IAccountRepository AccountRepository => accountRepository;

    public async Task<bool> CompleteAsync() => await context.SaveChangesAsync() > 0;

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, t =>
        {
            var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));

            return Activator.CreateInstance(repositoryType, context)
                ?? throw new InvalidOperationException($"Could not create repository instance for ${t}.");
        });
    }
}

using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    IAccountRepository AccountRepository { get; }
    Task<bool> CompleteAsync();
}

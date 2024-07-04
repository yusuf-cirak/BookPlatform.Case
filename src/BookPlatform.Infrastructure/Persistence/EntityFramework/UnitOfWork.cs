using BookPlatform.Infrastructure.Persistence.EntityFramework.Contexts;
using BookPlatform.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookPlatform.Infrastructure.Persistence.EntityFramework;

public interface IUnitOfWork
{
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    void CommitTransaction();

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public sealed class UnitOfWork(BookPlatformDbContext context) : IUnitOfWork, IScopedService
{
    public IDbContextTransaction BeginTransaction()
    {
        return context.Database.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return context.Database.BeginTransactionAsync(cancellationToken);
    }

    public void CommitTransaction()
    {
        context.Database.CommitTransaction();
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        return context.Database.CommitTransactionAsync(cancellationToken);
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        return context.Database.RollbackTransactionAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
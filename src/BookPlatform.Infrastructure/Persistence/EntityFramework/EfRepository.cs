using BookPlatform.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Infrastructure.Persistence.EntityFramework;

public interface IRepository<T> where T : Entity
{
    DbSet<T> Set();
}

public sealed class EfRepository<TDbContext>(TDbContext context)
    where TDbContext : DbContext
{
    public DbSet<T> Set<T>() where T : Entity
    {
        return context.Set<T>();
    }
}
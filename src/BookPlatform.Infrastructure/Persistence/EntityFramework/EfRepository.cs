using BookPlatform.Domain;
using BookPlatform.Infrastructure.Persistence.EntityFramework.Contexts;
using BookPlatform.SharedKernel.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Infrastructure.Persistence.EntityFramework;

public interface IEfRepository
{
    DbSet<User> Users { get; }
    DbSet<Book> Books { get; }
    DbSet<BookNote> BookNotes { get; }
    
    DbSet<UserFriend> UserFriends { get; }

}

public sealed class EfRepository(BookPlatformDbContext context) : IEfRepository, IScopedService
{
    public DbSet<User> Users { get; } = context.Set<User>();
    public DbSet<Book> Books { get; } = context.Set<Book>();
    public DbSet<BookNote> BookNotes { get; } = context.Set<BookNote>();
    
    public DbSet<UserFriend> UserFriends { get; } = context.Set<UserFriend>();
}
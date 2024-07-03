using System.Reflection;
using BookPlatform.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Infrastructure.Persistence.EntityFramework.Contexts;

public sealed class BookPlatformDbContext(DbContextOptions<BookPlatformDbContext> options)
    : IdentityDbContext<User>(options)
{
    public DbSet<Book> Books { get; set; }

    public DbSet<UserFriend> UserFriends { get; set; }

    public DbSet<BookNote> BookNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
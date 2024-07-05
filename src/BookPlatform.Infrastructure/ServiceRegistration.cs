using System.Reflection;
using BookPlatform.Domain;
using BookPlatform.Domain.ValueObjects;
using BookPlatform.Infrastructure.Helpers.Hashing;
using BookPlatform.Infrastructure.Persistence.EntityFramework.Contexts;
using BookPlatform.Infrastructure.Persistence.EntityFramework.Initializers;
using BookPlatform.Infrastructure.Persistence.EntityFramework.Interceptors;
using BookPlatform.SharedKernel.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookPlatform.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseServices(configuration);

        var assembly = Assembly.GetExecutingAssembly();

        services.AddLifetimedServices(assembly);
    }

    private static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditableEntityDateInterceptor>();

        services.AddDbContextPool<BookPlatformDbContext>((sp, opt) =>
        {
            var auditableEntityDateInterceptor = sp.GetRequiredService<AuditableEntityDateInterceptor>();

            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            opt.UseSqlServer(configuration.GetConnectionString("BookPlatformSqlServer"),
                    sqlOpt => { sqlOpt.EnableRetryOnFailure(maxRetryCount: 3); })
                .AddInterceptors(auditableEntityDateInterceptor);
        }, poolSize: 100);
    }

    public static void ApplyPendingMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookPlatformDbContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }

    public static void ApplyDatabaseInitializers(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookPlatformDbContext>();
        dbContext.InitializeFullTextSearchAsync().Wait();
        dbContext.InitializeFullTextSearchForBooksAsync().Wait();
    }

    public static void AddSeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<BookPlatformDbContext>();
        
        dbContext.Database.EnsureCreated();
        
        if (dbContext.Users.Any() || dbContext.Books.Any())
        {
            return;
        }
        
        HashingHelper.CreatePasswordHash("string", out var passwordHash, out var passwordSalt);

        List<User> users =
        [
            User.Create(GuidExtensions.GenerateGuidFromString("string").ToString(), "string", passwordHash,
                passwordSalt),
            User.Create(GuidExtensions.GenerateGuidFromString("string1").ToString(), "string1", passwordHash,
                passwordSalt)
        ];

        var shelfLocation = ShelfLocation.Create("deneme", "deneme", "deneme", "deneme");
        List<Book> books =
        [
            Book.Create(GuidExtensions.GenerateGuidFromString("deneme").ToString(), "deneme", "deneme", "deneme", 1,
                "deneme", DateTime.Now, "deneme", shelfLocation)
        ];

        dbContext.Users.AddRange(users);
        dbContext.Books.AddRange(books);
        dbContext.SaveChanges();
    }
}
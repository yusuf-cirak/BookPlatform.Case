using BookPlatform.Infrastructure.Persistence.EntityFramework.Contexts;
using BookPlatform.Infrastructure.Persistence.EntityFramework.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.WebAPI.Extensions;

public static class DatabaseExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
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
        dbContext.Database.Migrate();
    }
}
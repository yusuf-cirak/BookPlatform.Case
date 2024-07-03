using BookPlatform.Domain;
using BookPlatform.Infrastructure.Persistence.EntityFramework.Contexts;
using Microsoft.AspNetCore.Identity;

namespace BookPlatform.WebAPI.Extensions;

public static class IdentityExtensions
{
    public static void AddIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
    }

    public static void AddIdentityEndpoints(this IServiceCollection services)
    {
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<BookPlatformDbContext>()
            .AddApiEndpoints();
    }


    public static void UseIdentityAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
    }

    public static void MapIdentityEndpoints(this WebApplication app)
    {
        app.MapIdentityApi<User>()
            .WithTags("Auths");
    }
}
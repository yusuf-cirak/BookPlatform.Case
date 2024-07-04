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
        // .AddJwtBearer(options =>
        // {
        //     options.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuer = true,
        //         ValidateAudience = true,
        //         ValidateLifetime = true,
        //         ValidateIssuerSigningKey = true,
        //         ValidIssuer = configuration["TokenOptions:Issuer"],
        //         ValidAudience = configuration["TokenOptions:Audience"],
        //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenOptions:SecurityKey"]!))
        //     };
        // });
    }

    public static void AddIdentityEndpoints(this IServiceCollection services)
    {
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<BookPlatformDbContext>()
            .AddApiEndpoints();
    }


    public static void MapAuthenticationMiddleware(this IApplicationBuilder app)
    {
        app.UseAuthentication();
    }

    public static void MapIdentityEndpoints(this WebApplication app)
    {
        app.MapIdentityApi<User>()
            .WithTags("Auths");
    }
}
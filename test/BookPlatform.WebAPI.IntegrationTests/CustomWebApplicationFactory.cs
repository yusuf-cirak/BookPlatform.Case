using BookPlatform.Application.Contracts.JWT;
using BookPlatform.Infrastructure;
using BookPlatform.Infrastructure.Helpers.JWT;
using BookPlatform.Infrastructure.Persistence.EntityFramework.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookPlatform.WebAPI.IntegrationTests;

public sealed class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            JwtHelper.TokenOptions = context.Configuration.GetSection("TokenOptions").Get<TokenOptions>()!;
        });

        builder.ConfigureServices(services =>
        {
            var context =
                services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(BookPlatformDbContext));
            if (context != null)
            {
                services.Remove(context);
                var options = services.Where(r => r.ServiceType == typeof(DbContextOptions)
                                                  || r.ServiceType.IsGenericType &&
                                                  r.ServiceType.GetGenericTypeDefinition() ==
                                                  typeof(DbContextOptions<>)).ToArray();
                foreach (var option in options)
                {
                    services.Remove(option);
                }
            }

            // Add a new registration for ApplicationDbContext with an in-memory database
            services.AddDbContextPool<BookPlatformDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("Db");
            });
            
            var sp = services.BuildServiceProvider();
            
            using var scope = sp.CreateScope();
            
            var scopedServices = scope.ServiceProvider;
            
            var db = scopedServices.GetRequiredService<BookPlatformDbContext>();
            
            // db.Database.EnsureCreated();
            
            // db.AddSeedData();
        });
    }
}
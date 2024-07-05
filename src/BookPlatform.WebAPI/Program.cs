using BookPlatform.Application;
using BookPlatform.Infrastructure;
using BookPlatform.WebAPI.Extensions;
using BookPlatform.WebAPI.Infrastructure.Middlewares;

namespace BookPlatform.WebAPI;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGenServices();

        builder.Services.AddControllers();

        builder.AddSerilogLogging();

        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddApplicationServices();

        builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
        builder.Services.AddProblemDetails();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddJwtAuthenticationServices(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseSerilogLogging();

        app.UseExceptionHandler();

        app.MapAuthenticationMiddleware();
        app.MapApiEndpoints();

        app.ApplyPendingMigrations();
        app.AddSeedData();
        
        app.Run();
    }
}
using Serilog;

namespace BookPlatform.WebAPI.Extensions;

public static class LoggingExtensions
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog(logger);
    }


    public static void UseSerilogLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
    }
}
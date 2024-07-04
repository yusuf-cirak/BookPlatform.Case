using BookPlatform.WebAPI.Infrastructure.Handlers.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace BookPlatform.WebAPI.Infrastructure.Middlewares;

public sealed class GlobalExceptionHandlerMiddleware : IExceptionHandler
{
    private readonly HttpExceptionHandler _httpExceptionHandler = new();

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        _httpExceptionHandler.HttpResponse = httpContext.Response;

        await _httpExceptionHandler.HandleExceptionAsync(exception);

        return true;
    }
}
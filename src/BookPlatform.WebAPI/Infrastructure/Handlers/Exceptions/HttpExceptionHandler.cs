using BookPlatform.WebAPI.Infrastructure.Handlers.ExceptionDetails;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.WebAPI.Infrastructure.Handlers.Exceptions;

public sealed class HttpExceptionHandler : ExceptionHandler
{
    private HttpResponse? _httpResponse;

    public HttpResponse HttpResponse
    {
        get => _httpResponse ?? throw new ArgumentNullException(nameof(_httpResponse));
        set => _httpResponse = value;
    }

    public override async Task HandleExceptionAsync(Exception e)
    {
        HttpResponse.ContentType = "application/json";

        ProblemDetails response = e switch
        {
            ValidationException validationException => new ValidationExceptionDetails(validationException.Message),
            UnauthorizedAccessException unauthorizedAccessException => new UnauthorizedExceptionDetails(),
            _ => new ProblemDetails()
            {
                Detail = e.Message
            }
        };

        HttpResponse.StatusCode = (int)response.Status!;

        await HttpResponse.WriteAsJsonAsync(response);
    }
}
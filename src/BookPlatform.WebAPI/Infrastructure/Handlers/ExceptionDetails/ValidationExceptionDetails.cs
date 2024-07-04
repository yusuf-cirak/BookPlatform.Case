using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.WebAPI.Infrastructure.Handlers.ExceptionDetails;

public sealed class ValidationExceptionDetails : ProblemDetails
{
    public ValidationExceptionDetails(string detail)
    {
        Title = "Validation error";
        Detail = detail;
        Status = 400;
    }
}
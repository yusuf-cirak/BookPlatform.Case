using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.WebAPI.Infrastructure.Handlers.ExceptionDetails;

public sealed class UnauthorizedExceptionDetails : ProblemDetails
{
    public UnauthorizedExceptionDetails()
    {
        Title = "Unauthorized";
        Detail = "You are not authorized to access this resource";
        Status = 400;
    }
}
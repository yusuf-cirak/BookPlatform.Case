using Microsoft.AspNetCore.Http;
using YC.Result;

namespace BookPlatform.SharedKernel.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResponse<TValue>(this Result<TValue> httpResult, int successStatus = 200)
    {
        return httpResult.Match(
            (success) => successStatus switch
            {
                200 => Results.Ok(success),
                201 => Results.Created(string.Empty, success),
                204 => Results.NoContent(),
                _ => Results.StatusCode(successStatus)
            },
            (failure) => failure.Status switch
            {
                400 => Results.BadRequest(failure),
                401 => Results.Unauthorized(),
                403 => Results.Forbid(),
                404 => Results.NotFound(failure),
                409 => Results.Conflict(failure),
                // 500 => Results.Problem((failure.ToProblemDetails())),
                _ => Results.BadRequest(failure)
            });
    }

    public static IResult ToHttpResponse(this Result result, int successStatusCode = 200)
    {
        return result.Match(
            () => successStatusCode switch
            {
                200 => Results.Ok(),
                201 => Results.Created(),
                204 => Results.NoContent(),
                _ => Results.StatusCode(successStatusCode)
            },
            (failure) => failure.Status switch
            {
                400 => Results.BadRequest(failure),
                401 => Results.Unauthorized(),
                403 => Results.Forbid(),
                404 => Results.NotFound(failure),
                409 => Results.Conflict(failure),
                // 500 => Results.Problem(failure.ToProblemDetails()),
                _ => Results.BadRequest(failure)
            });
    }
}
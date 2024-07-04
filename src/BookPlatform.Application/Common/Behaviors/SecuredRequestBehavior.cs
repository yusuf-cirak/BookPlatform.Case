using BookPlatform.Application.Common.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BookPlatform.Application.Common.Behaviors;

public sealed class SecuredRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TRequest> _logger;

    public SecuredRequestBehavior(IHttpContextAccessor httpContextAccessor, ILogger<TRequest> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var claims = _httpContextAccessor?.HttpContext?.User.Claims;

        if (claims == null || !claims.Any())
        {
            _logger.LogWarning("Unauthorized request made by {RequestType}", typeof(TRequest).Name);
            throw new UnauthorizedAccessException();
        }

        return next();
    }
}
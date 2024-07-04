using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BookPlatform.Application.Common.Services;

public interface ICurrentUserService
{
    ClaimsPrincipal? User { get; }

    bool IsAuthenticated { get; }

    string GetUserId();
}

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService, IScopedService
{
    public ClaimsPrincipal? User { get; } = httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated { get; } = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public string GetUserId() => this.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}
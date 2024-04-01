using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Feedback.Analyzer.Infrastructure.Common.RequestContexts.Brokers;

/// <summary>
/// Provides client context information for the current request.
/// </summary>
public class RequestContextProvider(IHttpContextAccessor httpContextAccessor) : IRequestContextProvider
{
    public Guid GetUserId()
    {
        if(!IsLoggedIn())
            throw new InvalidOperationException("User is not logged in");
        
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext!.User.Claims.First(claim => claim.Type == ClaimConstants.ClientId).Value;

        return Guid.Parse(userIdClaim);
    }

    public string? GetAccessToken()
    {
        return httpContextAccessor.HttpContext?.Request.Headers.Authorization;
    }

    public bool IsLoggedIn()
    {
        return httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}
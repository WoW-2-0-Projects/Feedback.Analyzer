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
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext!.User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.ClientId)?.Value;

        return userIdClaim is not null ? Guid.Parse(userIdClaim) : throw new ArgumentNullException();
    }

    public string? GetAccessToken()
    {
        return httpContextAccessor.HttpContext?.Request.Headers.Authorization;
    }
}
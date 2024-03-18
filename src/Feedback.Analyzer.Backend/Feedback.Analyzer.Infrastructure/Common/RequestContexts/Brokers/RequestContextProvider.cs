using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Feedback.Analyzer.Infrastructure.Common.RequestContexts.Brokers;

/// <summary>
/// Provider request context data
/// </summary>
public class RequestContextProvider(IHttpContextAccessor httpContextAccessor) : IRequestContextProvider
{
    public Guid? GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext!.User.Claims.FirstOrDefault(claim => claim.Type == IdentityConstants.ClaimConstants.UserId)?.Value;

        return Guid.Parse(userIdClaim);
    }
}
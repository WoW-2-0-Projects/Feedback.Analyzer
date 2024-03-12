using System.Security.Authentication;
using Feedback.Analyzer.Application.Common.Identity.Services;
using Feedback.Analyzer.Domain.Constants;

namespace Feedback.Analyzer.Api.Middlewares;

/// <summary>
/// Middleware for validating access tokens.
/// </summary>
public class AccessTokenValidationMiddleware : IMiddleware
{
    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var identitySecurityTokenService = context.RequestServices.GetRequiredService<IIdentitySecurityTokenService>();

        var accessTokenIdValue = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.AccessTokenId)?.Value;
        if (accessTokenIdValue != null)
        {
            var accessTokenId = Guid.Parse(accessTokenIdValue);
            _ = await identitySecurityTokenService.GetAccessTokenByIdAsync(accessTokenId, context.RequestAborted) ??
                throw new AuthenticationException("Access token not found");
        }

        await next(context);
    }
}
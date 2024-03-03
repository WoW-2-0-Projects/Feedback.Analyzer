using System;
using System.Linq;
using System.Security.Claims;
using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.RequestContexts.Brokers;

/// <summary>
/// Provides client context information for the current request.
/// </summary>
public class RequestClientContextProvider(
    IHttpContextAccessor httpContextAccessor, 
    IOptions<RequestClientContextSettings> userContextProvider)
    : IRequestClientContextProvider
{
    private readonly RequestClientContextSettings _requestClientContextSettings = userContextProvider.Value;

    public string? GetAccessToken()
    {
        return httpContextAccessor.HttpContext?.Request.Headers.Authorization;
    }

    public Guid GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;

        var userIdClaim = httpContext!.User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.ClientId)?.Value;

        return userIdClaim is not null ? Guid.Parse(userIdClaim) : throw new ArgumentNullException();
    }
}
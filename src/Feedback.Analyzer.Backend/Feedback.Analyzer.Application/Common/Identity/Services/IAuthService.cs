using Feedback.Analyzer.Application.Common.Identity.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Identity.Services;

/// <summary>
/// Service interface for authorization operations
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Asynchronously registers a new user with the specified registration details.
    /// </summary>
    /// <param name="signUpDetails">The registration information for the new user</param>
    /// <param name="cancellationToken">A token that can be used to cancel the registration operation</param>
    /// <returns bool="that indicates whether the registration was successful.">A ValueTask</returns>
    ValueTask<bool> SignUpAsync(
        SignUpDetails signUpDetails, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Signs in user by email
    /// </summary>
    /// <param name="signInDetails">Sign in credentials</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Identity security tokens</returns>
    ValueTask<(AccessToken accessToken, RefreshToken refreshToken)> SignInByEmailAsync(
        SignInDetails signInDetails, 
        CancellationToken cancellationToken = default);
    

    /// <summary>
    /// Signs out the current user.
    /// </summary>
    /// <param name="accessTokenValue">User's access token</param>
    /// <param name="refreshTokenValue">User's refresh token</param>
    /// <param name="cancellationToken">Cancellation token</param>
    ValueTask SignOutAsync(string accessTokenValue, string refreshTokenValue, CancellationToken cancellationToken = default);

    
    /// <summary>
    /// Checks given refresh token and access token, then generates new access token for user
    /// </summary>
    /// <param name="refreshTokenValue">The unique token value of the refresh token to get</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed</param>
    /// <returns></returns>
    ValueTask<AccessToken> RefreshTokenAsync(
        string refreshTokenValue, 
        CancellationToken cancellationToken = default);
}
using System.Security.Authentication;
using AutoMapper;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.Common.Identity.Models;
using Feedback.Analyzer.Application.Common.Identity.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.Services;

/// <summary>
/// Encapsulates authentication-related functionality, such as user registration, login, and role management.
/// </summary>
public class AuthService(
    IMapper mapper,
    IClientService clientService,
    IAccountService accountService,
    IIdentitySecurityTokenService identitySecurityTokenService,
    IPasswordHasherService passwordHasherService,
    IPasswordGeneratorService passwordGeneratorService,
    IIdentitySecurityTokenGeneratorService identitySecurityTokenGeneratorService,
    IRequestClientContextProvider requestClientContextProvider
) : IAuthService
{
    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        //Check that the user is in the database at the entered email address
        var foundUserId = await clientService.Get(queryOptions: new QueryOptions(){AsNoTracking = true})
            .FirstOrDefaultAsync(client => client.EmailAddress == signUpDetails.EmailAddress, cancellationToken);
       
        if (foundUserId is not null)
            throw new InvalidOperationException("User with this email address already exists.");

        //Map the entered user object
        var client = mapper.Map<Client>(signUpDetails);

        //Generating complex password
        var password = signUpDetails.AutoGeneratePassword
            ? passwordGeneratorService.GeneratePassword()
            : passwordGeneratorService.GetValidatedPassword(signUpDetails.Password!, client);

        //Hash password
        client.PasswordHash = passwordHasherService.HashPassword(signUpDetails.Password);

        var createdUser = await accountService.CreateClientAsync(client, cancellationToken);
        
        
        
        return createdUser is not null;
    }

    public async ValueTask<(AccessToken accessToken, RefreshToken refreshToken)> SignInByEmailAsync(
        SignInDetails signInDetails,
        CancellationToken cancellationToken = default
    )
    {
        // Query user by email address
        var foundUser = 
            await clientService.Get(user => user.EmailAddress == signInDetails.EmailAddress,
                queryOptions: new QueryOptions { AsNoTracking = true })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (foundUser is null || !passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.PasswordHash))
            throw new AuthenticationException("Sign in details are invalid, contact support.");
       
        return await CreateTokens(foundUser, cancellationToken);
    }
    

    public async ValueTask SignOutAsync(string accessTokenValue, string refreshTokenValue, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accessTokenValue) || string.IsNullOrWhiteSpace(refreshTokenValue))
            throw new ArgumentException("Invalid identity security token value", nameof(accessTokenValue));

        await identitySecurityTokenService.RemoveRefreshTokenAsync(refreshTokenValue, cancellationToken);

        var accessToken = identitySecurityTokenGeneratorService.GetAccessToken(accessTokenValue);
        if (accessToken.HasValue) 
            await identitySecurityTokenService.RemoveAccessTokenAsync(accessToken.Value.AccessToken.Id, cancellationToken);
    }
    
    public async ValueTask<AccessToken> RefreshTokenAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
    {
        var accessTokenValue = requestClientContextProvider.GetAccessToken();

        if (string.IsNullOrWhiteSpace(refreshTokenValue))
            throw new ArgumentException("Invalid identity security token value", nameof(refreshTokenValue));

        if (string.IsNullOrWhiteSpace(accessTokenValue))
            throw new InvalidOperationException("Invalid identity security token value");

        // Check refresh token and access token
        var refreshToken = await identitySecurityTokenService.GetRefreshTokenByValueAsync(refreshTokenValue, cancellationToken);
        if (refreshToken is null)
            throw new AuthenticationException("Please login again.");

        var accessToken = identitySecurityTokenGeneratorService.GetAccessToken(accessTokenValue);
        if (!accessToken.HasValue)
        {
            // Remove refresh token if access token is not valid
            await identitySecurityTokenService.RemoveRefreshTokenAsync(refreshTokenValue, cancellationToken);
            throw new InvalidOperationException("Invalid identity security token value");
        }

        var foundAccessToken = await identitySecurityTokenService.GetAccessTokenByIdAsync(accessToken.Value.AccessToken.Id, cancellationToken);

        // Remove refresh token and access token if user id is not same
        if (refreshToken.UserId != accessToken.Value.AccessToken.ClientId)
        {
            await identitySecurityTokenService.RemoveRefreshTokenAsync(refreshTokenValue, cancellationToken);
            if (foundAccessToken is not null)
                await identitySecurityTokenService.RevokeAccessTokenAsync(accessToken.Value.AccessToken.Id, cancellationToken);

            throw new AuthenticationException("Please login again.");
        }

        var foundUser = await clientService.Get(
                user => user.Id == accessToken.Value.AccessToken.ClientId,
                new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new InvalidOperationException();

        // If access token exists, not revoked and still valid return it, otherwise remove
        if (foundAccessToken is not null && !foundAccessToken.IsRevoked)
        {
            if (!foundAccessToken.IsRevoked)
                return foundAccessToken;
            await identitySecurityTokenService.RemoveAccessTokenAsync(accessToken.Value.AccessToken.Id, cancellationToken);
        }

        // Generate access token
        var newAccessToken = identitySecurityTokenGeneratorService.GenerateAccessToken(foundUser);

        return await identitySecurityTokenService.CreateAccessTokenAsync(newAccessToken, cancellationToken: cancellationToken);
    }

    private async Task<(AccessToken AccessToken, RefreshToken RefreshToken)> CreateTokens(Client user, CancellationToken cancellationToken = default)
    {
        var accessToken = identitySecurityTokenGeneratorService.GenerateAccessToken(user);

        var refreshToken = identitySecurityTokenGeneratorService.GenerateRefreshToken(user);

        return (await identitySecurityTokenService.CreateAccessTokenAsync(accessToken, cancellationToken: cancellationToken),
            await identitySecurityTokenService.CreateRefreshTokenAsync(refreshToken, cancellationToken: cancellationToken));
    }
}
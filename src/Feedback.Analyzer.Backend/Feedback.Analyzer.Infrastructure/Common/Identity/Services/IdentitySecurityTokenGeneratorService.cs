﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Feedback.Analyzer.Application.Common.Identity.Services;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Extension;
using Feedback.Analyzer.Infrastructure.Common.Settings;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.Services;

public class IdentitySecurityTokenGeneratorService(IOptions<JwtSettings> jwtSettings) : IIdentitySecurityTokenGeneratorService
{
    
    /// <summary>
    /// Initializes a new instance of the AccessTokenGeneratorService class.
    /// </summary>
    /// <param name="jwtSettings">Options for configuring JWT settings injected via dependency injection.</param>
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    
    public AccessToken GenerateAccessToken(Client client)
    {
        var accessToken = new AccessToken()
        {
            Id = Guid.NewGuid()
        };

        var jwtToken = GetToken(client, accessToken);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        accessToken.Token = token;

        return accessToken;
    }

    public RefreshToken GenerateRefreshToken(Client client, bool extendedExpiryTime = false)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new RefreshToken()
        {
            Id = Guid.NewGuid(),
            Token = Convert.ToBase64String(randomNumber),
            UserId = client.Id,
            ExpiryTime = DateTime.UtcNow.AddMinutes(
                extendedExpiryTime
                    ? _jwtSettings.RefreshTokenExtendedExpirationTimeInMinutes
                    : _jwtSettings.RefreshTokenExpirationTimeInMinutes
            )
        };
    }

    public (AccessToken AccessToken, bool IsExpired)? GetAccessToken(string tokenValue)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var getAccessToken = () =>
        {
            var tokenWithPrefix = tokenValue.Replace("Bearer ", string.Empty);

            var tokenValidationParameters = _jwtSettings.MapToTokenValidationParameters();
            tokenValidationParameters.ValidateLifetime = false;

            var principal = tokenHandler.ValidateToken(tokenWithPrefix, tokenValidationParameters, out var validatedToken);
            
            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                ))
                throw new SecurityTokenException("Invalid token");
            
            var isExpired = jwtSecurityToken.ValidTo.ToUniversalTime() < DateTime.UtcNow;

            return (new AccessToken
            {
                Id = Guid.Parse(principal.FindFirst(JwtRegisteredClaimNames.Jti)!.Value),
                ClientId = Guid.Parse(principal.FindFirst(ClaimConstants.ClientId)!.Value),
                Token = tokenValue,
                ExpiryTime = jwtSecurityToken.ValidTo.ToUniversalTime()
            }, isExpired);
        };

        return getAccessToken.GetValue().Data;
    }

    public Guid GetAccessTokenId(string accessToken)
    {
        // Extract the token value from the authorization header.
        var tokenValue = accessToken.Split(' ')[1];

        // Create a JwtSecurityTokenHandler to read and parse the JWT token.
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(tokenValue);

        // Retrieve the unique identifier (ID) claim from the token.
        var tokenId = token.Claims.FirstOrDefault(c => c.Type == ClaimConstants.AccessTokenId)?.Value;

        // Validate and parse the retrieved ID, throwing an exception if it is invalid or missing.
        if (string.IsNullOrEmpty(tokenId))
            throw new ArgumentException("Invalid AccessToken");

        return Guid.Parse(tokenId);
    }

    public JwtSecurityToken GetToken(Client client, AccessToken accessToken)
    {
        var claims = GetClaims(client, accessToken);
        accessToken.ClientId = client.Id;
        accessToken.ExpiryTime = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: accessToken.ExpiryTime.UtcDateTime,
            signingCredentials: credentials
            );
    }

    public List<Claim> GetClaims(Client client, AccessToken accessToken)
    {
        return new List<Claim>()
        {
            new(ClaimTypes.Email, client.EmailAddress),
            
            new(ClaimConstants.ClientId, client.Id.ToString()),
            
            new(JwtRegisteredClaimNames.Jti, accessToken.Id.ToString())
        };
    }
}
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.Caching.Models;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository for managing AccessToken entities using a cache-based storage mechanism.
/// Initializes a new instance of the AccessTokenRepository class.
/// </summary>
/// <param name="cacheBroker">The cache broker responsible for handling cache operations.</param>
public class AccessTokenRepository(ICacheBroker cacheBroker) : IAccessTokenRepository
{
    public async ValueTask<AccessToken> CreateAsync(AccessToken accessToken, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        // Set cache entry with expiration based on AccessToken's ExpiryTime.
        var cacheEntryOptions = new CacheEntryOptions(accessToken.ExpiryTime - DateTimeOffset.UtcNow, null);
        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);

        return accessToken;
    }

    public ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return cacheBroker.GetAsync<AccessToken>(accessTokenId.ToString(), cancellationToken);
    }

    public async ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default)
    {
        // Update cache entry with expiration based on AccessToken's ExpiryTime.
        var cacheEntryOptions = new CacheEntryOptions(accessToken.ExpiryTime - DateTimeOffset.UtcNow, null);
        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);

        return accessToken;
    }

    public async ValueTask<AccessToken?> DeleteByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        var foundAccessToken = await cacheBroker.GetAsync<AccessToken>(accessTokenId.ToString(), cancellationToken);
        await cacheBroker.DeleteAsync(accessTokenId.ToString(), cancellationToken);

        return foundAccessToken;
    }
} 
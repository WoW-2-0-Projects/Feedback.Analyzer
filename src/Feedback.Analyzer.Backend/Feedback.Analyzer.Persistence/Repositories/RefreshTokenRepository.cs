﻿using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.Caching.Models;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Provides refresh token repository functionality using a cache storage
/// </summary>
/// <param name="cacheBroker"></param>
public class RefreshTokenRepository(ICacheBroker cacheBroker) : IRefreshTokenRepository
{
    public async ValueTask<RefreshToken> CreateAsync(
        RefreshToken refreshToken,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default
    )
    {
        var cacheEntryOptions = new CacheEntryOptions(null, refreshToken.ExpiryTime - DateTimeOffset.UtcNow);
        await cacheBroker.SetAsync($"{nameof(RefreshToken)}-{refreshToken.Token}", refreshToken, cacheEntryOptions, cancellationToken);

        return refreshToken;
    }

    public ValueTask<RefreshToken?> GetByValueAsync(string refreshTokenValue, CancellationToken cancellationToken = default) =>
        cacheBroker.GetAsync<RefreshToken>($"{nameof(RefreshToken)}-{refreshTokenValue}", cancellationToken: cancellationToken);

    public ValueTask RemoveAsync(string refreshTokenValue, CancellationToken cancellationToken = default) =>
        cacheBroker.DeleteAsync($"{nameof(RefreshToken)}-{refreshTokenValue}", cancellationToken: cancellationToken);
}
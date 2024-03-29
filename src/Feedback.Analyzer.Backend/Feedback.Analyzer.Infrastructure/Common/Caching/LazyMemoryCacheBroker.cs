﻿using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.Caching.Models;
using Force.DeepCloner;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.Common.Caching;

/// <summary>
/// Provides caching functionalities using an in-memory cache.
/// </summary>
public class LazyMemoryCacheBroker(IOptions<CacheSettings> cacheSettings, IAppCache memoryCache) : ICacheBroker
{
    private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSettings.Value.AbsoluteExpirationInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Value.SlidingExpirationInSeconds)
    };

    public ValueTask<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(memoryCache.Get<T?>(key));
    }

    public ValueTask<bool> TryGetAsync<T>(string key, out T? value, CancellationToken cancellationToken = default)
    {
        value = memoryCache.Get<T?>(key);

        return value is not null ? ValueTask.FromResult(true) : ValueTask.FromResult(false);
    }

    public async ValueTask<T?> GetOrSetAsync<T>(
        string key,
        T value,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var cachedValue = memoryCache.Get<T?>(key);

        if (cachedValue is not null)
            return cachedValue;

        await SetAsync(key, value, entryOptions, cancellationToken);

        return value;
    }

    public ValueTask<T?> GetOrSetAsync<T>(
        string key,
        Func<T> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return new ValueTask<T?>(memoryCache.GetOrAdd(key, valueProvider, GetCacheEntryOptions(entryOptions)));
    }

    public async ValueTask<T?> GetOrSetAsync<T>(
        string key,
        Func<ValueTask<T>> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return await memoryCache.GetOrAddAsync(key, async () => await valueProvider(), GetCacheEntryOptions(entryOptions));
    }

    public ValueTask SetAsync<T>(string key, T value, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default)
    {
        memoryCache.Add(key, value, GetCacheEntryOptions(entryOptions));
        return ValueTask.CompletedTask;
    }

    public ValueTask SetAsync<T>(
        string key,
        Func<T> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        memoryCache.Add(key, valueProvider(), GetCacheEntryOptions(entryOptions));
        return ValueTask.CompletedTask;
    }

    public async ValueTask SetAsync<T>(
        string key,
        Func<ValueTask<T>> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        memoryCache.Add(key, await valueProvider(), GetCacheEntryOptions(entryOptions));
    }

    public ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        memoryCache.Remove(key);
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Gets the cache entry options based on given entry options or default options.
    /// </summary>
    /// <param name="entryOptions">Given cache entry options.</param>
    /// <returns>The memory cache entry options.</returns>
    private MemoryCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions)
    {
        if (!entryOptions.HasValue ||
            (!entryOptions.Value.AbsoluteExpirationRelativeToNow.HasValue && !entryOptions.Value.SlidingExpiration.HasValue))
            return _memoryCacheEntryOptions;

        var currentEntryOptions = _memoryCacheEntryOptions.DeepClone();
        currentEntryOptions.AbsoluteExpirationRelativeToNow = entryOptions.Value.AbsoluteExpirationRelativeToNow;
        currentEntryOptions.SlidingExpiration = entryOptions.Value.SlidingExpiration;

        return currentEntryOptions;
    }
}
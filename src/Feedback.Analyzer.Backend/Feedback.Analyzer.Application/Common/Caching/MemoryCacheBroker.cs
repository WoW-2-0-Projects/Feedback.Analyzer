using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.Caching.Models;
using Force.DeepCloner;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Application.Common.Caching;

/// <summary>
/// Provides caching functionalities using an in-memory cache.
/// </summary>
public class MemoryCacheBroker(
    IOptions<CacheSettings> cacheSettings, 
    IMemoryCache memoryCache) : ICacheBroker
{

    private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new MemoryCacheEntryOptions()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSettings.Value.AbsoluteExpirationInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Value.SlidingExpirationInSeconds)
    };
    
    public ValueTask<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = memoryCache.Get<T?>(key);
        return value is not null ? new ValueTask<T?>(value) : default;
    }

    public ValueTask<bool> TryGetAsync<T>(string key, out T? value, CancellationToken cancellationToken = default)
    {
        var foundEntry = memoryCache.Get<T?>(key);

        if (foundEntry is not null)
        {
            value = foundEntry;
            return ValueTask.FromResult(true);
        }

        value = default;
        return ValueTask.FromResult(false);
    }

    public async ValueTask<T?> GetOrSetAsync<T>(
        string key, Func<Task<T>> valueFactory,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
        )
    {
        var cachedValue = memoryCache.Get<T?>(key);

        if (cachedValue is not null) return await new ValueTask<T?>(cachedValue);

        var value = await valueFactory();
        await SetAsync(key, value, entryOptions, cancellationToken);

        return value;
    }

    public ValueTask SetAsync<T>(string key, T value, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default)
    {
        memoryCache.Set(key, value, GetCacheEntryOptions(entryOptions));
        return ValueTask.CompletedTask;
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
    /// <returns>The distributed cache entry options.</returns>
    private MemoryCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions)
    {
        if (entryOptions == default || (!entryOptions.AbsoluteExpirationRelativeToNow.HasValue && !entryOptions.SlidingExpiration.HasValue))
            return _memoryCacheEntryOptions;
        
        var currentEntryOptions = _memoryCacheEntryOptions.DeepClone();

        currentEntryOptions.AbsoluteExpirationRelativeToNow = entryOptions.AbsoluteExpirationRelativeToNow;

        currentEntryOptions.SlidingExpiration = entryOptions.SlidingExpiration;

        return currentEntryOptions;
    }
}
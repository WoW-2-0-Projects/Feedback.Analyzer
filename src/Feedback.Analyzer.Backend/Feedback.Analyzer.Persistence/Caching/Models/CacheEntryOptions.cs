namespace Feedback.Analyzer.Persistence.Caching.Models;

/// <summary>
/// Represents the options for configuring cache entry behavior.
/// </summary>
public readonly struct CacheEntryOptions(TimeSpan? absoluteExpirationRelativeToNow, TimeSpan? slidingExpiration)
{
    /// <summary>
    /// Gets or sets the absolute expiration time relative to the current moment.
    /// </summary>
    public TimeSpan? AbsoluteExpirationRelativeToNow { get; } = absoluteExpirationRelativeToNow;

    /// <summary>
    /// Gets or sets the sliding expiration time for cached items.
    /// </summary>
    public TimeSpan? SlidingExpiration { get; } = slidingExpiration;
}
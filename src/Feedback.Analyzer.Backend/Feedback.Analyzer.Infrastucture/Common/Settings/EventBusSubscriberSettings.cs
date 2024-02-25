namespace Feedback.Analyzer.Infrastucture.Common.Settings;

/// <summary>
/// Represents settings for an event bus subscriber.
/// </summary>
public abstract class EventBusSubscriberSettings
{
    /// <summary>
    /// Gets or sets the number of messages that the subscriber should prefetch at once.
    /// </summary>
    public ushort PrefetchCount { get; set; }
}

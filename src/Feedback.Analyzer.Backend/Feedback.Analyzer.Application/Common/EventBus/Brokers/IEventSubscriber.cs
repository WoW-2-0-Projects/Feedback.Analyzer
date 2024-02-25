namespace Feedback.Analyzer.Application.Common.EventBus.Brokers;

/// <summary>
/// Represents an interface for an event subscriber, which handles starting and stopping event subscription.
/// </summary>
public interface IEventSubscriber
{
    /// <summary>
    /// Starts the event subscription asynchronously.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask StartAsync(CancellationToken token);

    /// <summary>
    /// Stops the event subscription asynchronously.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask StopAsync(CancellationToken token);
}
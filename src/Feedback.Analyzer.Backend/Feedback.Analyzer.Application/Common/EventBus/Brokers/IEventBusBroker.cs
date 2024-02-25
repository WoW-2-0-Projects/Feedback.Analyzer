using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.EventBus.Brokers;

/// <summary>
/// Represents a generic interface for an event bus broker, which facilitates the publishing and subscribing to events.
/// </summary>
public interface IEventBusBroker
{
    /// <summary>
    /// Publishes a local event asynchronously.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to publish.</typeparam>
    /// <param name="event">The event to publish.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask PublishLocalAsync<TEvent>(TEvent @event) where TEvent : IEvent;

    /// <summary>
    /// Publishes an event asynchronously to a specified exchange and routing key.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to publish.</typeparam>
    /// <param name="event">The event to publish.</param>
    /// <param name="exchange">The exchange to which the event will be published.</param>
    /// <param name="routingKey">The routing key for the event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask PublishAsync<TEvent>(TEvent @event, string exchange, string routingKey, CancellationToken cancellationToken) where TEvent : Event;
}
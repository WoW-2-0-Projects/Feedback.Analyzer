using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.EventBus.Brokers;

/// <summary>
/// Interface for an event bus broker, responsible for publishing events locally or asynchronously.
/// </summary>
public interface IEventBusBroker
{
    /// <summary>
    /// Publishes an event locally.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to be published.</typeparam>
    /// <param name="event">The event to be published.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask PublishLocalAsync<TEvent>(TEvent @event) where TEvent : Event;

    /// <summary>
    /// Publishes an event asynchronously.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to be published.</typeparam>
    /// <param name="event">The event to be published.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
}

using MassTransit;
using MediatR;

namespace Feedback.Analyzer.Domain.Common.Events;

/// <summary>
/// Represents base for event handlers
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public abstract class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : class, INotification
{
    public async Task Handle(TEvent notification, CancellationToken cancellationToken) => await HandleAsync(notification, cancellationToken);

    public async Task Consume(ConsumeContext<TEvent> context) => await HandleAsync(context.Message, context.CancellationToken);

    /// <summary>
    /// Internal handle method that can be called by any event bus
    /// </summary>
    /// <param name="event">Event to handle</param>
    /// <param name="cancellationToken">Cancellation token</param>
    protected abstract ValueTask HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
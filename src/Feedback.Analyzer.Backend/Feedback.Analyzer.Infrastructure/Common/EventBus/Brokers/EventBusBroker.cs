using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Domain.Common.Events;
using MediatR;

namespace Feedback.Analyzer.Infrastructure.Common.EventBus.Brokers;

/// <summary>
/// Implementation of <see cref="IEventBusBroker"/> that utilizes a mediator for publishing events.
/// </summary>
/// <param name="mediator">The mediator instance to be used for event publication.</param>
public class EventBusBroker(IPublisher mediator) : IEventBusBroker
{
    public ValueTask PublishLocalAsync<TEvent>(TEvent @event) where TEvent : Contract
    {
        return new ValueTask(mediator.Publish(@event));
    }

    public ValueTask PublishAsync<TEvent>(TEvent @event) where TEvent : Contract
    {
        return new ValueTask(mediator.Publish(@event));
    }
}
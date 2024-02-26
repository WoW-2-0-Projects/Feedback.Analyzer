using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Domain.Common.Events;
using MediatR;

namespace Feedback.Analyzer.Infrastucture.Common.EventBus.Brokers;

public class EventBusBroker(IPublisher mediator) : IEventBusBroker
{
    public ValueTask PublishLocalAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        return new ValueTask(mediator.Publish(@event));
    }

    public ValueTask PublishAsync<TEvent>(TEvent @event) where TEvent : Event
    {
        return new ValueTask(mediator.Publish(@event));
    }
}
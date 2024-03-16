using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Domain.Common.Events;
using MassTransit;
using MediatR;

namespace Feedback.Analyzer.Infrastructure.Common.EventBus.Brokers;

public class MassTransitEventBusBroker(IBus bus, IPublisher mediator) : IEventBusBroker
{
    public ValueTask PublishLocalAsync<TEvent>(TEvent @event) where TEvent : EventBase
    {
        return new ValueTask(mediator.Publish(@event));
    }

    public async ValueTask PublishAsync<TEvent>(TEvent @event) where TEvent : EventBase
    {
        await bus.Publish(@event);
    }
}
using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Domain.Common.Events;
using RabbitMQ.Client;

namespace Feedback.Analyzer.Infrastucture.Common.EventBus.Services;

public abstract class EventSubscriber<TEvent> : IEventSubscriber where TEvent : Event
{
    protected IChannel Channel = default!;
    public async ValueTask StartAsync(CancellationToken token)
    {
        //to continue other codes which is related to rabbitmq
    }

    public ValueTask StopAsync(CancellationToken token)
    {
        Channel.Dispose();
        return ValueTask.CompletedTask;
    }

    protected abstract ValueTask<(bool Result, bool Redeliver)> ProcessAsync(TEvent @event,
        CancellationToken cancellationToken);
}
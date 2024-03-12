using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.Common.Identity.Events;
using Feedback.Analyzer.Domain.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.EventHandlers;

public class UserCreatedEventHandler(IEventBusBroker eventBusBroker, IServiceScopeFactory serviceScopeFactory) : IEventHandler<ClientCreatedEvent>
{
    public Task Handle(ClientCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

}
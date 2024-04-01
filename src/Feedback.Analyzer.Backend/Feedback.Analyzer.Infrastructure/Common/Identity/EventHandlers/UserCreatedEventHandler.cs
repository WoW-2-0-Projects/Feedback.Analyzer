using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.Common.Identity.Events;
using Feedback.Analyzer.Domain.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.EventHandlers;

/// <summary>
/// Represents event handler for user created event
/// </summary>
/// <param name="serviceScopeFactory"></param>
public class UserCreatedEventHandler(IServiceScopeFactory serviceScopeFactory) : EventHandlerBase<ClientCreatedEvent>
{
    protected override ValueTask HandleAsync(ClientCreatedEvent notification, CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }

}
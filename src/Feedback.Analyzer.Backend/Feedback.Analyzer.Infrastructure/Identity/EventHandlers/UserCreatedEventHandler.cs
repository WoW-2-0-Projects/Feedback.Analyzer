using Feedback.Analyzer.Application.Identity.Events;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.Identity.EventHandlers;

/// <summary>
///  Represents event handler for user created event
/// </summary>
public class UserCreatedEventHandler : EventHandlerBase<UserCreatedEvent>
{
    protected override ValueTask HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }
}

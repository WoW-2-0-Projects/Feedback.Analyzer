using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

public class ExecuteWorkflowSinglePromptEventHandler : IEventHandler<ExecuteWorkflowSinglePromptEvent>
{
    public Task Handle(ExecuteWorkflowSinglePromptEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
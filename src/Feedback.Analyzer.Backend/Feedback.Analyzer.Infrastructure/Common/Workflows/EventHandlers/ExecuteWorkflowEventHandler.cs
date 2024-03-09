using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

public class ExecuteWorkflowEventHandler(IFeedbackBatchAnalysisWorkflowOrchestrationService feedbackBatchAnalysisWorkflowOrchestrationService)
    : IEventHandler<ExecuteWorkflowEvent>
{
    public async Task Handle(ExecuteWorkflowEvent notification, CancellationToken cancellationToken)
    {
        await feedbackBatchAnalysisWorkflowOrchestrationService.ExecuteWorkflowAsync(notification.WorkflowId, cancellationToken);
    }
}
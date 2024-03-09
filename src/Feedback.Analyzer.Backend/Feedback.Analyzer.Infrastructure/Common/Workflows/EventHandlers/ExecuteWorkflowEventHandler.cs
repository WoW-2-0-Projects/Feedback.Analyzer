using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

/// <summary>
/// Represents workflow execution event handler
/// </summary>
/// <param name="feedbackBatchAnalysisWorkflowOrchestrationService"></param>
public class ExecuteWorkflowEventHandler(IFeedbackBatchAnalysisWorkflowOrchestrationService feedbackBatchAnalysisWorkflowOrchestrationService)
    : IEventHandler<ExecuteWorkflowEvent>
{
    public async Task Handle(ExecuteWorkflowEvent notification, CancellationToken cancellationToken)
    {
        await feedbackBatchAnalysisWorkflowOrchestrationService.ExecuteWorkflowAsync(notification.WorkflowId, cancellationToken);
    }
}
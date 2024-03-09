using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
/// Represents workflow execution event handler
/// </summary>
/// <param name="feedbackBatchAnalysisWorkflowOrchestrationService"></param>
public class AnalyzeWorkflowFeedbacksEventHandler(IFeedbackBatchAnalysisWorkflowOrchestrationService feedbackBatchAnalysisWorkflowOrchestrationService)
    : IEventHandler<AnalyzeWorkflowFeedbacksEvent>
{
    public async Task Handle(AnalyzeWorkflowFeedbacksEvent notification, CancellationToken cancellationToken)
    {
        await feedbackBatchAnalysisWorkflowOrchestrationService.ExecuteWorkflowAsync(notification.WorkflowId, cancellationToken);
    }
}
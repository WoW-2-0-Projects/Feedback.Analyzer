using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.EventHandler;

/// <summary>
/// Represents feedback analysis result update event handler
/// </summary>
public class FeedbackAnalysisWorkflowResultUpdateEventHandler(IFeedbackAnalysisWorkflowResultService feedbackAnalysisWorkflowResultService)
    : EventHandlerBase<FeedbackAnalysisWorkflowResultUpdateEvent>
{
    protected override async ValueTask HandleAsync(FeedbackAnalysisWorkflowResultUpdateEvent @event, CancellationToken cancellationToken)
    {
        // Load workflow result with all feedback analysis results 
        var workflowResult = await feedbackAnalysisWorkflowResultService
            .Get(result => result.Id == @event.WorkflowResultId)
            .Include(result => result.FeedbackAnalysisResults)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new InvalidOperationException("Workflow result not found");

        // Calculate processed and failed feedbacks count
        workflowResult.ProcessedFeedbacksCount =
            (uint)workflowResult.FeedbackAnalysisResults.Count(result => result.AnalysisResult.Status == WorkflowStatus.Completed);

        workflowResult.FailedFeedbacksCount =
            (uint)workflowResult.FeedbackAnalysisResults.Count(result => result.AnalysisResult.Status == WorkflowStatus.Failed);

        // TODO : Calculate feedback analysis results stats

        // TODO : Calculate feedback analysis result points

        // TODO : Update workflow status

        await feedbackAnalysisWorkflowResultService.UpdateAsync(workflowResult, cancellationToken: cancellationToken);
    }
}
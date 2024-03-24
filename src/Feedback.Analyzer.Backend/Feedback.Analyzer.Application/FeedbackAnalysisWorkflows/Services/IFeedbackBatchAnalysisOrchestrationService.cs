namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Defines feedback batch analysis orchestration functionality
/// </summary>
public interface IFeedbackBatchAnalysisOrchestrationService
{
    /// <summary>
    /// Triggers given workflow
    /// </summary>
    /// <param name="workflowId">Id of workflow to execute</param>
    /// <param name="cancellationToken">Cancellation token</param>
    ValueTask RunWorkflowAsync(Guid workflowId, CancellationToken cancellationToken = default);
}
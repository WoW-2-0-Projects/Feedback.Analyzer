namespace Feedback.Analyzer.Application.CustomerFeedbacks.Services;

/// <summary>
/// Defines feedback analysis workflow orchestration functionality
/// </summary>
public interface IFeedbackBatchAnalysisWorkflowOrchestrationService
{
    /// <summary>
    /// Executes workflow for the given feedback
    /// </summary>
    /// <param name="workflowId">Id of workflow to execute</param>
    /// <param name="cancellationToken">Cancellation token</param>
    ValueTask ExecuteWorkflowAsync(Guid workflowId, CancellationToken cancellationToken = default);
}
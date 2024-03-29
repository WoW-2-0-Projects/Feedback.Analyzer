using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Defines feedback analysis processing service functionality
/// </summary>
public interface IFeedbackAnalysisWorkflowProcessingService
{
    /// <summary>
    /// Creates feedback analysis workflow with related analysis workflow
    /// </summary>
    /// <param name="feedbackAnalysisWorkflow">Workflow to create</param>
    /// <param name="analysisWorkflow">Related analysis workflow to create</param>
    /// <param name="baseWorkflowId">Base workflow Id to clone execution options from</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created feedback analysis workflow with new created, related analysis workflow</returns>
    ValueTask<(FeedbackAnalysisWorkflow FeedbackAnalysisWorkflow, AnalysisWorkflow AnalysisWorkflow)>
        CreateAsync(
            FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
            AnalysisWorkflow analysisWorkflow,
            Guid baseWorkflowId,
            CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates feedback analysis workflow with related analysis workflow
    /// </summary>
    /// <param name="feedbackAnalysisWorkflow">Workflow to update</param>
    /// <param name="analysisWorkflow">Related analysis workflow to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated feedback analysis workflow with updated, related analysis workflow</returns>
    ValueTask<(FeedbackAnalysisWorkflow FeedbackAnalysisWorkflow, AnalysisWorkflow AnalysisWorkflow)>
        UpdateAsync(
            FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
            AnalysisWorkflow analysisWorkflow,
            CancellationToken cancellationToken = default);
}
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Defines feedback analysis workflow orchestration functionality
/// </summary>
public interface IFeedbackAnalysisOrchestrationService
{
    /// <summary>
    /// Executes workflow for the given feedback
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask ExecuteWorkflowAsync(SingleFeedbackAnalysisWorkflowContext context, CancellationToken cancellationToken = default);
}
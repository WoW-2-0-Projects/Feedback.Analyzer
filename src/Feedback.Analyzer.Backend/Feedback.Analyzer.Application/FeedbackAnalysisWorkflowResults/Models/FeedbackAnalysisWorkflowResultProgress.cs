using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Models;

/// <summary>
/// Represents the progress of a feedback analysis workflow result.
/// </summary>
public struct FeedbackAnalysisWorkflowResultProgress(FeedbackAnalysisWorkflowResult workflowResult)
{
    /// <summary>
    /// Gets or the identifier of the workflow result.
    /// </summary>
    public Guid WorkflowResultId { get; init; } = workflowResult.Id;

    /// <summary>
    /// Gets the total number of feedbacks.
    /// </summary>
    public uint FeedbacksCount { get; init; } = workflowResult.FeedbacksCount;

    /// <summary>
    /// Gets or sets the number of feedbacks that have been analysed.
    /// </summary>
    public uint AnalyzedFeedbacksCount { get; set; } = default;
}
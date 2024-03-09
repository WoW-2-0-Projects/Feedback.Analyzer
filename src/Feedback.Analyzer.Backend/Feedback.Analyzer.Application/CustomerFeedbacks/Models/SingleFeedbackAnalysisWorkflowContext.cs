using Feedback.Analyzer.Application.Common.Prompts.Models;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Models;

/// <summary>
/// Represents prompt execution context for feedback analysis.
/// </summary>
public record SingleFeedbackAnalysisWorkflowContext : WorkflowContext
{
    /// <summary>
    /// Gets customer feedback Id
    /// </summary>
    public Guid FeedbackId { get; init; }

    // /// <summary>
    // /// Gets feedbacks Id
    // /// </summary>
    // public IImmutableList<Guid> FeedbacksId { get; init; } = new ImmutableArray<Guid>();
    //
    // /// <summary>
    // /// Gets product Id
    // /// </summary>
    // public Guid ProductId { get; init; }
    //
    // /// <summary>
    // /// Gets feedback analysis result
    // /// </summary>
    // public FeedbackAnalysisResult AnalysisResult { get; set; } = default!;
}
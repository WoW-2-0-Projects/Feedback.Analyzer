using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;

/// <summary>
/// Represents workflow execution event
/// </summary>
public record AnalyzeWorkflowFeedbacksEvent : Contract
{
    /// <summary>
    /// Gets workflow Id
    /// </summary>
    public Guid WorkflowId { get; init; }
}
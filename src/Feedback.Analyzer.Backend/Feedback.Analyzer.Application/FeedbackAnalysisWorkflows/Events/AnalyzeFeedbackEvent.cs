using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;

/// <summary>
/// Represents feedback analysis event
/// </summary>
public record AnalyzeFeedbackEvent : EventBase
{
    /// <summary>
    /// Gets feedback analysis workflow context
    /// </summary>
    public SingleFeedbackAnalysisWorkflowContext Context { get; init; } = default!;
}
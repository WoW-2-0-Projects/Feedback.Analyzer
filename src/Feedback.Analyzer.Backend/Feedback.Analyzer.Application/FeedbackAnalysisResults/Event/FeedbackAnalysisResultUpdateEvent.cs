using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Event;

/// <summary>
/// Represents feedback analysis update event
/// </summary>
/// <param name="FeedbackAnalysisWorkflowResult"></param>
public record FeedbackAnalysisResultUpdateEvent : EventBase
{
    /// <summary>
    /// Given workflow result
    /// </summary>
    public FeedbackAnalysisWorkflowResult WorkflowResult { get; set; }
}

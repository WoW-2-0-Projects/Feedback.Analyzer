using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Events;

/// <summary>
/// Represents feedback analysis result creation event
/// </summary>
public record FeedbackAnalysisResultCreatedEvent : EventBase
{
    /// <summary>
    /// Created feedback analysis result
    /// </summary>
    public FeedbackAnalysisResult FeedbackAnalysisResult { get; init; } = default!;
}

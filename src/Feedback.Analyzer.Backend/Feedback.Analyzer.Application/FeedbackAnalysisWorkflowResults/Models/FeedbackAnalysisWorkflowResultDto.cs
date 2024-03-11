using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Models;

/// <summary>
/// Represents data transfer object for feedback analysis workflow result
/// </summary>
public record FeedbackAnalysisWorkflowResultDto
{
    /// <summary>
    /// Gets feedback workflow result Id 
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets workflow Id
    /// </summary>
    public Guid WorkflowId { get; init; }

    public DateTimeOffset StartedTime { get; set; }

    public DateTimeOffset FinishedTime { get; set; }

    /// <summary>
    /// Gets related feedbacks results
    /// </summary>
    public ICollection<FeedbackAnalysisResultDto> Results { get; init; } = default!;
}
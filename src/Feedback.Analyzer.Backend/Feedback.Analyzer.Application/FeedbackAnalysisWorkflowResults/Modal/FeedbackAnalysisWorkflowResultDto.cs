using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Modal;

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
    
    /// <summary>
    /// Gets or sets the start time of the feedback analysis workflow.
    /// </summary>
    public DateTimeOffset StartedTime { get; set; }

    /// <summary>
    /// Gets or sets the finish time of the feedback analysis workflow.
    /// </summary>
    public DateTimeOffset FinishedTime { get; set; }
}
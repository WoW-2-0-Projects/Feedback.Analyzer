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
    
    /// <summary>
    /// Gets or sets the start time of the feedback analysis workflow.
    /// </summary>
    public DateTimeOffset StartedTime { get; set; }

    /// <summary>
    /// Gets or sets the finish time of the feedback analysis workflow.
    /// </summary>
    public DateTimeOffset FinishedTime { get; set; }
    
    /// <summary>
    /// Gets or sets the total count of successfully analyzed feedbacks
    /// </summary>
    public ulong ProcessedAnalysisCount { get; set; }
    
    /// <summary>
    /// Gets or sets the total count of feedbacks that are failed to analyze
    /// </summary>
    public ulong FailedAnalysisCount { get; set; }
}
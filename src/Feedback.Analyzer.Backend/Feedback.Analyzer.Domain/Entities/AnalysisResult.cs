using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents a class that keeps the analysis result information for feedback analysis result entity
/// </summary>
public class AnalysisResult
{
    /// <summary>
    /// Gets or sets the status of the analysis.
    /// </summary>
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Running;

    /// <summary>
    /// Gets or sets the error message associated with the analysis result, if any.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the history associated with the analysis result.
    /// </summary>
    public Guid? HistoryId { get; set; }
    
    /// <summary>
    /// Gets or sets the analysis time in milliseconds.
    /// </summary>
    public TimeSpan ModelExecutionDuration { get; set; }
    
    /// <summary>
    /// Gets or sets the analysis time in milliseconds
    /// </summary>
    public TimeSpan AnalysisDuration { get; set; }

    /// <summary>
    /// Navigation History property.
    /// </summary>
    public PromptExecutionHistory History { get; set; }
}
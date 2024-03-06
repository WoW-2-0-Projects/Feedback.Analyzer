namespace Feedback.Analyzer.Application.PromptsHistory.Models;

/// <summary>
/// Represents a Data Transfer Object (DTO) for prompt execution history.
/// </summary>
public class PromptExecutionHistoryDto
{ 
    /// <summary>
    /// Gets or sets the unique identifier for the prompt execution history.
    /// </summary>
    public Guid Id { get; set; } 
    
    /// <summary>
    /// Gets or sets the unique identifier for the prompt.
    /// </summary>
    public Guid PromptId { get; set; }

    /// <summary>
    /// Gets or sets the result of the prompt execution.
    /// </summary>    
    public string? Result { get; set; }

    /// <summary>
    /// Gets or sets the exception message if an exception occurred during execution.
    /// </summary>
    public string? Exception { get; set; } 
    
    /// <summary>
    /// Gets or sets the date and time when the prompt was executed.
    /// </summary>
    public DateTime ExecutionTime { get; set; } 
    
    /// <summary>
    /// Gets or sets the duration of the prompt execution in seconds.
    /// </summary>
    public int ExecutionDurationInMilliseconds { get; set; }
}
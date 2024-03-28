namespace Feedback.Analyzer.Application.Common.Prompts.Models;

/// <summary>
/// Represents a data transfer object for prompt results.
/// </summary>
public record PromptResultDto
{
    /// <summary>
    /// Gets or sets the ID of the prompt associated with the result.
    /// </summary>
    public Guid PromptId { get; set; }
    
    /// <summary>
    /// Gets or sets the version of the prompt result.
    /// </summary>
    public int Version { get; set; }
    
    /// <summary>
    /// Gets or sets the revision of the prompt result.
    /// </summary>
    public int Revision { get; set; }
    
    /// <summary>
    /// Gets or sets the count of executions for the prompt.
    /// </summary>
    public int ExecutionsCount { get; set; }
    
    /// <summary>
    /// Gets or sets the average execution time for the prompt.
    /// </summary>
    public uint AverageExecutionTimeInMilliSeconds { get; set; }
    
    /// <summary>
    /// Gets or sets the average accuracy for the prompt.
    /// </summary>
    public int AverageAccuracy { get; set; }
}


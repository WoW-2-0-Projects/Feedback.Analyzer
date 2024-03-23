using System.ComponentModel.DataAnnotations.Schema;
using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents the execution history of a prompt.
/// </summary>
public class PromptExecutionHistory : Entity
{ 
    /// <summary>
    /// Gets or sets the unique identifier of the prompt.
    /// </summary>
    public Guid PromptId { get; set; }

    /// <summary>
    /// Gets or sets the result of the prompt execution.
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// Gets or sets the exception encountered during prompt execution, if any.
    /// </summary>
    public string? Exception { get; set; } 
    
    /// <summary>
    /// Gets or sets the time when the prompt was executed.
    /// </summary>
    public DateTimeOffset ExecutionTime { get; set; } 
    
    /// <summary>
    /// Gets or sets the duration of the prompt execution.
    /// </summary>
    public TimeSpan ExecutionDuration { get; set; }

    /// <summary>   
    /// Gets or sets the AnalysisPrompt object used for prompting analysis.
    /// </summary>
    public AnalysisPrompt Prompt { get; set; } = default!;

    /// <summary>
    /// Gets or sets prompt category Id
    /// </summary>
    [NotMapped]
    public Guid PromptCategoryId { get; set; }
}
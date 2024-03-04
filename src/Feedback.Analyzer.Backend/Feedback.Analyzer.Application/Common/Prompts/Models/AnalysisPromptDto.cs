using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

/// <summary>
/// Represents a data transfer object for analysis prompts.
/// </summary>
public class AnalysisPromptDto
{
    /// <summary>
    /// Gets or sets the ID of the analysis prompt.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the type of feedback analysis prompt.
    /// </summary>
    // public FeedbackAnalysisPromptType Type { get; set; }
    
    /// <summary>
    /// Gets or sets the text of the prompt.
    /// </summary>
    public string Prompt { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the version of the analysis prompt.
    /// </summary>
    public int Version { get; set; }
    
    /// <summary>
    /// Gets or sets the revision of the analysis prompt.
    /// </summary>
    public int Revision { get; set; }
    
    /// <summary>
    /// Gets or sets the category ID of the analysis prompt.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Gets or sets the creation time of the analysis prompt.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }
    
    /// <summary>
    /// Gets or sets the modified time of the analysis prompt.
    /// </summary>
    public DateTimeOffset ModifiedTime { get; set; }
}

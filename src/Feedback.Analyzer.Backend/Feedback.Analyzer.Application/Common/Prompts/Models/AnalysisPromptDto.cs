using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

/// <summary>
/// Represents a data transfer object for analysis prompts.
/// </summary>
public class AnalysisPromptDto
{
    public Guid Id { get; set; }
    
    public FeedbackAnalysisPromptCategory Category { get; set; }
    
    public string Prompt { get; set; } = default!;
    
    public int Version { get; set; }
    
    public int Revision { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset ModifiedTime { get; set; }
}

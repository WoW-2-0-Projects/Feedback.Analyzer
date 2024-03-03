using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public class AnalysisPromptDto
{
    public Guid Id { get; set; }
    
    public FeedbackAnalysisPromptType Type { get; set; }
    
    public string Prompt { get; set; } = default!;
    
    public int MajorVersion { get; set; }
    
    public int Revision { get; set; }
    
    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset ModifiedTime { get; set; }
}
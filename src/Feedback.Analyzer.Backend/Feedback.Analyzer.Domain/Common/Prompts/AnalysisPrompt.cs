using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Common.Prompts;

public class AnalysisPrompt : AuditableEntity
{
    public string Prompt { get; set; } = default!;
    
    public int MajorVersion { get; set; }
    
    public int MinorVersion { get; set; }
}
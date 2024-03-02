namespace Feedback.Prompt.Analyzer.Domain.Common.Prompts;

public abstract class AnalysisPrompt
{
    public string Prompt { get; set; } = default!;
    
    public int Version { get; set; }
}
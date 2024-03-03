using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Common.Prompts;

public class AnalysisPrompt : AuditableEntity, ICloneable<AnalysisPrompt>
{
    public FeedbackAnalysisPromptType Type { get; set; }
    
    public string Prompt { get; set; } = default!;
    
    public int MajorVersion { get; set; }
    
    public int MinorVersion { get; set; }
    
    public AnalysisPrompt Clone()
    {
        return new AnalysisPrompt
        {
            Prompt = Prompt,
            MajorVersion = MajorVersion,
            MinorVersion = MinorVersion
        };
    }
}
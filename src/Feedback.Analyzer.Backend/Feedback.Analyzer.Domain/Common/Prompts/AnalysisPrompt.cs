using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Domain.Common.Prompts;

public class AnalysisPrompt : AuditableEntity, ICloneable<AnalysisPrompt>
{
    public string Prompt { get; set; } = default!;

    public int Version { get; set; }

    public int Revision { get; set; }

    public Guid CategoryId { get; set; }

    public AnalysisPromptCategory Category { get; set; } = default!;

    public AnalysisPrompt Clone()
    {
        return new AnalysisPrompt
        {
            Prompt = Prompt,
            Version = Version,
            Revision = Revision,
        };
    }
}
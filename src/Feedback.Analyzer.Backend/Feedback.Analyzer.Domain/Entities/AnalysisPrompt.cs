using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class AnalysisPrompt : AuditableEntity, ICloneable<AnalysisPrompt>
{
    public string Prompt { get; set; } = default!;

    public int Version { get; set; }

    public int Revision { get; set; }

    public Guid CategoryId { get; set; }

    public AnalysisPromptCategory Category { get; set; } = default!;

    public IEnumerable<PromptExecutionHistory> ExecutionHistories { get; set; } = default!;

    public AnalysisPrompt Clone()
    {
        return new AnalysisPrompt
        {
            Prompt = Prompt,
            Version = Version,
            Revision = Revision,
            CategoryId = CategoryId
        };
    }
}
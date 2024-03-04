using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents an analysis prompt entity.
/// </summary>
public class AnalysisPrompt : AuditableEntity, ICloneable<AnalysisPrompt>
{
    /// <summary>
    /// Gets or sets the prompt text.
    /// </summary>
    public string Prompt { get; set; } = default!;

    /// <summary>
    /// Gets or sets the version of the analysis prompt.
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Gets or sets the revision number of the analysis prompt.
    /// </summary>
    public int Revision { get; set; }

    /// <summary>
    /// Creates a deep clone of the analysis prompt.
    /// </summary>
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

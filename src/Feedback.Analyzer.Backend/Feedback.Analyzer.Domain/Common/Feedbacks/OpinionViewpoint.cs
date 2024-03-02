using Feedback.Prompt.Analyzer.Domain.Enums;

namespace Feedback.Prompt.Analyzer.Domain.Common.Feedbacks;

/// <summary>
/// Represents opinion viewpoint
/// </summary>
public class OpinionViewpoint
{
    /// <summary>
    /// Gets or sets opinion target
    /// </summary>
    public string Target { get; set; } = default!;

    /// <summary>
    /// Gets or sets opinion type
    /// </summary>
    public OpinionType Type { get; set; } = default!;
}
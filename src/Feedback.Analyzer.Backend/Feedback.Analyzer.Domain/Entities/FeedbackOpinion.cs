using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents opinions provided in feedback.
/// </summary>
public class FeedbackOpinion
{
    /// <summary>
    /// Gets or sets the overall opinion type of the feedback.
    /// </summary>
    public OpinionType OverallOpinion { get; set; }

    /// <summary>
    /// Gets or sets an array of positive points mentioned in the feedback.
    /// </summary>
    public string[] PositiveOpinionPoints { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets an array of negative points mentioned in the feedback.
    /// </summary>
    public string[] NegativeOpinionPoints { get; set; } = Array.Empty<string>();
}
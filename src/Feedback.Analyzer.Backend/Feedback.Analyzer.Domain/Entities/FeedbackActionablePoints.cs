namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents actionable points extracted from feedback.
/// </summary>
public class FeedbackActionablePoints
{
    /// <summary>
    /// Gets or sets generic points that are actionable.
    /// </summary>
    public string[] GenericPoints { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets specific points that are actionable.
    /// </summary>
    public string[] SpecificPoints { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets actionable points.
    /// </summary>
    public string[] ActionablePoints { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets non-actionable points.
    /// </summary>
    public string[] NonActionablePoints { get; set; } = Array.Empty<string>();
}
namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents entities for storing facts and questions related to feedback.
/// </summary>
public class FeedbackEntities
{
    /// <summary>
    /// Gets or sets the array of facts related to the feedback.
    /// </summary>
    public string[] Facts { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the array of questions related to the feedback.
    /// </summary>
    public string[] Questions { get; set; } = Array.Empty<string>();
}
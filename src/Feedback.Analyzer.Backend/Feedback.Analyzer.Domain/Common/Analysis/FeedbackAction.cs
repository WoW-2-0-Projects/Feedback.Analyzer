namespace Feedback.Prompt.Analyzer.Domain.Common.Analysis;

/// <summary>
/// Represents feedback action
/// </summary>
public class FeedbackAction<TFeedbackPoint> 
{
    /// <summary>
    /// Gets or sets feedback point
    /// </summary>
    public TFeedbackPoint FeedbackPoint { get; set; } = default!;

    /// <summary>
    /// Gets or sets suggested action
    /// </summary>
    public string SuggestedAction { get; set; } = default!;
}
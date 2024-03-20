namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents the relevance and content of feedback.
/// </summary>
public class FeedbackRelevance
{
    /// <summary>
    /// Gets or sets a value indicating whether the feedback is relevant.
    /// </summary>
    public bool IsRelevant { get; set; }

    /// <summary>
    /// Gets or sets the extracted relevant content from the feedback.
    /// </summary>
    public string ExtractedRelevantContent { get; set; } = "";

    /// <summary>
    /// Gets or sets the personally identifiable information (PII) redacted content of the feedback.
    /// </summary>
    public string PiiRedactedContent { get; set; } = "";

    /// <summary>
    /// Gets or sets an array of recognized languages in the feedback content.
    /// </summary>
    public string[] RecognizedLanguages { get; set; } = Array.Empty<string>();
}
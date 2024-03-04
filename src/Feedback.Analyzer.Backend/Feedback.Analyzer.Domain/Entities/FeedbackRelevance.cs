namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackRelevance
{
    public bool IsRelevant { get; set; }

    public string ExtractedRelevantContent { get; set; } = default!;

    public string PiiRedactedContent { get; set; } = default!;

    public string[] RecognizedLanguages { get; set; } = default!;
}
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackOpinion
{
    public OpinionType OverallOpinion { get; set; }

    public string[] PositiveOpinionPoints { get; set; } = Array.Empty<string>();

    public string[] NegativeOpinionPoints { get; set; } = Array.Empty<string>();
}
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackOpinion
{
    public OpinionType OverallOpinion { get; set; }

    public string[] PositiveOpinionPoints { get; set; } = default!;

    public string[] NegativeOpinionPoints { get; set; } = default!;
}
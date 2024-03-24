using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;

/// <summary>
/// Represents the DTO (Data Transfer Object) for feedback analysis results.
/// </summary>
public class FeedbackAnalysisResultDto
{
    /// <summary>
    /// Gets or sets the feedback analysis result Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the relevance of the feedback.
    /// </summary>
    public FeedbackRelevance FeedbackRelevance { get; set; }

    /// <summary>
    /// Gets or sets the opinion conveyed by the feedback.
    /// </summary>
    public FeedbackOpinion FeedbackOpinion { get; set; }

    /// <summary>
    /// Gets or sets the actionable points extracted from the feedback.
    /// </summary>
    public FeedbackActionablePoints FeedbackActionablePoints { get; set; }

    /// <summary>
    /// Gets or sets the entities mentioned in the feedback.
    /// </summary>
    public FeedbackEntities FeedbackEntities { get; set; }

    /// <summary>
    /// Gets or sets the metrics associated with the feedback.
    /// </summary>
    public FeedbackMetrics FeedbackMetrics { get; set; }

    /// <summary>
    /// Gets or sets the customer feedback associated with this analysis result.
    /// </summary>
    public CustomerFeedbackDto CustomerFeedback { get; set; } = default!;
}
using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents the result of feedback analysis.
/// </summary>
public class FeedbackAnalysisResult : AuditableEntity
{
    /// <summary>
    /// Gets or sets the relevance of the feedback.
    /// </summary>
    public FeedbackRelevance FeedbackRelevance { get; set; }

    /// <summary>
    /// Gets or sets the opinion derived from the feedback.
    /// </summary>
    public FeedbackOpinion FeedbackOpinion { get; set; }

    /// <summary>
    /// Gets or sets the actionable points extracted from the feedback.
    /// </summary>
    public FeedbackActionablePoints FeedbackActionablePoints { get; set; }

    /// <summary>
    /// Gets or sets the entities identified in the feedback.
    /// </summary>
    public FeedbackEntities FeedbackEntities { get; set; }

    /// <summary>
    /// Gets or sets the metrics derived from the feedback.
    /// </summary>
    public FeedbackMetrics FeedbackMetrics { get; set; }

    /// <summary>
    /// Gets or sets the categorized opinions based on the feedback.
    /// </summary>
    public List<CategorizedOpinions> CategorizedOpinions { get; set; }

    /// <summary>
    /// Gets or sets the identifications of entities within the feedback.
    /// </summary>
    public List<EntityIdentification> EntityIdentifications { get; set; }

    /// <summary>
    /// Gets or sets the ID of the original customer feedback.
    /// </summary>
    public Guid CustomerFeedbackId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the original customer feedback.
    /// </summary>
    public virtual CustomerFeedback CustomerFeedback { get; set; }
}

using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;

/// <summary>
/// Represents a Data Transfer Object (DTO) containing information about the results of analyzing customer feedback.
/// </summary>
public class FeedbackAnalysisResultDto
{
    /// <summary>
    /// Gets the unique identifier of the feedback analysis result.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the relevance of the customer feedback.
    /// </summary>
    public FeedbackRelevance FeedbackRelevance { get; set; }

    /// <summary>
    /// Gets the opinion derived from the feedback.
    /// </summary>
    public FeedbackOpinion FeedbackOpinion { get; set; }

    /// <summary>
    /// Gets the actionable points extracted from the feedback.
    /// </summary>
    public FeedbackActionablePoints FeedbackActionablePoints { get; set; }

    /// <summary>
    /// Gets the entities identified in the feedback.
    /// </summary>
    public FeedbackEntities FeedbackEntities { get; set; }

    /// <summary>
    /// Gets the metrics derived from the feedback.
    /// </summary>
    public FeedbackMetrics FeedbackMetrics { get; set; }

    /// <summary>
    /// Gets a list of categorized opinions based on the feedback.
    /// </summary>
    public List<CategorizedOpinions> CategorizedOpinions { get; set; }

    /// <summary>
    /// Gets a list of identifications of entities within the feedback.
    /// </summary>
    public List<EntityIdentification> EntityIdentifications { get; set; }

    /// <summary>
    /// Gets the ID of the original customer feedback.
    /// </summary>
    public Guid CustomerFeedbackId { get; set; }

    /// <summary>
    /// Gets the reference to the original customer feedback.
    /// This property is currently marked as virtual, but may not be used in a DTO context.
    /// </summary>
    public virtual CustomerFeedback CustomerFeedback { get; set; }
}
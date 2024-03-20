using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents the result of feedback analysis.
/// </summary>
public class FeedbackAnalysisResult : AuditableEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedbackAnalysisResult"/> class.
    /// </summary>
    public FeedbackAnalysisResult()
    {
        FeedbackRelevance = new FeedbackRelevance();
        FeedbackOpinion = new FeedbackOpinion();
        FeedbackActionablePoints = new FeedbackActionablePoints();
        FeedbackEntities = new FeedbackEntities();
        FeedbackMetrics = new FeedbackMetrics();
    }
    
    /// <summary>
    /// Gets or sets the workflow result Id
    /// </summary>
    public Guid FeedbackAnalysisWorkflowResultId { get; set; }

    /// <summary>
    /// Gets or sets the relevance analysis result of the feedback.
    /// </summary>
    public FeedbackRelevance FeedbackRelevance { get; set; }

    /// <summary>
    /// Gets or sets the opinion analysis result of the feedback.
    /// </summary>
    public FeedbackOpinion FeedbackOpinion { get; set; }

    /// <summary>
    /// Gets or sets the actionable points analysis result of the feedback.
    /// </summary>
    public FeedbackActionablePoints FeedbackActionablePoints { get; set; }

    /// <summary>
    /// Gets or sets the entities analysis result of the feedback.
    /// </summary>
    public FeedbackEntities FeedbackEntities { get; set; }

    /// <summary>
    /// Gets or sets the metrics analysis result of the feedback.
    /// </summary>
    public FeedbackMetrics FeedbackMetrics { get; set; }
    
    /// <summary>
    /// Gets or sets the analysis result owned type.
    /// </summary>
    public AnalysisResult AnalysisResult { get; set; } = new();

    /// <summary>
    /// Gets or sets the ID of the customer feedback associated with this analysis result.
    /// </summary>
    public Guid CustomerFeedbackId { get; set; }

    /// <summary>
    /// Gets or sets the customer feedback associated with this analysis result.
    /// </summary>
    public CustomerFeedback CustomerFeedback { get; set; } = default!;
}
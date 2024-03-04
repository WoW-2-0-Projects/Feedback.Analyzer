using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackAnalysisResult : AuditableEntity
{
    public FeedbackRelevance FeedbackRelevance { get; set; }

    public FeedbackOpinion FeedbackOpinion { get; set; }

    public FeedbackActionablePoints FeedbackActionablePoints { get; set; }

    public FeedbackEntities FeedbackEntities { get; set; }

    public FeedbackMetrics FeedbackMetrics { get; set; }

    public List<CategorizedOpinions> CategorizedOpinions { get; set; }

    public List<EntityIdentification> EntityIdentifications { get; set; }

    public Guid CustomerFeedbackId { get; set; }

    public virtual CustomerFeedback CustomerFeedback { get; set; }
}
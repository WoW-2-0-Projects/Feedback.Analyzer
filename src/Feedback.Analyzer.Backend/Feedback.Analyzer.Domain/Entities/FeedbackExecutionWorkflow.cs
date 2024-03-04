using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackExecutionWorkflow : AuditableEntity
{
    public Guid ProductId { get; set; }

    public List<AnalysisPromptCategory> AnalysisPromptCategories { get; set; }
}
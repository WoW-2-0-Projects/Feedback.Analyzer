using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackAnalysisWorkflow : AuditableEntity
{
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;
    
    public AnalysisWorkflow Workflow { get; set; } = default!;

    public ICollection<FeedbackAnalysisWorkflowResult> Results { get; set; } = default!;
}
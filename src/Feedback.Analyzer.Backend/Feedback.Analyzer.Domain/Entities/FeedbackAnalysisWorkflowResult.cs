using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackAnalysisWorkflowResult : Entity
{
    public Guid WorkflowId { get; set; }
    
    public FeedbackAnalysisWorkflow Workflow { get; set; } = default!;
}
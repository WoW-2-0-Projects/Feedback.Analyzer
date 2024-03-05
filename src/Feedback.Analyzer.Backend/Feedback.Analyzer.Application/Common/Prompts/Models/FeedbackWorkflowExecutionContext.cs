namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public class FeedbackWorkflowExecutionContext : FeedbackExecutionContext
{
    public Guid WorkflowId { get; set; }
}
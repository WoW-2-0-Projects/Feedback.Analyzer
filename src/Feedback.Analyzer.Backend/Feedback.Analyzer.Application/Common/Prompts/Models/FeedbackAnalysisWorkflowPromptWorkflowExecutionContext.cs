using Feedback.Analyzer.Application.CustomerFeedbacks.Models;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public record FeedbackAnalysisWorkflowPromptWorkflowExecutionContext : FeedbackAnalysisWorkflowContext
{
    public Guid WorkflowId { get; set; }
}
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public class FeedbackAnalysisWorkflowPromptWorkflowExecutionContext : FeedbackAnalysisWorkflowContext
{
    public Guid WorkflowId { get; set; }
}
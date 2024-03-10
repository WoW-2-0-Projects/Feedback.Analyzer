using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;

public record ExecuteWorkflowSinglePromptEvent : Event
{
    public Guid WorkflowId { get; set; }
    
    public Guid PromptId { get; set; }
}
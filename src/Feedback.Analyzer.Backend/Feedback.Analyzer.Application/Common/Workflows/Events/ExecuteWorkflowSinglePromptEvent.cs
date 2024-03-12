using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.Workflows.Events;

public class ExecuteWorkflowSinglePromptEvent : Event
{
    public Guid WorkflowId { get; init; }
    
    public Guid PromptId { get; init; }
}
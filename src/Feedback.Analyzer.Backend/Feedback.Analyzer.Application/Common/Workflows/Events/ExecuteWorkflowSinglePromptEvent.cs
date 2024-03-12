using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.Workflows.Events;

/// <summary>
/// Represents an event for executing a workflow with a single prompt.
/// </summary>
public class ExecuteWorkflowSinglePromptEvent : Event
{
    public Guid WorkflowId { get; init; }
    
    public Guid PromptId { get; init; }
}
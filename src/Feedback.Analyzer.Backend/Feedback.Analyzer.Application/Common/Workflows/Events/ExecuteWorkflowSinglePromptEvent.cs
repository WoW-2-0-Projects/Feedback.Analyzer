using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.Workflows.Events;

/// <summary>
/// Represents an event for executing a workflow with a single prompt.
/// </summary>
public record ExecuteWorkflowSinglePromptEvent : EventBase
{
    /// <summary>
    /// Gets or sets the unique identifier of the workflow associated with the event.
    /// </summary>
    public Guid WorkflowId { get; init; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the prompt associated with the event.
    /// </summary>
    public Guid PromptId { get; init; }
}
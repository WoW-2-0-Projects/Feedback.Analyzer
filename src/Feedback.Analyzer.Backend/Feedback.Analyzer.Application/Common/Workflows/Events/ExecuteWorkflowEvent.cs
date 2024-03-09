using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.Workflows.Events;

/// <summary>
/// Represents workflow execution event
/// </summary>
public record ExecuteWorkflowEvent : Event
{
    /// <summary>
    /// Gets workflow Id
    /// </summary>
    public Guid WorkflowId { get; init; }
}
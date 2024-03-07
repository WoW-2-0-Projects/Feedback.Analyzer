using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.Workflows.Events;

public class ExecuteWorkflowEvent : Event
{
    public Guid WorkflowId { get; set; }
}
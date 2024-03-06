using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.FeedbackWorkflows.Commands;

/// <summary>
/// Represents a command for creating a new feedback workflow.
/// </summary>
public record FeedbackWorkflowCreateCommand : ICommand<FeedbackWorkflowDto>
{
    /// <summary>
    /// Gets or sets the feedback workflow data transfer object.
    /// </summary>
    public FeedbackWorkflowDto FeedbackWorkflow { get; set; } = default!;
}

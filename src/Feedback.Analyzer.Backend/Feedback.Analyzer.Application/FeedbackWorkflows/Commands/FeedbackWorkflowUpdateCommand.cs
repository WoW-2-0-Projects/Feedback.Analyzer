using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.FeedbackWorkflows.Commands;

/// <summary>
/// Represents a command for updating a feedback workflow.
/// </summary>
public record FeedbackWorkflowUpdateCommand : ICommand<FeedbackWorkflowDto>
{
    /// <summary>
    /// Gets or sets the feedback workflow data transfer object containing the updated information.
    /// </summary>
    public FeedbackWorkflowDto FeedbackWorkflow { get; set; } = default!;
}

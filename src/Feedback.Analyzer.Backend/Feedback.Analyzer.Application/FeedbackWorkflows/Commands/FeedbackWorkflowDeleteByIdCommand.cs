using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.FeedbackWorkflows.Commands;

/// <summary>
/// Represents a command for deleting a feedback workflow by its unique identifier.
/// </summary>
public record FeedbackWorkflowDeleteByIdCommand : ICommand<bool>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product associated with the feedback workflow to be deleted.
    /// </summary>
    public Guid FeedbackWorkflowId { get; set; }
}

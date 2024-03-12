using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;

/// <summary>
/// Represents a command for deleting a feedback analysis workflow by its ID.
/// </summary>
public record FeedbackAnalysisWorkflowDeleteByIdCommand : ICommand<bool>
{
    /// <summary>
    /// Gets or sets the ID of the feedback analysis workflow to delete.
    /// </summary>
    public Guid FeedbackAnalysisWorkflowId { get; set; }
}
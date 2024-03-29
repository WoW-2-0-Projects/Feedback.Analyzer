using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;

/// <summary>
/// Represents a command for updating a feedback analysis workflow.
/// </summary>
public record FeedbackAnalysisWorkflowUpdateCommand : ICommand<FeedbackAnalysisWorkflowDto>
{
    /// <summary>
    /// Gets or sets the data transfer object (DTO) representing the feedback analysis workflow to be updated.
    /// </summary>
    public FeedbackAnalysisWorkflowDto FeedbackAnalysisWorkflow { get; set; } = default!;
}

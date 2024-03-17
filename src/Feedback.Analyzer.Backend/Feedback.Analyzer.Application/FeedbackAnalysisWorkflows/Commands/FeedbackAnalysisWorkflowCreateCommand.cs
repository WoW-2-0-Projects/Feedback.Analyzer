using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;

/// <summary>
/// Represents a command for creating a feedback analysis workflow.
/// </summary>
public record FeedbackAnalysisWorkflowCreateCommand : ICommand<FeedbackAnalysisWorkflowDto>
{
     /// <summary>
     /// Gets or sets the data transfer object (DTO) representing the feedback analysis workflow to be created.
     /// </summary>
     public FeedbackAnalysisWorkflowDto FeedbackAnalysisWorkflow { get; set; } = default!;
}

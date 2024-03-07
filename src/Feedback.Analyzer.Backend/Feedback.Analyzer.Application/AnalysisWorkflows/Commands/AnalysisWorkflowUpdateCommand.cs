using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.AnalysisWorkflows.Commands;

/// <summary>
/// Represents a command for updating an analysis workflow.
/// </summary>
public record AnalysisWorkflowUpdateCommand : ICommand<AnalysisWorkflowDto>
{
    /// <summary>
    /// Gets or sets the analysis workflow data to update.
    /// </summary>
    public AnalysisWorkflowDto AnalysisWorkflow { get; set; } = default!;
}

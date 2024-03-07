using System.Windows.Input;
using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.AnalysisWorkflows.Commands;

/// <summary>
/// Represents a command for creating an analysis workflow.
/// </summary>
public record AnalysisWorkflowCreateCommand : ICommand<AnalysisWorkflowDto>
{
    /// <summary>
    /// Gets or sets the analysis workflow data to create.
    /// </summary>
    public AnalysisWorkflowDto AnalysisWorkflow { get; set; } = default!;
}

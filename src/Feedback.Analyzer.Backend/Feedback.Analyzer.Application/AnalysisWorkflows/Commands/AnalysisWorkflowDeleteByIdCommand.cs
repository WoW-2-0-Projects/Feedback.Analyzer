using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.AnalysisWorkflows.Commands;

/// <summary>
/// Represents a command for deleting an analysis workflow.
/// </summary>
public record AnalysisWorkflowDeleteByIdCommand : ICommand<bool>
{
    /// <summary>
    /// The unique identifier of the Analysis Workflow to be deleted.
    /// </summary>
    public Guid AnalysisWorkflowId { get; set; }
}

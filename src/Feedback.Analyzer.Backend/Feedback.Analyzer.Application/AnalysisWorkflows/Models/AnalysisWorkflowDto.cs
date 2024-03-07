using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.AnalysisWorkflows.Models;

/// <summary>
/// Represents a data transfer object (DTO) for an analysis workflow.
/// </summary>
public class AnalysisWorkflowDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the analysis workflow.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the analysis workflow.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the analysis workflow.
    /// </summary>
    public WorkflowType Type { get; set; }
}

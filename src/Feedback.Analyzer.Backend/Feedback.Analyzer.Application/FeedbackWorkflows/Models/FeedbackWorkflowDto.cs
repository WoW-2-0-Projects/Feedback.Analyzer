using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.FeedbackWorkflows.Models;

/// <summary>
/// Data transfer object (DTO) representing feedback workflow information.
/// </summary>
public class FeedbackWorkflowDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the Feedback workflow.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the product associated with the feedback workflow.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the feedback workflow.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the workflow.
    /// </summary>
    public WorkflowType Type { get; set; }
}
using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents an feedback workflow entity, extending AuditableEntity for audit-related properties.
/// </summary>
public class FeedbackWorkflow : AuditableEntity
{
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
using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents a feedback analysis workflow.
/// </summary>
public class FeedbackAnalysisWorkflow : Entity
{
    /// <summary>
    /// Gets or sets the ID of the product associated with the feedback analysis workflow.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Navigation to related analysis workflow
    /// </summary>
    public AnalysisWorkflow AnalysisWorkflow { get; set; } = default!;

    /// <summary>
    /// Navigation to related product
    /// </summary>
    public WorkflowType Type { get; set; }
    public Product Product { get; set; } = default!;
}
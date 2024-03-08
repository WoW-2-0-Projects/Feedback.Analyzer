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
    /// Gets or sets the name of the feedback analysis workflow.
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the type of the feedback analysis workflow.
    /// </summary>
    public WorkflowType Type { get; set; }

    public AnalysisWorkflow AnalysisWorkflow { get; set; } = default!;

    public Product Product { get; set; } = default!;
}
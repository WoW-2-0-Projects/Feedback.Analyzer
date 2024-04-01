using System.ComponentModel.DataAnnotations.Schema;
using Feedback.Analyzer.Domain.Common.Entities;

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
    public Product Product { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the collection of feedback analysis workflow results associated with this entity.
    /// </summary>
    public ICollection<FeedbackAnalysisWorkflowResult> Results { get; set; } = default!;
}
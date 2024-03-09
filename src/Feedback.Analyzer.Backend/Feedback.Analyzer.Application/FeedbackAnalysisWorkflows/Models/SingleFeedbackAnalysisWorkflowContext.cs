using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;

/// <summary>
/// Represents prompt execution context for feedback analysis.
/// </summary>
public record SingleFeedbackAnalysisWorkflowContext : WorkflowContext
{
    /// <summary>
    /// Gets or sets workflow result id
    /// </summary>
    public Guid WorkflowResultId { get; set; }
    
    /// <summary>
    /// Organization of the product
    /// </summary>
    public Organization Organization { get; set; } = default!;

    /// <summary>
    /// Product to analyze
    /// </summary>
    public Product Product { get; set; } = default!;
    
    /// <summary>
    /// Gets customer feedback Id
    /// </summary>
    public Guid FeedbackId { get; set; }
}
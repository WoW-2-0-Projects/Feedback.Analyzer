using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;

/// <summary>
/// Define a record type representing the context of a single feedback analysis workflow.
/// </summary>
public record SingleFeedbackAnalysisWorkflowContext : WorkflowContext
{
    /// <summary>
    /// Gets or sets the ID of the workflow result associated with this context.
    /// </summary>
    public Guid WorkflowResultId { get; set; }

    /// <summary>
    /// Gets or sets the organization associated with this context.
    /// </summary>
    public Organization Organization { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product associated with this context.
    /// </summary>
    public Product Product { get; set; } = default!;

    /// <summary>
    /// Gets or sets the ID of the feedback associated with this context.
    /// </summary>
    public Guid FeedbackId { get; set; }

    /// <summary>
    /// Gets or sets the result of the feedback analysis associated with this context.
    /// </summary>
    public FeedbackAnalysisResult Result { get; set; } = default!;
}
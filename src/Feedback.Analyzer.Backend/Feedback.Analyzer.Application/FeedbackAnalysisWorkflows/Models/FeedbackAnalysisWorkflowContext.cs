using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;

/// <summary>
/// Represents feedback analysis workflow context
/// </summary>
public record FeedbackAnalysisWorkflowContext : WorkflowContext
{
    /// <summary>
    /// Organization of the product
    /// </summary>
    public Organization Organization { get; set; } = default!;

    /// <summary>
    /// Product to analyze
    /// </summary>
    public Product Product { get; set; } = default!;

    /// <summary>
    /// Gets feedbacks Id
    /// </summary>
    public ICollection<Guid> FeedbacksId { get; set; } = new List<Guid>();
}
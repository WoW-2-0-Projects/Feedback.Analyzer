using System.Collections.Immutable;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.CustomerFeedbacks.Models;

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
    public IImmutableList<Guid> FeedbacksId { get; init; } = new ImmutableArray<Guid>();

    // /// <summary>
    // /// Gets feedback analysis result
    // /// </summary>
    // public FeedbackAnalysisResult AnalysisResult { get; set; } = default!;
}
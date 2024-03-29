using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;

/// <summary>
/// Represents a query for retrieving feedback analysis workflows based on a filter.
/// </summary>
public record FeedbackAnalysisWorkflowGetQuery  : IQuery<ICollection<FeedbackAnalysisWorkflowDto>>
{
    /// <summary>
    /// Gets or sets the filter criteria for retrieving feedback analysis workflows.
    /// </summary>
    public FeedbackAnalysisWorkflowFilter Filter { get; set; } = default!;
}
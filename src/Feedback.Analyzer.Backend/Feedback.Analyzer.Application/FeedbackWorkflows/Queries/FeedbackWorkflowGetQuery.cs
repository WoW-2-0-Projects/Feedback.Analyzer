using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackWorkflows.Queries;

/// <summary>
/// Represents a query for retrieving feedback workflows based on a filter.
/// </summary>
public record FeedbackWorkflowGetQuery : IQuery<ICollection<FeedbackWorkflowDto>>
{
    /// <summary>
    /// Gets or sets the filter criteria for retrieving feedback workflows.
    /// </summary>
    public FeedbackWorkflowFilter Filter { get; set; } = default!;
}

using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackWorkflows.Queries;

/// <summary>
/// Represents a query for retrieving a feedback workflow by its unique identifier.
/// </summary>
public record FeedbackWorkflowGetByIdQuery : IQuery<FeedbackWorkflowDto>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product associated with the feedback workflow.
    /// </summary>
    public Guid FeedbackWorkflowId { get; set; }
}
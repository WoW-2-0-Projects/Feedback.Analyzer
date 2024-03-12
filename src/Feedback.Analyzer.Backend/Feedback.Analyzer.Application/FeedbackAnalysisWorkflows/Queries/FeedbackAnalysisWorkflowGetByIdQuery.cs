using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;

/// <summary>
/// Represents a query for retrieving a feedback analysis workflow by its ID.
/// </summary>
public record FeedbackAnalysisWorkflowGetByIdQuery : IQuery<FeedbackAnalysisWorkflowDto>
{
    /// <summary>
    /// Gets or sets the ID of the feedback analysis workflow to retrieve.
    /// </summary>
    public Guid FeedbackAnalysisWorkflowId { get; set; }
}
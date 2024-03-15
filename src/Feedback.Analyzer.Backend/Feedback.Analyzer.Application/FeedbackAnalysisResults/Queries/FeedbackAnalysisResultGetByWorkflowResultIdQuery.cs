using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;

/// <summary>
/// Represents a query to retrieve feedback analysis result by workflow result ID.
/// </summary>
public record FeedbackAnalysisResultGetByWorkflowResultIdQuery : IQuery<FeedbackAnalysisResultDto?>
{
    /// <summary>
    /// Gets or sets the workflow result ID.
    /// </summary>
    public Guid WorkflowResultId { get; set; }
}
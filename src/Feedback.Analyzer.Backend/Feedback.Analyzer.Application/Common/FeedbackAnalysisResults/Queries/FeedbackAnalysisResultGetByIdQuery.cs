using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Queries;

/// <summary>
/// Represents a query to retrieve a feedback analysis result by its ID.
/// </summary>
public record FeedbackAnalysisResultGetByIdQuery : IQuery<FeedbackAnalysisResultDto?>
{
    /// <summary>
    /// Gets the ID of the feedback analysis result to retrieve.
    /// </summary>
    public Guid FeedbackAnalysisResultId { get; set; }
}

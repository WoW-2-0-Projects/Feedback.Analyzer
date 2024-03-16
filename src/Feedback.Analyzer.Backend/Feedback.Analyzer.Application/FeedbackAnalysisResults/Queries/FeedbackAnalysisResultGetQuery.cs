using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;

/// <summary>
/// Represents a query to retrieve feedback analysis results.
/// </summary>
public record FeedbackAnalysisResultGetQuery : IQuery<ICollection<FeedbackAnalysisResultDto>>
{
    /// <summary>
    /// Gets or sets the filter to apply while retrieving the feedback analysis results.
    /// </summary>
    public FeedbackAnalysisResultFilter Filter { get; set; } = default!;
}
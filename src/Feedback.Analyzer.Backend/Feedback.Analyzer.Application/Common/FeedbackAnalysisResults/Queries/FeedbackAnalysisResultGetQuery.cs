using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Queries;

/// <summary>
/// Represents a query to retrieve feedback analysis results.
/// </summary>
public record FeedbackAnalysisResultGetQuery : IQuery<ICollection<FeedbackAnalysisResultDto>>
{
    /// <summary>
    /// Gets or sets an optional expression used to filter the retrieved feedback analysis results.
    /// Defaults to null, retrieving all results.
    /// </summary>
    public FeedbackAnalysisResultFilter Filter { get; set; } = default!;
}
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;

/// <summary>
/// Represents a query to retrieve feedback analysis result by Id
/// </summary>
/// <param name="ResultId">Id of the feedback analysis result</param>
public record FeedbackAnalysisResultGetByIdQuery(Guid ResultId) : IQuery<FeedbackAnalysisResultDto?>;

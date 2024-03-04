using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository interface for managing feedback analysis results.
/// </summary>
public interface IFeedbackAnalysisResultRepository
{
    /// <summary>
    /// Retrieves a queryable collection of feedback analysis results based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">The predicate to filter feedback analysis results.</param>
    /// <param name="queryOptions">The options for querying feedback analysis results.</param>
    /// <returns>A queryable collection of feedback analysis results.</returns>
    IQueryable<FeedbackAnalysisResult> Get(Expression<Func<FeedbackAnalysisResult, bool>>? predicate = default,
        QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a feedback analysis result by its unique identifier asynchronously.
    /// </summary>
    /// <param name="feedbackAnalysisResultId">The unique identifier of the feedback analysis result.</param>
    /// <param name="queryOptions">The options for querying feedback analysis results.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the feedback analysis result, if found; otherwise, null.</returns>
    ValueTask<FeedbackAnalysisResult?> GetByIdAsync(Guid feedbackAnalysisResultId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default); 
}

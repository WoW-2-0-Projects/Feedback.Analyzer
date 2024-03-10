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
    
    /// <summary>
    /// Creates a feedback analysis result
    /// </summary>
    /// <param name="result">The result to be created.</param>
    /// <param name="commandOptions">Commands options for the creation operation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created feedback analysis result.</returns>
    ValueTask<FeedbackAnalysisResult> CreateAsync(FeedbackAnalysisResult result,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously deletes a result from by its Id
    /// </summary>
    /// <param name="resultId">If of feedback analysis result being deleted</param>
    /// <param name="commandOptions">Commands options for the deletion operation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Deleted feedback analysis result if soft deletion enabled, otherwise null</returns>
    ValueTask<FeedbackAnalysisResult?> DeleteByIdAsync(Guid resultId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}

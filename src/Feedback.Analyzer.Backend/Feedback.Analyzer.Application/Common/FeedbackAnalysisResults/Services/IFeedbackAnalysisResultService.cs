using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Services;

/// <summary>
/// Defines an interface for services related to feedback analysis results.
/// </summary>
public interface IFeedbackAnalysisResultService
{
    /// <summary>
    /// Retrieves a queryable collection of feedback analysis results.
    /// </summary>
    /// <param name="predicate">An optional expression used to filter the results. Defaults to null, retrieving all results.</param>
    /// <param name="queryOptions">Optional configuration options for the query execution. Defaults to an empty configuration.</param>
    /// <returns>An IQueryable object representing the retrieved feedback analysis results.</returns>
    IQueryable<FeedbackAnalysisResult> Get(Expression<Func<FeedbackAnalysisResult, bool>>? predicate = default, QueryOptions queryOptions = default);

    IQueryable<FeedbackAnalysisResult> Get(FeedbackAnalysisResultFilter feedbackAnalysisResultFilter, QueryOptions queryOptions = default);

    /// <summary>
    /// Asynchronously retrieves a single feedback analysis result by its ID.
    /// </summary>
    /// <param name="feedbackAnalysisResultId">The ID of the feedback analysis result to retrieve.</param>
    /// <param name="queryOptions">Optional configuration options for the query execution. Defaults to an empty configuration.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask holding the retrieved feedback analysis result (can be null if not found), or a cancelled ValueTask if the operation is cancelled.</returns>
    ValueTask<FeedbackAnalysisResult?> GetByIdAsync(
        Guid feedbackAnalysisResultId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Creates feedback analysis result
    /// </summary>
    /// <param name="analysisResult">Analysis result to create.</param>
    /// <param name="commandOptions">Command options for the creation</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Created <see cref="FeedbackAnalysisResult"/> instance</returns>
    ValueTask<FeedbackAnalysisResult> CreateAsync(
        FeedbackAnalysisResult analysisResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes feedback analysis result by Id
    /// </summary>
    /// <param name="analysisResultId">The Id of the feedback analysis result to delete.</param>
    /// <param name="commandOptions">Command options for the deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Deleted feedback analysis result if soft deletion enabled, otherwise null</returns>
    ValueTask<FeedbackAnalysisResult?> DeleteByIdAsync(
        Guid analysisResultId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );
}
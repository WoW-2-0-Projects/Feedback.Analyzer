using System.Linq.Expressions;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;

/// <summary>
/// Interface for the feedback analysis result service.
/// </summary>
public interface IFeedbackAnalysisResultService
{
    /// <summary>
    /// Retrieves feedback analysis results based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">A predicate to filter the results.</param>
    /// <param name="queryOptions">Options for querying.</param>
    /// <returns>An <see cref="IQueryable{T}"/> of <see cref="FeedbackAnalysisResult"/>.</returns>
    IQueryable<FeedbackAnalysisResult> Get(Expression<Func<FeedbackAnalysisResult, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Retrieves feedback analysis results based on the specified filter and query options.
    /// </summary>
    /// <param name="feedbackAnalysisResultFilter">The filter to apply.</param>
    /// <param name="queryOptions">Options for querying.</param>
    /// <returns>An <see cref="IQueryable{T}"/> of <see cref="FeedbackAnalysisResult"/>.</returns>
    IQueryable<FeedbackAnalysisResult> Get(FeedbackAnalysisResultFilter feedbackAnalysisResultFilter, QueryOptions queryOptions = default);
   
    /// <summary>
    /// Asynchronously retrieves a feedback analysis result by its ID.
    /// </summary>
    /// <param name="feedbackId">The ID of the feedback analysis result to retrieve.</param>
    /// <param name="queryOptions">Options for querying.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, containing the feedback analysis result, or null if not found.</returns>
    ValueTask<FeedbackAnalysisResult?> GetByIdAsync(Guid feedbackId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
}
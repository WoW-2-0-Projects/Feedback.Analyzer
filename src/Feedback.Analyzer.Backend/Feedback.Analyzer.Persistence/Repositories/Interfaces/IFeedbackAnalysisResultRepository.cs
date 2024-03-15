﻿using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for accessing and managing feedback analysis results.
/// </summary>
public interface IFeedbackAnalysisResultRepository
{
    /// <summary>
    /// Retrieves feedback analysis results based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">The predicate to filter feedback analysis results.</param>
    /// <param name="queryOptions">Options for customizing the query.</param>
    /// <returns>An <see cref="IQueryable"/> of feedback analysis results.</returns>
    IQueryable<FeedbackAnalysisResult> Get(Expression<Func<FeedbackAnalysisResult, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Retrieves a feedback analysis result by its identifier asynchronously.
    /// </summary>
    /// <param name="feedbackId">The unique identifier of the feedback analysis result.</param>
    /// <param name="queryOptions">Options for customizing the query.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="FeedbackAnalysisResult"/> instance if found, otherwise null.</returns>
    ValueTask<FeedbackAnalysisResult?> GetByIdAsync(Guid feedbackId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously creates a new feedback analysis result.
    /// </summary>
    /// <param name="feedbackAnalysisResult">The feedback analysis result to create.</param>
    /// <param name="commandOptions">Options for the command.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, containing the created feedback analysis result.</returns>
    ValueTask<FeedbackAnalysisResult> CreateAsync(FeedbackAnalysisResult feedbackAnalysisResult, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}
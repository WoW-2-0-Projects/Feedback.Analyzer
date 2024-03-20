﻿using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Repository interface for managing history execution history.
/// </summary>
public interface IPromptExecutionHistoryRepository
{
    /// <summary>
    /// Retrieves a queryable collection of history execution history records based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">An optional predicate to filter the history execution history records.</param>
    /// <param name="queryOptions">Optional query options for paging, sorting, etc.</param>
    /// <returns>A queryable collection of history execution history records.</returns>
    IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Creates a new history execution history record asynchronously.
    /// </summary>
    /// <param name="history">The history execution history record to be created.</param>
    /// <param name="commandOptions">Optional command options for the creation operation.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, containing the created history execution history record.</returns>
    ValueTask<PromptExecutionHistory> CreateAsync(PromptExecutionHistory history,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Repository interface for managing prompt execution history.
/// </summary>
public interface IPromptExecutionHistoryRepository
{
    /// <summary>
    /// Retrieves a queryable collection of prompt execution history records based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">An optional predicate to filter the prompt execution history records.</param>
    /// <param name="queryOptions">Optional query options for paging, sorting, etc.</param>
    /// <returns>A queryable collection of prompt execution history records.</returns>
    IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Creates a new prompt execution history record asynchronously.
    /// </summary>
    /// <param name="prompt">The prompt execution history record to be created.</param>
    /// <param name="commandOptions">Optional command options for the creation operation.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, containing the created prompt execution history record.</returns>
    ValueTask<PromptExecutionHistory> CreateAsync(PromptExecutionHistory prompt,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
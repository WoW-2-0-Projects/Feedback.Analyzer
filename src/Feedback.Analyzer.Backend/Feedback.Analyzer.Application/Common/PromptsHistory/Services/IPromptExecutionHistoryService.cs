using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.PromptsHistory.Services;

/// <summary>
/// Service interface for managing prompt execution history records.
/// </summary>
public interface IPromptExecutionHistoryService
{
    /// <summary>
    /// Retrieves prompt execution history records based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">The predicate to filter prompt execution history records.</param>
    /// <param name="queryOptions">The query options to apply.</param>
    /// <returns>An <see cref="IQueryable{T}"/> of prompt execution history records.</returns>
    IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate,
        QueryOptions queryOptions);

    /// <summary>
    /// Retrieves prompt execution history records by prompt ID asynchronously.
    /// </summary>
    /// <param name="promptId">The ID of the prompt.</param>
    /// <param name="queryOptions">The query options to apply.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, containing a list of prompt execution history records.</returns>

    ValueTask<IList<PromptExecutionHistory>> GetByPromptIdAsync(
        Guid promptId,
        QueryOptions queryOptions,
        CancellationToken cancellationToken
        );

    /// <summary>
    /// Creates a new prompt execution history record asynchronously.
    /// </summary>
    /// <param name="promptExecutionHistory">The prompt execution history to create.</param>
    /// <param name="commandOptions">The command options to apply.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, containing the created prompt execution history record.</returns>
    ValueTask<PromptExecutionHistory> CreateAsync(
        PromptExecutionHistory promptExecutionHistory, 
        CommandOptions commandOptions,
        CancellationToken cancellationToken
        );
}
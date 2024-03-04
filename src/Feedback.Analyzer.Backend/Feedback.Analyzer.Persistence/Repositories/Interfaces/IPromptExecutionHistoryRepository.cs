using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

public interface IPromptExecutionHistoryRepository
{
    /// <summary>
    /// Retrieves a queryable collection of prompt entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the clients (optional).</param>
    /// <param name="queryOptions">Indicates whether the entities should be queried without tracking changes (default is false).</param>
    /// <returns>A queryable collection of prompt entities.</returns>
    IQueryable<PromptExecutionHistory> Get(Expression<Func<PromptExecutionHistory, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    
    /// <summary>
    /// Asynchronously creates a new prompt in the repository.
    /// </summary>
    /// <param name="prompt">The prompt to be created.</param>
    /// <param name="commandOptions">Commands options for the creation operation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that yields the created prompt.</returns>
    ValueTask<PromptExecutionHistory> CreateAsync(PromptExecutionHistory prompt,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
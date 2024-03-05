using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for managing prompt entities in the repository.
/// </summary>
public interface IPromptRepository
{
    /// <summary>
    /// Retrieves a queryable collection of prompt entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the clients (optional).</param>
    /// <param name="queryOptions">Indicates whether the entities should be queried without tracking changes (default is false).</param>
    /// <returns>A queryable collection of prompt entities.</returns>
    IQueryable<AnalysisPrompt> Get(Expression<Func<AnalysisPrompt, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Asynchronously retrieves a prompt entity by its unique identifier.
    /// </summary>
    /// <param name="promptId">The unique identifier of the prompt.</param>
    /// <param name="queryOptions">Indicates whether the entity should be queried without tracking changes (default is false).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the prompt entity, or null if not found.</returns>
    ValueTask<AnalysisPrompt?> GetByIdAsync(Guid promptId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new prompt in the repository.
    /// </summary>
    /// <param name="prompt">The prompt to be created.</param>
    /// <param name="commandOptions">Command options for the creation operation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that yields the created prompt.</returns>
    ValueTask<AnalysisPrompt> CreateAsync(AnalysisPrompt prompt,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously updates an existing prompt entity.
    /// </summary>
    /// <param name="prompt">The prompt entity to update.</param>
    /// <param name="commandOptions">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the updated prompt entity.</returns>
    ValueTask<AnalysisPrompt> UpdateAsync(
        AnalysisPrompt prompt,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );
    
    /// <summary>
    /// Deletes an existing prompt record.
    /// </summary>
    /// <param name="prompt">The prompt object to be deleted.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted prompt object if successful, otherwise null (e.g., if the prompt was not found).</returns>
    ValueTask<AnalysisPrompt?> DeleteAsync(AnalysisPrompt prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes a prompt entity by its unique identifier.
    /// </summary>
    /// <param name="promptId">The unique identifier of the prompt to delete.</param>
    /// <param name="commandOptions">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted prompt entity, or null if not found.</returns>
    ValueTask<AnalysisPrompt?> DeleteByIdAsync(Guid promptId, CommandOptions commandOptions, CancellationToken cancellationToken = default);
}
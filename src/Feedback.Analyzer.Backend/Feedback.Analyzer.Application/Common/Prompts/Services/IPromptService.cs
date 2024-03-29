using System.Linq.Expressions;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Prompts.Services;

/// <summary>
/// Interface for managing AnalysisPrompt entities.
/// </summary>
public interface IPromptService
{
    /// <summary>
    /// Retrieves a collection of AnalysisPrompt entities based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">The predicate to filter the AnalysisPrompt entities.</param>
    /// <param name="queryOptions">Query options for the retrieval.</param>
    /// <returns>A collection of AnalysisPrompt entities.</returns>
    IQueryable<AnalysisPrompt> Get(Expression<Func<AnalysisPrompt, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a collection of AnalysisPrompt entities based on the provided PromptFilter and query options.
    /// </summary>
    /// <param name="promptFilter">The PromptFilter to apply for filtering.</param>
    /// <param name="queryOptions">Query options for the retrieval.</param>
    /// <returns>A collection of AnalysisPrompt entities.</returns>
    IQueryable<AnalysisPrompt> Get(PromptFilter promptFilter, QueryOptions queryOptions = default);

    /// <summary>
    /// Asynchronously retrieves a single AnalysisPrompt entity by its ID.
    /// </summary>
    /// <param name="promptId">The ID of the AnalysisPrompt entity to retrieve.</param>
    /// <param name="queryOptions">Query options for the retrieval.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the AnalysisPrompt entity.</returns>
    ValueTask<AnalysisPrompt?> GetByIdAsync(Guid promptId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new AnalysisPrompt entity.
    /// </summary>
    /// <param name="prompt">The AnalysisPrompt entity to create.</param>
    /// <param name="commandOptions">Command options for the creation.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the created AnalysisPrompt entity.</returns>
    ValueTask<AnalysisPrompt> CreateAsync(AnalysisPrompt prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing AnalysisPrompt entity.
    /// </summary>
    /// <param name="prompt">The AnalysisPrompt entity to update.</param>
    /// <param name="commandOptions">Command options for the update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the updated AnalysisPrompt entity.</returns>
    ValueTask<AnalysisPrompt> UpdateAsync(AnalysisPrompt prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the specified AnalysisPrompt entity.
    /// </summary>
    /// <param name="prompt">The AnalysisPrompt entity to delete.</param>
    /// <param name="commandOptions">Command options for the deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the deleted AnalysisPrompt entity.</returns>
    ValueTask<AnalysisPrompt?> DeleteAsync(AnalysisPrompt prompt, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the AnalysisPrompt entity with the specified ID.
    /// </summary>
    /// <param name="promptId">The ID of the AnalysisPrompt entity to delete.</param>
    /// <param name="commandOptions">Command options for the deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the deleted AnalysisPrompt entity.</returns>
    ValueTask<AnalysisPrompt?> DeleteByIdAsync(Guid promptId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}


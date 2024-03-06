using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository interface for managing FeedbackWorkflow entities.
/// </summary>
public interface IFeedbackWorkflowRepository
{
    /// <summary>
    /// Retrieves FeedbackWorkflow entities based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter the entities.</param>
    /// <param name="queryOptions">Options for customizing the query behavior.</param>
    /// <returns>An IQueryable of FeedbackWorkflow entities.</returns>
    public IQueryable<FeedbackWorkflow> Get(Expression<Func<FeedbackWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves an FeedbackWorkflow entity asynchronously by its unique identifier.
    /// </summary>
    /// <param name="workflowId">The unique identifier of the FeedbackWorkflow entity.</param>
    /// <param name="queryOptions">Options for customizing the query behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the retrieved FeedbackWorkflow entity.</returns>
    public ValueTask<FeedbackWorkflow> GetByIdAsync(Guid workflowId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new FeedbackWorkflow entity.
    /// </summary>
    /// <param name="feedbackWorkflow">The FeedbackWorkflow entity to be created.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the newly created FeedbackWorkflow entity.</returns>
    public ValueTask<FeedbackWorkflow> CreateAsync(FeedbackWorkflow feedbackWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing FeedbackWorkflow entity.
    /// </summary>
    /// <param name="feedbackWorkflow">The FeedbackWorkflow entity to be updated.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the updated FeedbackWorkflow entity.</returns>
    public ValueTask<FeedbackWorkflow> UpdateAsync(FeedbackWorkflow feedbackWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the specified FeedbackWorkflow entity.
    /// </summary>
    /// <param name="feedbackWorkflow">The FeedbackWorkflow entity to be deleted.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the deleted FeedbackWorkflow entity.</returns>
    public ValueTask<FeedbackWorkflow> DeleteAsync(FeedbackWorkflow feedbackWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the FeedbackWorkflow entity with the specified identifier.
    /// </summary>
    /// <param name="workflowId">The unique identifier of the FeedbackWorkflows entity to be deleted.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the deleted FeedbackWorkflows entity.</returns>
    public ValueTask<FeedbackWorkflow> DeleteByIdAsync(Guid workflowId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}
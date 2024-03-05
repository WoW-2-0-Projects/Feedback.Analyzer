using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Workflows.Services;

/// <summary>
/// Interface for managing FeedbackExecutionWorkflow entities.
/// </summary>
public interface IFeedbackExecutionWorkflowService
{
    /// <summary>
    /// Retrieves a collection of FeedbackExecutionWorkflow entities based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">The predicate to filter the FeedbackExecutionWorkflow entities.</param>
    /// <param name="queryOptions">Query options for the retrieval.</param>
    /// <returns>A collection of FeedbackExecutionWorkflow entities.</returns>
    IQueryable<FeedbackExecutionWorkflow> Get(Expression<Func<FeedbackExecutionWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Asynchronously retrieves a single FeedbackExecutionWorkflow entity by its ID.
    /// </summary>
    /// <param name="workflowId">The ID of the FeedbackExecutionWorkflow entity to retrieve.</param>
    /// <param name="queryOptions">Query options for the retrieval.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the FeedbackExecutionWorkflow entity.</returns>
    ValueTask<FeedbackExecutionWorkflow?> GetByIdAsync(Guid workflowId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new FeedbackExecutionWorkflow entity.
    /// </summary>
    /// <param name="workflow">The FeedbackExecutionWorkflow entity to create.</param>
    /// <param name="commandOptions">Command options for the creation.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the created FeedbackExecutionWorkflow entity.</returns>
    ValueTask<FeedbackExecutionWorkflow> CreateAsync(FeedbackExecutionWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing FeedbackExecutionWorkflow entity.
    /// </summary>
    /// <param name="workflow">The FeedbackExecutionWorkflow entity to update.</param>
    /// <param name="commandOptions">Command options for the update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the updated FeedbackExecutionWorkflow entity.</returns>
    ValueTask<FeedbackExecutionWorkflow> UpdateAsync(FeedbackExecutionWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the specified FeedbackExecutionWorkflow entity.
    /// </summary>
    /// <param name="workflow">The FeedbackExecutionWorkflow entity to delete.</param>
    /// <param name="commandOptions">Command options for the deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the deleted FeedbackExecutionWorkflow entity.</returns>
    ValueTask<FeedbackExecutionWorkflow?> DeleteAsync(FeedbackExecutionWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the FeedbackExecutionWorkflow entity with the specified ID.
    /// </summary>
    /// <param name="workflowId">The ID of the FeedbackExecutionWorkflow entity to delete.</param>
    /// <param name="commandOptions">Command options for the deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the deleted FeedbackExecutionWorkflow entity.</returns>
    ValueTask<FeedbackExecutionWorkflow?> DeleteByIdAsync(Guid workflowId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}

using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository for managing feedback execution workflow entities.
/// </summary>
public interface IFeedbackAnalysisWorkflowRepository
{
    /// <summary>
    /// Retrieves a queryable collection of feedback execution workflow entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the workflows (optional).</param>
    /// <param name="queryOptions">Indicates whether the entities should be queried without tracking changes (default is false).</param>
    /// <returns>A queryable collection of feedback execution workflow entities.</returns>
    IQueryable<FeedbackAnalysisWorkflow> Get(Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Asynchronously retrieves a feedback execution workflow entity by its unique identifier.
    /// </summary>
    /// <param name="workflowId">The unique identifier of the workflow.</param>
    /// <param name="queryOptions">Indicates whether the entity should be queried without tracking changes (default is false).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the workflow entity, or null if not found.</returns>
    ValueTask<FeedbackAnalysisWorkflow?> GetByIdAsync(Guid workflowId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new feedback execution workflow entity.
    /// </summary>
    /// <param name="workflow">The workflow execution to be created.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The newly created execution workflow.</returns>
    ValueTask<FeedbackAnalysisWorkflow> CreateAsync(FeedbackAnalysisWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing feedback execution workflow entity.
    /// </summary>
    /// <param name="workflow">The workflow entity to update.</param>
    /// <param name="commandOptions">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the updated workflow entity.</returns>
    ValueTask<FeedbackAnalysisWorkflow> UpdateAsync(FeedbackAnalysisWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates feedback workflows in batch
    /// </summary>
    /// <param name="setPropertyCalls">Delegates to set updating property and value for it</param>
    /// <param name="batchUpdatePredicate">Filter to apply for batch update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of updated rows</returns>
    ValueTask<int> UpdateBatchAsync(
        Expression<Func<SetPropertyCalls<FeedbackAnalysisWorkflow>, SetPropertyCalls<FeedbackAnalysisWorkflow>>> setPropertyCalls,
        Expression<Func<FeedbackAnalysisWorkflow, bool>>? batchUpdatePredicate = default,
        CancellationToken cancellationToken = default
    );
    
    /// <summary>
    /// Deletes an existing workflow record.
    /// </summary>
    /// <param name="workflow">The workflow object to be deleted.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted workflow object if successful, otherwise null (e.g., if the workflow was not found).</returns>
    ValueTask<FeedbackAnalysisWorkflow?> DeleteAsync(FeedbackAnalysisWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously deletes a feedback execution workflow entity by its unique identifier.
    /// </summary>
    /// <param name="workflowId">The unique identifier of the workflow to delete.</param>
    /// <param name="commandOptions">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted workflow entity, or null if not found.</returns>
    ValueTask<FeedbackAnalysisWorkflow?> DeleteByIdAsync(Guid workflowId, CommandOptions commandOptions, CancellationToken cancellationToken = default);
}

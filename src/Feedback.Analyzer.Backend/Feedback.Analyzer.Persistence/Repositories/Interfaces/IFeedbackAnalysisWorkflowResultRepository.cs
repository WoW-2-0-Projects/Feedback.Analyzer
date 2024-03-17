using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for managing FeedbackAnalysisWorkflowResult entities in the repository.
/// </summary>
public interface IFeedbackAnalysisWorkflowResultRepository
{
    /// <summary>
    /// Retrieves a queryable collection of FeedbackAnalysisWorkflowResult entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the results (optional).</param>
    /// <param name="queryOptions">Indicates whether the entities should be queried without tracking changes (default is false).</param>
    /// <returns>A queryable collection of FeedbackAnalysisWorkflowResult entities.</returns>
    IQueryable<FeedbackAnalysisWorkflowResult> Get(Expression<Func<FeedbackAnalysisWorkflowResult, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Asynchronously retrieves a FeedbackAnalysisWorkflowResult entity by its unique identifier.
    /// </summary>
    /// <param name="resultId">The unique identifier of the FeedbackAnalysisWorkflowResult.</param>
    /// <param name="queryOptions">Indicates whether the entity should be queried without tracking changes (default is false).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the FeedbackAnalysisWorkflowResult entity, or null if not found.</returns>
    ValueTask<FeedbackAnalysisWorkflowResult?> GetByIdAsync(Guid resultId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new FeedbackAnalysisWorkflowResult in the repository.
    /// </summary>
    /// <param name="workflowResult">The FeedbackAnalysisWorkflowResult to be created.</param>
    /// <param name="commandOptions">Command options for the creation operation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation that yields the created FeedbackAnalysisWorkflowResult.</returns>
    ValueTask<FeedbackAnalysisWorkflowResult> CreateAsync(FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously updates an existing FeedbackAnalysisWorkflowResult entity.
    /// </summary>
    /// <param name="workflowResult">The FeedbackAnalysisWorkflowResult entity to update.</param>
    /// <param name="commandOptions">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the updated FeedbackAnalysisWorkflowResult entity.</returns>
    ValueTask<FeedbackAnalysisWorkflowResult> UpdateAsync(
        FeedbackAnalysisWorkflowResult workflowResult,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    );
    
    /// <summary>
    /// Deletes an existing FeedbackAnalysisWorkflowResult record.
    /// </summary>
    /// <param name="workflowResult">The FeedbackAnalysisWorkflowResult object to be deleted.</param>
    /// <param name="commandOptions">If true, automatically saves changes to the underlying data store. If false, additional changes can be made before saving.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>The deleted FeedbackAnalysisWorkflowResult object if successful, otherwise null (e.g., if the result was not found).</returns>
    ValueTask<FeedbackAnalysisWorkflowResult?> DeleteAsync(FeedbackAnalysisWorkflowResult workflowResult, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes a FeedbackAnalysisWorkflowResult entity by its unique identifier.
    /// </summary>
    /// <param name="resultId">The unique identifier of the FeedbackAnalysisWorkflowResult to delete.</param>
    /// <param name="commandOptions">Indicates whether changes should be saved to the underlying data store (default is true).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted FeedbackAnalysisWorkflowResult entity, or null if not found.</returns>
    ValueTask<FeedbackAnalysisWorkflowResult?> DeleteByIdAsync(Guid resultId, CommandOptions commandOptions, CancellationToken cancellationToken = default);
}
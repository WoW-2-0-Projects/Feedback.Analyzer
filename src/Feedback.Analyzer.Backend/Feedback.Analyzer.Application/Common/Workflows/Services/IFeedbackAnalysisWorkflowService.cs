using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Workflows.Services;

/// <summary>
/// Interface for managing FeedbackAnalysisWorkflow entities.
/// </summary>
public interface IFeedbackAnalysisWorkflowService
{
    /// <summary>
    /// Retrieves a collection of FeedbackAnalysisWorkflow entities based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">The predicate to filter the FeedbackAnalysisWorkflow entities.</param>
    /// <param name="queryOptions">Query options for the retrieval.</param>
    /// <returns>A collection of FeedbackAnalysisWorkflow entities.</returns>
    IQueryable<FeedbackAnalysisWorkflow> Get(Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Asynchronously retrieves a single FeedbackAnalysisWorkflow entity by its ID.
    /// </summary>
    /// <param name="workflowId">The ID of the FeedbackAnalysisWorkflow entity to retrieve.</param>
    /// <param name="queryOptions">Query options for the retrieval.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the FeedbackAnalysisWorkflow entity.</returns>
    ValueTask<FeedbackAnalysisWorkflow?> GetByIdAsync(Guid workflowId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously creates a new FeedbackAnalysisWorkflow entity.
    /// </summary>
    /// <param name="workflow">The FeedbackAnalysisWorkflow entity to create.</param>
    /// <param name="commandOptions">Command options for the creation.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the created FeedbackAnalysisWorkflow entity.</returns>
    ValueTask<FeedbackAnalysisWorkflow> CreateAsync(FeedbackAnalysisWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing FeedbackAnalysisWorkflow entity.
    /// </summary>
    /// <param name="workflow">The FeedbackAnalysisWorkflow entity to update.</param>
    /// <param name="commandOptions">Command options for the update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the updated FeedbackAnalysisWorkflow entity.</returns>
    ValueTask<FeedbackAnalysisWorkflow> UpdateAsync(FeedbackAnalysisWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the specified FeedbackAnalysisWorkflow entity.
    /// </summary>
    /// <param name="workflow">The FeedbackAnalysisWorkflow entity to delete.</param>
    /// <param name="commandOptions">Command options for the deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the deleted FeedbackAnalysisWorkflow entity.</returns>
    ValueTask<FeedbackAnalysisWorkflow?> DeleteAsync(FeedbackAnalysisWorkflow workflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the FeedbackAnalysisWorkflow entity with the specified ID.
    /// </summary>
    /// <param name="workflowId">The ID of the FeedbackAnalysisWorkflow entity to delete.</param>
    /// <param name="commandOptions">Command options for the deletion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the operation and containing the deleted FeedbackAnalysisWorkflow entity.</returns>
    ValueTask<FeedbackAnalysisWorkflow?> DeleteByIdAsync(Guid workflowId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}

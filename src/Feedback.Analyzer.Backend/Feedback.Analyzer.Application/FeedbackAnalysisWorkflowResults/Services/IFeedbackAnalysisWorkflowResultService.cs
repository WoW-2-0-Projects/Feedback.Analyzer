using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;

/// <summary>
/// Interface for managing FeedbackAnalysisWorkflowResult entities.
/// </summary>
public interface IFeedbackAnalysisWorkflowResultService
{
    /// <summary>
    /// Retrieves a queryable collection of FeedbackAnalysisWorkflowResult entities based on the provided predicate and query options.
    /// </summary>
    /// <param name="predicate">An optional predicate to filter the FeedbackAnalysisWorkflowResult entities.</param>
    /// <param name="queryOptions">Query options for sorting, pagination, etc.</param>
    /// <returns>A queryable collection of FeedbackAnalysisWorkflowResult entities.</returns>
    IQueryable<FeedbackAnalysisWorkflowResult> Get(Expression<Func<FeedbackAnalysisWorkflowResult, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a FeedbackAnalysisWorkflowResult entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="workflowResultId">The unique identifier of the FeedbackAnalysisWorkflowResult entity to retrieve.</param>
    /// <param name="queryOptions">Query options for sorting, pagination, etc.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation that returns the FeedbackAnalysisWorkflowResult entity.</returns>
    ValueTask<FeedbackAnalysisWorkflowResult?> GetByIdAsync(Guid workflowResultId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new FeedbackAnalysisWorkflowResult entity asynchronously.
    /// </summary>
    /// <param name="workflowResult">The FeedbackAnalysisWorkflowResult entity to create.</param>
    /// <param name="commandOptions">Command options for the create operation.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation that returns the created FeedbackAnalysisWorkflowResult entity.</returns>
    ValueTask<FeedbackAnalysisWorkflowResult> CreateAsync(FeedbackAnalysisWorkflowResult workflowResult, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a FeedbackAnalysisWorkflowResult entity asynchronously.
    /// </summary>
    /// <param name="workflowResult">The FeedbackAnalysisWorkflowResult entity to update.</param>
    /// <param name="commandOptions">Command options for the update operation.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation that returns the updated FeedbackAnalysisWorkflowResult entity.</returns>
    ValueTask<FeedbackAnalysisWorkflowResult> UpdateAsync(FeedbackAnalysisWorkflowResult workflowResult, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a FeedbackAnalysisWorkflowResult entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="workflowResultId">The unique identifier of the FeedbackAnalysisWorkflowResult entity to delete.</param>
    /// <param name="commandOptions">Command options for the delete operation.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask<FeedbackAnalysisWorkflowResult?> DeleteByIdAsync(Guid workflowResultId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

}
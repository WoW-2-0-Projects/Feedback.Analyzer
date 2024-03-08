using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

public interface IFeedbackAnalysisWorkflowRepository
{
    /// <summary>
    /// Retrieves FeedbackAnalysisWorkflow entities based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter the entities.</param>
    /// <param name="queryOptions">Options for customizing the query behavior.</param>
    /// <returns>An IQueryable of FeedbackAnalysisWorkflow entities.</returns>
    public IQueryable<FeedbackAnalysisWorkflow> Get(Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves an FeedbackAnalysisWorkflow entity asynchronously by its unique identifier.
    /// </summary>
    /// <param name="workflowId">The unique identifier of the FeedbackAnalysisWorkflow entity.</param>
    /// <param name="queryOptions">Options for customizing the query behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the retrieved FeedbackAnalysisWorkflow entity.</returns>
    public ValueTask<FeedbackAnalysisWorkflow> GetByIdAsync(Guid workflowId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new FeedbackAnalysisWorkflow entity.
    /// </summary>
    /// <param name="feedbackAnalysisWorkflow">The FeedbackAnalysisWorkflow entity to be created.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the newly created FeedbackAnalysisWorkflow entity.</returns>
    public ValueTask<FeedbackAnalysisWorkflow> CreateAsync(FeedbackAnalysisWorkflow feedbackAnalysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing FeedbackAnalysisWorkflow entity.
    /// </summary>
    /// <param name="feedbackAnalysisWorkflow">The FeedbackAnalysisWorkflow entity to be updated.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the updated FeedbackAnalysisWorkflow entity.</returns>
    public ValueTask<FeedbackAnalysisWorkflow> UpdateAsync(FeedbackAnalysisWorkflow feedbackAnalysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the specified FeedbackAnalysisWorkflow entity.
    /// </summary>
    /// <param name="feedbackAnalysisWorkflow">The FeedbackAnalysisWorkflow entity to be deleted.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the deleted FeedbackAnalysisWorkflow entity.</returns>
    public ValueTask<FeedbackAnalysisWorkflow> DeleteAsync(FeedbackAnalysisWorkflow feedbackAnalysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the FeedbackAnalysisWorkflow entity with the specified identifier.
    /// </summary>
    /// <param name="workflowId">The unique identifier of the FeedbackAnalysisWorkflow entity to be deleted.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the deleted FeedbackAnalysisWorkflow entity.</returns>
    public ValueTask<FeedbackAnalysisWorkflow> DeleteByIdAsync(Guid workflowId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}
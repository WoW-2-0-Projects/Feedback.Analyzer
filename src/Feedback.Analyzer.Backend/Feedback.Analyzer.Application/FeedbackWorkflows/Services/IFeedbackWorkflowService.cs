using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackWorkflows.Services;

/// <summary>
/// Represents a service interface for managing feedback workflows.
/// </summary>
public interface IFeedbackWorkflowService
{
    /// <summary>
    /// Retrieves FeedbackWorkflow entities based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter the entities.</param>
    /// <param name="queryOptions">Options for customizing the query behavior.</param>
    /// <returns>An IQueryable of FeedbackWorkflow entities.</returns>
    IQueryable<FeedbackWorkflow> Get(Expression<Func<FeedbackWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Retrieves FeedbackWorkflow entities based on the provided filter and query options.
    /// </summary>
    /// <param name="feedbackWorkflowFilter">The filter criteria for retrieving FeedbackWorkflow entities.</param>
    /// <param name="queryOptions">Options for customizing the query behavior.</param>
    /// <returns>An IQueryable of FeedbackWorkflow entities.</returns>
    IQueryable<FeedbackWorkflow> Get(FeedbackWorkflowFilter feedbackWorkflowFilter, QueryOptions queryOptions = default);

    
    /// <summary>
    /// Retrieves a FeedbackWorkflow entity asynchronously by its unique identifier.
    /// </summary>
    /// <param name="feedbackWorkflowId">The unique identifier of the FeedbackWorkflow entity to retrieve.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the retrieved FeedbackWorkflow entity.</returns>
    ValueTask<FeedbackWorkflow> GetByIdAsync(Guid feedbackWorkflowId, QueryOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new FeedbackWorkflow entity.
    /// </summary>
    /// <param name="feedbackWorkflow">The FeedbackWorkflow entity to be created.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the newly created FeedbackWorkflow entity.</returns>
    ValueTask<FeedbackWorkflow> CreateAsync(FeedbackWorkflow feedbackWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates an existing FeedbackWorkflow entity.
    /// </summary>
    /// <param name="feedbackWorkflow">The FeedbackWorkflow entity to be updated.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the updated FeedbackWorkflow entity.</returns>
    ValueTask<FeedbackWorkflow> UpdateAsync(FeedbackWorkflow feedbackWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes a FeedbackWorkflow entity by its unique identifier.
    /// </summary>
    /// <param name="feedbackWorkflowId">The unique identifier of the FeedbackWorkflow entity to delete.</param>
    /// <param name="commandOptions">Options for customizing the command behavior.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, indicating the success of the deletion.</returns>
    ValueTask<FeedbackWorkflow> DeleteByIdAsync(Guid feedbackWorkflowId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}
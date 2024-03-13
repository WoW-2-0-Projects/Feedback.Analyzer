using System.Linq.Expressions;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Represents a service interface for managing feedback analysis workflows.
/// </summary>
public interface IFeedbackAnalysisWorkflowService
{
    /// <summary>
    /// Retrieves feedback analysis workflows based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">The predicate used to filter the feedback analysis workflows (optional).</param>
    /// <param name="queryOptions">The query options for controlling the retrieval process (optional).</param>
    /// <returns>An IQueryable of feedback analysis workflows.</returns>
    IQueryable<FeedbackAnalysisWorkflow> Get(
        Expression<Func<FeedbackAnalysisWorkflow, bool>>? predicate = default, 
        QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves feedback analysis workflows based on the specified filter and query options.
    /// </summary>
    /// <param name="feedbackAnalysisWorkflowFilter">The filter criteria for retrieving feedback analysis workflows.</param>
    /// <param name="queryOptions">The query options for controlling the retrieval process (optional).</param>
    /// <returns>An IQueryable of feedback analysis workflows.</returns>
    IQueryable<FeedbackAnalysisWorkflow> Get(
        FeedbackAnalysisWorkflowFilter feedbackAnalysisWorkflowFilter,
        QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a feedback analysis workflow by its ID asynchronously.
    /// </summary>
    /// <param name="feedbackAnalysisWorkflowId">The ID of the feedback analysis workflow to retrieve.</param>
    /// <param name="queryOptions">The query options for controlling the retrieval process (optional).</param>
    /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A ValueTask representing the asynchronous operation, containing the retrieved feedback analysis workflow.</returns>
    ValueTask<FeedbackAnalysisWorkflow> GetByIdAsync(
        Guid feedbackAnalysisWorkflowId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new feedback analysis workflow asynchronously.
    /// </summary>
    /// <param name="feedbackAnalysisWorkflow">The feedback analysis workflow to create.</param>
    /// <param name="commandOptions">The command options for controlling the creation process (optional).</param>
    /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A ValueTask representing the asynchronous operation, containing the created feedback analysis workflow.</returns>
    ValueTask<FeedbackAnalysisWorkflow> CreateAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a feedback analysis workflow asynchronously.
    /// </summary>
    /// <param name="feedbackAnalysisWorkflow">The feedback analysis workflow to delete.</param>
    /// <param name="commandOptions">The command options for controlling the delete process (optional).</param>
    /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A ValueTask representing the asynchronous operation, containing the result of the delete operation.</returns>
    ValueTask<FeedbackAnalysisWorkflow> DeleteAsync(
        FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a feedback analysis workflow by its ID asynchronously.
    /// </summary>
    /// <param name="feedbackAnalysisWorkflowId">The ID of the feedback analysis workflow to delete.</param>
    /// <param name="commandOptions">The command options for controlling the delete process (optional).</param>
    /// <param name="cancellationToken">The cancellation token to cancel the asynchronous operation (optional).</param>
    /// <returns>A ValueTask representing the asynchronous operation, containing the deleted feedback analysis workflow.</returns>
    ValueTask<FeedbackAnalysisWorkflow> DeleteByIdAsync(
        Guid feedbackAnalysisWorkflowId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
using System.Linq.Expressions;
using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.AnalysisWorkflows.Services;

/// <summary>
/// Represents a service interface for managing analysis workflows.
/// </summary>
public interface IAnalysisWorkflowService
{
    /// <summary>
    /// Retrieves analysis workflows based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter analysis workflows based on specific conditions.</param>
    /// <param name="queryOptions">Query options to specify how the analysis workflows should be retrieved.</param>
    /// <returns>An IQueryable of AnalysisWorkflow objects.</returns>
    IQueryable<AnalysisWorkflow> Get(Expression<Func<AnalysisWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default);


    /// <summary>
    /// Retrieves analysis workflows based on the specified filter and query options.
    /// </summary>
    /// <param name="analysisWorkflowFilter">The filter to apply when retrieving analysis workflows.</param>
    /// <param name="queryOptions">Query options to specify how the analysis workflows should be retrieved.</param>
    /// <returns>An IQueryable of AnalysisWorkflow objects.</returns>
    IQueryable<AnalysisWorkflow> Get(AnalysisWorkflowFilter analysisWorkflowFilter, QueryOptions queryOptions);

    
    /// <summary>
    /// Asynchronously retrieves an analysis workflow by its ID.
    /// </summary>
    /// <param name="analysisWorkflowId">The ID of the analysis workflow to retrieve.</param>
    /// <param name="queryOptions">Query options to specify how the analysis workflow should be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the retrieved AnalysisWorkflow.</returns>
    ValueTask<AnalysisWorkflow> GetByIdAsync(Guid analysisWorkflowId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);


    /// <summary>
    /// Asynchronously creates a new analysis workflow.
    /// </summary>
    /// <param name="analysisWorkflow">The analysis workflow object to create.</param>
    /// <param name="commandOptions">Command options to specify how the creation operation should be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the created AnalysisWorkflow.</returns>
    ValueTask<AnalysisWorkflow> CreateAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);


    /// <summary>
    /// Asynchronously updates an existing analysis workflow.
    /// </summary>
    /// <param name="analysisWorkflow">The analysis workflow object to update.</param>
    /// <param name="commandOptions">Command options to specify how the update operation should be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated AnalysisWorkflow.</returns>
    ValueTask<AnalysisWorkflow> UpdateAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);


    /// <summary>
    /// Asynchronously deletes an existing analysis workflow.
    /// </summary>
    /// <param name="analysisWorkflow">The analysis workflow object to delete.</param>
    /// <param name="commandOptions">Command options to specify how the deletion operation should be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the deleted AnalysisWorkflow.</returns>
    ValueTask<AnalysisWorkflow> DeleteAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Asynchronously deletes an existing analysis workflow by its ID.
    /// </summary>
    /// <param name="analysisWorkflowId">The analysis workflow object representing the ID of the analysis workflow to delete.</param>
    /// <param name="commandOptions">Command options to specify how the deletion operation should be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the deleted AnalysisWorkflow.</returns>
    ValueTask<AnalysisWorkflow> DeleteByIdAsync(Guid analysisWorkflowId , CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}
using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository interface for managing analysis workflows.
/// </summary>
public interface IAnalysisWorkflowRepository
{
    /// <summary>
    /// Retrieves analysis workflows based on the specified predicate and query options.
    /// </summary>
    /// <param name="predicate">Optional predicate to filter analysis workflows based on organization properties.</param>
    /// <param name="queryOptions">Query options to specify how the analysis workflows should be queried.</param>
    /// <returns>An IQueryable of AnalysisWorkflow objects.</returns>
    IQueryable<AnalysisWorkflow> Get(Expression<Func<AnalysisWorkflow, bool>>? predicate = default, QueryOptions queryOptions = default);
    
    /// <summary>
    /// Retrieves an analysis workflow by its ID asynchronously.
    /// </summary>
    /// <param name="analysisWorkflowId">The ID of the analysis workflow to retrieve.</param>
    /// <param name="queryOptions">Query options to specify how the analysis workflow should be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the retrieved AnalysisWorkflow or null if not found.</returns>
    ValueTask<AnalysisWorkflow?> GetByIdAsync(Guid analysisWorkflowId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    
    /// <summary>
    /// Creates a new analysis workflow asynchronously.
    /// </summary>
    /// <param name="analysisWorkflow">The analysis workflow object to create.</param>
    /// <param name="commandOptions">Command options to specify how the creation operation should be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the created AnalysisWorkflow.</returns>
    ValueTask<AnalysisWorkflow> CreateAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    
    /// <summary>
    /// Updates an existing analysis workflow asynchronously.
    /// </summary>
    /// <param name="analysisWorkflow">The analysis workflow object to update.</param>
    /// <param name="commandOptions">Command options to specify how the update operation should be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated AnalysisWorkflow.</returns>
    ValueTask<AnalysisWorkflow> UpdateAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    
    /// <summary>
    /// Deletes an analysis workflow asynchronously.
    /// </summary>
    /// <param name="analysisWorkflow">The analysis workflow object to delete.</param>
    /// <param name="commandOptions">Command options to specify how the deletion operation should be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the deleted AnalysisWorkflow or null if not found.</returns>
    ValueTask<AnalysisWorkflow?> DeleteAsync(AnalysisWorkflow analysisWorkflow, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    
    /// <summary>
    /// Deletes an analysis workflow by its ID asynchronously.
    /// </summary>
    /// <param name="analysisWorkflowId">The ID of the analysis workflow to delete.</param>
    /// <param name="commandOptions">Command options to specify how the deletion operation should be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, returning the deleted AnalysisWorkflow or null if not found.</returns>
    ValueTask<AnalysisWorkflow?> DeleteByIdAsync(Guid analysisWorkflowId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}
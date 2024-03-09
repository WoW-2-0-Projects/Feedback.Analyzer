using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines workflow execution options repository functionality
/// </summary>
public interface IWorkflowExecutionOptionRepository
{
    /// <summary>
    /// Queries an workflow execution options with all grand children and references
    /// </summary>
    /// <param name="optionId">Id of workflow execution options</param>
    /// <param name="queryOptions">Indicates whether the entity should be queried without tracking changes (default is false).</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution workflow with all included categories and children options if found, otherwise null</returns>
    ValueTask<WorkflowExecutionOption?> GetByIdAndIncludeAllGrandChildrenAsync(
        Guid optionId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    );
}
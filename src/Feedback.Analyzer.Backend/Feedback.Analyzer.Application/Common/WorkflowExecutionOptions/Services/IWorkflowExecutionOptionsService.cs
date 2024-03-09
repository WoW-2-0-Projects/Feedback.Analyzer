using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.WorkflowExecutionOptions.Services;

/// <summary>
/// Defines workflow execution options service.
/// </summary>
public interface IWorkflowExecutionOptionsService
{
    /// <summary>
    /// Gets workflow execution options for the given execution options Id.
    /// </summary>
    /// <param name="executionOptionsId">Id of workflow execution options</param>
    /// <param name="queryOptions">Query options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution workflow with all included categories and children options if found, otherwise null</returns>
    ValueTask<WorkflowExecutionOption?> GetByIdForExecutionAsync(
        Guid executionOptionsId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    );
}
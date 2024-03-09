using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.AnalysisWorkflowExecutionOptions.Services;

/// <summary>
/// Defines workflow execution options service.
/// </summary>
public interface IWorkflowExecutionOptionsService
{
    /// <summary>
    /// Gets workflow execution options for the given execution options Id.
    /// </summary>
    /// <param name="executionOptionsId">Id of workflow execution options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution workflow with all included categories and children options if found, otherwise null</returns>
    ValueTask<WorkflowExecutionOptions?> GetByIdForExecutionAsync(Guid executionOptionsId, CancellationToken cancellationToken = default);
}
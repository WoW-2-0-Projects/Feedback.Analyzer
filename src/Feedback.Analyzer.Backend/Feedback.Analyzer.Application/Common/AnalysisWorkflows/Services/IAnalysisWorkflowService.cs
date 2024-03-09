using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Common.AnalysisWorkflows.Services;

/// <summary>
/// Interface for managing FeedbackAnalysisWorkflow entities.
/// </summary>
public interface IAnalysisWorkflowService
{
    /// <summary>
    /// Updates workflow status
    /// </summary>
    /// <param name="workflowId">Id of workflow to update</param>
    /// <param name="status">New status</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of update operation, true if succeeded, otherwise false</returns>
    ValueTask<bool> UpdateStatus(Guid workflowId, WorkflowStatus status, CancellationToken cancellationToken = default);
}

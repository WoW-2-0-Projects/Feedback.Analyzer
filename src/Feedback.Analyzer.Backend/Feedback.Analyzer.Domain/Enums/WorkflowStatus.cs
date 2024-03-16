namespace Feedback.Analyzer.Domain.Enums;

/// <summary>
/// Define an enumeration type to represent different statuses of a workflow.
/// </summary>
public enum WorkflowStatus
{
    /// <summary>
    /// Represents that the workflow is queued for execution.
    /// </summary>
    Queued,
    
    /// <summary>
    /// Represents that the workflow is currently running.
    /// </summary>
    Running,
    
    /// <summary>
    /// Represents that the workflow has successfully completed.
    /// </summary>
    Completed,
    
    /// <summary>
    /// Represents that the workflow has failed to complete.
    /// </summary>
    Failed
}
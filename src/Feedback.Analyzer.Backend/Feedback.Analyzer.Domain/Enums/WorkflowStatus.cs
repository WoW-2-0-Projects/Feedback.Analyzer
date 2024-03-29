namespace Feedback.Analyzer.Domain.Enums;

/// <summary>
/// Define an enumeration type to represent different statuses of a workflow.
/// </summary>
public enum WorkflowStatus
{
    /// <summary>
    /// Represents that the workflow is queued for execution.
    /// </summary>
    Queued = 0,
    
    /// <summary>
    /// Represents that the workflow is currently running.
    /// </summary>
    Running = 1,
    
    /// <summary>
    /// Represents that the workflow has successfully completed.
    /// </summary>
    Completed = 2,
    
    /// <summary>
    /// Represents that the workflow has failed to complete.
    /// </summary>
    Failed = 3
}
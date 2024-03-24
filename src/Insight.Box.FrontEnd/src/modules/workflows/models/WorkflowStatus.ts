/*
 * Defines an enumeration type to represent different statuses of a workflow.
 */
export enum WorkflowStatus {
    /*
     * Represents that the workflow is queued for execution.
     */
    Queued = 0,

    /*
     * Represents that the workflow is currently running.
     */
    Running = 1,

    /*
     * Represents that the workflow has successfully completed.
     */
    Completed = 2,

    /*
     * Represents that the workflow has failed to complete.
     */
    Failed = 3
}
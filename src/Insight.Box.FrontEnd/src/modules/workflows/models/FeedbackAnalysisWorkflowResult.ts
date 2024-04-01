/**
 * Represents feedback analysis workflow result
 */
export class FeedbackAnalysisWorkflowResult {
    /**
     * Result Id
     */
    public id!: string;

    /**
     * Workflow Id
     */
    public workflowId!: string;

    /**
     * The start time of the feedback analysis workflow.
     */
    public startedTime!: Date;

    /**
     * The finish time of the feedback analysis workflow.
     */
    public finishedTime?: Date;
}
/**
 * Represents the different types of feedback execution workflows.
 */
export enum WorkflowType {

    /*
     * Refers to a template workflow.
     */
    Template = 0,

    /*
     * Refers to a training workflow.
     */
    Training = 1,

    /*
     * Refers to inference workflow
     */
    Inference = 2,
}

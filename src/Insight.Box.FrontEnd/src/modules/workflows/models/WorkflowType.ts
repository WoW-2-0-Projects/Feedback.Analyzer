/**
 * Represents the different types of feedback execution workflows.
 */
export enum WorkflowType {

    /*
     * Refers to a template workflow.
     */
    Template = 'Template',

    /*
     * Refers to a training workflow.
     */
    Training = 'Training',

    /*
     * Refers to inference workflow
     */
    Inference = 'Inference'
}

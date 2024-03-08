/*
 * Represents training data for category
 */
export class CategoryTrainingData {

    /*
     * Category id
     */
    public categoryId: string;

    /*
     * Training workflow id
     */
    public workflowId: string;

    constructor(categoryId: string, trainingWorkflowId: string) {
        this.categoryId = categoryId;
        this.workflowId = trainingWorkflowId;
    }
}
import {FilterPagination} from "@/common/FilterPagination";

/*
 * Represents workflow results filter
 */
export class FeedbackAnalysisWorkflowResultFilter extends  FilterPagination {

    /*
     * Workflow id
     */
    public workflowId: string;

    constructor(workflowId: string) {
        super();
        this.pageSize = 5;
        this.workflowId = workflowId;
    }
}
import {FilterPagination} from "@/common/FilterPagination";

/*
 * Represents workflow results filter
 */
export class FeedbackAnalysisResultFilter extends  FilterPagination {

    /*
     * Workflow id
     */
    public workflowId: string;

    /*
     * Result id
     */
    public resultId: string;

    constructor(workflowId: string, resultId: string) {
        super();
        this.pageSize = 5;
        this.workflowId = workflowId;
        this.resultId = resultId;

        this.searchKeyword = '';
    }
}
import {FilterPagination} from "@/common/FilterPagination";

/*
 * Represents workflow results filter
 */
export class FeedbackAnalysisWorkflowResultsFilter extends  FilterPagination {

    /*
     * Workflow id
     */
    public workflowId: string;

    constructor() {
        super();
        this.pageSize = 5;

        this.searchKeyword = '';
    }
}
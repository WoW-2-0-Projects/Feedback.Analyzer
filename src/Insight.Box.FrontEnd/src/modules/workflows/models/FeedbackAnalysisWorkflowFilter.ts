import type {WorkflowType} from "@/modules/workflows/models/WorkflowType";
import {FilterPagination} from "@/common/FilterPagination";

/*
 * Represents workflows filter
 */
export class FeedbackAnalysisWorkflowFilter extends  FilterPagination {

    /*
     * Workflow type
     */
    public type: WorkflowType;

    /*
    * Search keyword
    */
    public searchKeyword : string;

    constructor() {
        super();

        this.searchKeyword = '';
    }
}
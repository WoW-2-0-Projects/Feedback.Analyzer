import {FilterPagination} from "@/infrastructure/models/query/FilterPagination";
import {WorkflowType} from "@/modules/prompts/models/WorkflowType";

/*
 * Represents product filter
 */
export class AnalysisWorkflowFilter extends FilterPagination {

    /*
     * Search keyword
     */
    public type!: WorkflowType

    constructor(type = WorkflowType.Template, pageSize = 20, pageToken = 1) {
        super();

        this.type = type;
        this.pageSize = pageSize;
        this.pageToken = pageToken
    }
}
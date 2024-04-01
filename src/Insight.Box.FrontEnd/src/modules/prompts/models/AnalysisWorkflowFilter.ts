import {FilterPagination} from "@/infrastructure/models/query/FilterPagination";
import {WorkflowType} from "@/modules/workflows/models/WorkflowType";

/*
 * Represents product filter
 */
export class AnalysisWorkflowFilter extends FilterPagination {

    /*
     * Workflow type
     */
    public type!: WorkflowType

    constructor(type = WorkflowType.Template, pageSize = 20, pageToken = 1) {
        super();

        this.type = type;
        this.pageSize = pageSize;
        this.pageToken = pageToken
    }
}
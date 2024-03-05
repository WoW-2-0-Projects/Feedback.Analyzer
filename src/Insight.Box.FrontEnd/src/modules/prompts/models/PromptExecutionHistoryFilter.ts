import {FilterPagination} from "@/infrastructure/models/query/FilterPagination";


export class PromptExecutionHistoryFilter extends  FilterPagination {
    /*
     * Search keyword
     */
    public promptId: string;

    constructor(pageSize = 20, pageToken = 1) {
        super();

        this.pageSize = pageSize;
        this.pageToken = pageToken;
    }
}
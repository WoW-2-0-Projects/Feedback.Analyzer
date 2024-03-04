import {FilterPagination} from "@/infrastructure/models/query/FilterPagination";

export class PromptExecutionResultFilter extends  FilterPagination {
    /*
     * Search keyword
     */
    public searchKeyword: string = '';

    constructor(searchKeyword = '', pageSize = 20, pageToken = 1) {
        super();

        this.searchKeyword = searchKeyword;
        this.pageSize = pageSize;
        this.pageToken = pageToken;
    }
}
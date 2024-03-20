import {FilterPagination} from "@/infrastructure/models/query/FilterPagination";

/*
 * Represents product filter
 */
export class PromptCategoryFilter extends  FilterPagination {

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
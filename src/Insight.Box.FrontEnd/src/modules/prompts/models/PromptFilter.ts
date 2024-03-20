import {FilterPagination} from "@/common/FilterPagination";
import {withKeys} from "vue";

/*
 * Represents prompt filter
 */
export  class  PromptFilter  extends FilterPagination {

    /*
     * Search keyword
     */
    public searchKeyword: string = '';

    constructor(searchKeyword = '', pageSize = 20, pageToken  = 1) {
        super();

        this.searchKeyword = searchKeyword;
        this.pageSize = pageSize;
        this.pageToken = pageToken;
    }
}

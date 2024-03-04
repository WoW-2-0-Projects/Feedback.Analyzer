/*
 * Represents product filter
 */
import {FilterPagination} from "@/common/FilterPagination";

/*
 * Represents organization filter
 */
export class OrganizationFilter extends  FilterPagination {

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
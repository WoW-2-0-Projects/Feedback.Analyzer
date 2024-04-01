/*
 * Represents query pagination
 */
export class FilterPagination {
    /*
     * Page size
     */
    public pageSize: number;

    /*
     * Page token
     */
    public pageToken: number;

    constructor(pageSize = 20, pageToken = 1) {
        this.pageSize = pageSize;
        this.pageToken = pageToken;
    }
}
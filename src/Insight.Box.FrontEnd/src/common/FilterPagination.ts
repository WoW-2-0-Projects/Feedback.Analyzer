/*
 * Represents query filter pagination
 */
export class FilterPagination {

    /*
     * Represents query filter pagination
    */
    public pageSize: number;

    /*
     * Represents query filter pagination
     */
    public pageToken: number;

    constructor(pageSize = 20, pageToken = 1) {
        this.pageSize = pageSize;
        this.pageToken = pageToken;
    }
}
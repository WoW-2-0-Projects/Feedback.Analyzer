import type {FilterPagination} from "@/infrastructure/models/query/FilterPagination";

/*
 * Represents generic query model
 */
export class Query<TFilter extends FilterPagination> {
    /*
     * Filter
     */
    public filter: TFilter;

    constructor(filter: TFilter) {
        this.filter = filter;
    }
}
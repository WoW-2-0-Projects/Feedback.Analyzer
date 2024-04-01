import {FilterPagination} from "@/common/FilterPagination";

/*
 * Represents workflow results filter
 */
export class FeedbackAnalysisResultFilter extends  FilterPagination {

    /*
     * Result id
     */
    public resultId!: string;

    constructor(resultId: string) {
        super();
        this.pageSize = 5;
        this.resultId = resultId;
    }
}
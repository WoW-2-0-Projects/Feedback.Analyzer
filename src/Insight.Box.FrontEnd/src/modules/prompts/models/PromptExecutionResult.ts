import type {ITableRow} from "@/common/components/appTable/ITableRow";
import {TableRowData} from "@/common/components/appTable/TableRowData";
import {ApiResponse} from "@/infrastructure/apiClients/apiClientBase/ApiResponse";

/*
 * Represents a prompt execution result.
 */
export class PromptExecutionResult implements ITableRow {

    /*
     * Prompt Id
     */
    public promptId!: string;

    /*
     * Prompt version
     */
    public version!: number;

    /*
     * Prompt revision
     */
    public revision!: number;

    /*
     * Prompt executions count
     */
    public executionsCount!: number;

    /*
     * Average execution time
     */
    public averageExecutionTime!: number;

    /*
     * Prompt average accuracy
     */
    public averageAccuracy!: number;

    public mapToTableRow() {
        return new TableRowData([
            `${this.version}.${this.revision}`,
            this.averageExecutionTime.toString(),
            this.averageAccuracy.toString(),
            this.executionsCount.toString(),
        ], []);
    }
}



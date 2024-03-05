import type {ITableRow} from "@/common/components/appTable/ITableRow";
import {TableRowData} from "@/common/components/appTable/TableRowData";
import {ApiResponse} from "@/infrastructure/apiClients/apiClientBase/ApiResponse";

export class PromptExecutionResult implements ITableRow {
    promptId: string;
    version: number;
    revision: number;
    executionsCount: number;
    averageExecutionTime: number;
    averageAccuracy: number;

    public mapToTableRow() {
        new TableRowData([
            `${this.version}.${this.revision}`,
            this.averageExecutionTime,
            this.averageAccuracy,
            this.executionsCount,  // Comma added here
        ]);
    }
}



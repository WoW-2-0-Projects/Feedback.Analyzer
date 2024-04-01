/*
 * Represents the data of the table
 */
import type {ParameterizedAction} from "@/infrastructure/models/delegates/ParameterizedAction";
import {TableAction} from "@/common/components/appTable/TableAction";

export class TableRowData {

    /*
     * Table headers
     */
    public data: Array<string>;

    /*
     * Row actions
     */
    public actions: Array<TableAction>;

    constructor(data: Array<string>, actions: Array<TableAction>) {
        this.data = data;
        this.actions = actions;
    }
}
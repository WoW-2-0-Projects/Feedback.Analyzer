/*
 * Represents the data of the table
 */
import type {Action} from "@/infrastructure/models/delegates/Action";
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
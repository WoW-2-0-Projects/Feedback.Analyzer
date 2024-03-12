/*
 * Represents the data of the table
 */
import type {TableRowData} from "@/common/components/appTable/TableRowData";

export class TableData {

    /*
     * Table headers
     */
    public headers: Array<string>;

    /*
     Table rows
     */
    public rows: Array<TableRowData>;

    constructor(headers: Array<string>, rows: Array<TableRowData>) {
        this.headers = headers;
        this.rows = rows;
    }
}
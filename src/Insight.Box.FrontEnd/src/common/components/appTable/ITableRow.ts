import type {TableRowData} from "@/common/components/appTable/TableRowData";

export interface ITableRow {

    mapToTableRow(): TableRowData;
}
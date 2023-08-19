import { TableColumn } from '@swimlane/ngx-datatable';

// Generic ngx-datatable
export interface DataTableTemplateEntry {
    [key: string]: string ;
}

export interface DataTableRow {
    [key: string]: number | string | string[] | DataTableTemplateEntry | DataTableTemplateEntry[];
}

export interface DataTableColumn extends TableColumn {
    // use innerTemplate instead of cellTemplate
    // cellTemplate is over-written by DataTableComponent that will wrap innerTemplate in it
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    innerTemplate?: any;
    // a function to get the router link from the row
    getLink?: (row: DataTableRow) => string;
}

// Scrolling
export interface DataTableScroll {
    offsetX: number;
    offsetY: number;
}

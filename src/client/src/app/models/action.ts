import { DataTableRow } from ".";

export interface ActionRow extends DataTableRow {
    allowed: number
    action: string;
}

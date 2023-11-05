import { DataTableRow } from ".";
import { Guid } from "guid-typescript";

export interface Industry {
    id: Guid;
    name: string;
}

export interface IndustryRow extends DataTableRow {
    id: string;
    internalId: string;
    name: string;
}

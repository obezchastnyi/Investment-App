import { DataTableRow } from ".";
import { Guid } from "guid-typescript";

export interface Criteria {
    id: Guid;
    name: string;
}

export interface CriteriaRow extends DataTableRow {
    id: string;
    internalId: string;
    name: string;
}

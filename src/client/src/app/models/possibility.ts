import { DataTableRow } from ".";
import { Guid } from "guid-typescript";

export interface Possibility {
    id: Guid
    rate: number;
}

export interface PossibilityRow extends DataTableRow {
    id: string,
    internalId: string;
    rate: number;
}

import { DataTableRow } from ".";
import { Guid } from "guid-typescript";

export interface Portfolio {
    id: Guid
    name: string;
    sum: number;
}

export interface PortfolioRow extends DataTableRow {
    id: string,
    name: string;
    sum: number;
}

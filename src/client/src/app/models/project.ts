import { DataTableRow } from ".";
import { Guid } from "guid-typescript";

export interface Project {
    id: Guid
    name: string;
    startingInvestmentSum: number;
}

export interface ProjectRow extends DataTableRow {
    id: string,
    name: string;
    startingInvestmentSum: number;
}

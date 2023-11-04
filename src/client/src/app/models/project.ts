import { DataTableRow } from ".";
import { Guid } from "guid-typescript";
import { Enterprise } from "./enterprise";

export interface Project {
    id: Guid
    name: string;
    startingInvestmentSum: number;
    enterprise: Enterprise;
    enterpriseId: Guid;
}

export interface ProjectRow extends DataTableRow {
    id: string,
    name: string;
    startingInvestmentSum: number;
    enterprise: string;
}

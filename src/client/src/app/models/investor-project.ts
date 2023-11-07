import { DataTableRow, Project } from ".";
import { Guid } from "guid-typescript";

// TODO: add investor
export interface InvestorProject {
    id: Guid
    minIncomeRate: number;
    maxRiskRate: number;
    project: Project;
    projectId: Guid;
}

export interface InvestorProjectRow extends DataTableRow {
    id: string,
    internalId: string;
    minIncomeRate: number;
    maxRiskRate: number;
    project: string;
    projectId: string;
}

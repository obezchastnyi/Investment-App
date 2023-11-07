import { DataTableRow, Period, Possibility, Project } from ".";
import { Guid } from "guid-typescript";

// TODO add expert
export interface ExpertProject {
    id: Guid
    cashFlowRate: number;
    possibility: Possibility;
    possibilityId: Guid;
    period: Period;
    periodId: Guid;
    project: Project;
    projectId: Guid;
}

export interface ExpertProjectRow extends DataTableRow {
    id: string,
    internalId: string;
    cashFlowRate: number;
    possibility: string;
    possibilityId: string;
    period: string;
    periodId: string;
    project: string;
    projectId: string;
}

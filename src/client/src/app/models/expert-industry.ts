import { DataTableRow } from ".";
import { Guid } from "guid-typescript";
import { Criteria } from "./criteria";
import { Industry } from "./industry";
import { Expert } from "./expert";

export interface ExpertIndustry {
    id: Guid
    industry: Industry;
    industryId: Guid;
    expert: Expert;
    expertId: Guid;
    rate: number;
}

export interface ExpertIndustryRow extends DataTableRow {
    id: string,
    internalId: string;
    industry: string;
    industryId: string;
    expert: string;
    expertId: string;
    rate: number;
}

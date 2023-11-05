import { DataTableRow } from ".";
import { Guid } from "guid-typescript";
import { Criteria } from "./criteria";
import { Industry } from "./industry";

export interface IndustryCriteria {
    id: Guid
    industry: Industry;
    industryId: Guid;
    criteria: Criteria;
    criteriaId: Guid;
    industrySpecificWeight: number;
}

export interface IndustryCriteriaRow extends DataTableRow {
    id: string,
    internalId: string;
    industry: string;
    industryId: string;
    criteria: string;
    criteriaId: string;
    industrySpecificWeight: number;
}

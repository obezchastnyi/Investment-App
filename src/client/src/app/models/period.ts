import { DataTableRow } from ".";
import { Guid } from "guid-typescript";

export interface Period {
    id: Guid
    startDate: Date,
    endDate: Date,
    discountRate: number;
    riskFreeDiscountRate: number;
}

export interface PeriodRow extends DataTableRow {
    id: string,
    internalId: string;
    startDate: string,
    endDate: string,
    discountRate: number;
    riskFreeDiscountRate: number;
}

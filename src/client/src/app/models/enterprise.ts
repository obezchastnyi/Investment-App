import { DataTableRow } from ".";
import { Guid } from "guid-typescript";

export interface Enterprise {
    id: Guid
    name: string;
    address: string;
    bankAccount: string;
    taxNumber: number;
}

export interface EnterpriseRow extends DataTableRow {
    id: string,
    internalId: string;
    name: string;
    address: string;
    bankAccount: string;
    taxNumber: number;
}

import { DataTableRow, Role } from ".";
import { Guid } from "guid-typescript";

export interface Expert {
    id: Guid
    name: string;
    surName: string;
    middleName: string;
    wokPlace: string;
    speciality: string;
    competenceCoefficient: number;
}

export interface ExpertRow extends DataTableRow {
    id: string,
    internalId: string;
    name: string;
    surName: string;
    middleName: string;
    wokPlace: string;
    speciality: string;
    competenceCoefficient: number;
}

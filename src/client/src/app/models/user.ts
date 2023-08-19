import { DataTableRow } from ".";
import { Guid } from "guid-typescript";

export interface User {
    id: Guid
    userName: string;
    userRoleId: number;
}

export interface UserRow extends DataTableRow {
    id: string,
    userName: string;
    role: string;
}

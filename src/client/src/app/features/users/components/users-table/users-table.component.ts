import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { UsersCacheService } from '../../../../shared/services';
import { UserRow, DataTableColumn, Role } from '../../../../models';

@Component({
    selector: 'ia-users-table',
    templateUrl: 'users-table.component.html',
    styleUrls: ['users-table.component.scss'],
})
export class UsersTableComponent implements OnInit {

    @ViewChild('dropdownTemplate', { static: true }) dropdownTemplate: TemplateRef<unknown>;
    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('accessTemplate', { static: true }) accessTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 80;

    usersTableColumnsObs: Observable<DataTableColumn[]>;
    usersTableRowsObs: Observable<UserRow[] | null>;
    updatedUsers: UserRow[] = []

    rolesObs: Observable<Role[] | null>;

    constructor(
        private titleService: Title,
        private dataService: UsersCacheService,
    ) {
        this.titleService.setTitle('Users - Investments');
    }

    ngOnInit(): void {
        const USERS_TABLE_COLUMNS: DataTableColumn[] = [
            {
                prop: 'userName',
                name: 'UserName',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'role',
                name: 'Role',
                innerTemplate: this.dropdownTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'role',
                name: 'Role',
                innerTemplate: this.accessTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
        ];
        this.usersTableColumnsObs = new BehaviorSubject<DataTableColumn[]>(USERS_TABLE_COLUMNS).asObservable();
        this.usersTableRowsObs = this.dataService.getTableRows();

        this.rolesObs = this.dataService.getUserRoles();
    }

    onInputChange(row: UserRow, value: any, column: string) {
        let changedRow = {...row};
        if (this.updatedUsers.indexOf(changedRow) === -1) {
            this.updatedUsers.push(changedRow)
        }

        this.updatedUsers[(this.updatedUsers.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: UserRow, value: any, column: string) {
        let updatedRow = {...row};
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRoleChange(row: UserRow, role: string) {
        row.role = role;
        this.dataService.updateTableRow(row);
    }
}

import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService, EnterpriseCacheService } from '../../../../shared/services';
import { DataTableColumn } from 'src/app/models';
import { BehaviorSubject, Observable } from 'rxjs';
import { EnterpriseRow } from 'src/app/models/enterprise';

@Component({
    selector: 'ia-enterprise',
    templateUrl: 'enterprise.component.html',
    styleUrls: ['enterprise.component.scss'],
})
export class EnterpriseComponent implements OnInit {

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    dataTableColumnsObs: Observable<DataTableColumn[]>;
    dataTableRowsObs: Observable<EnterpriseRow[] | null>;
    updatedData: EnterpriseRow[] = []

    userName: string;
    role: string;

    constructor(private titleService: Title,
        private dataService: EnterpriseCacheService,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Enterprise - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
        const ENTERPRISE_TABLE_COLUMNS: DataTableColumn[] = [
            {
                prop: 'internalId',
                name: 'ID',
                sortable: true,
                width: 100,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'name',
                name: 'Name',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 200,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'bankAccount',
                name: 'Bank Account',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 200,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'address',
                name: 'Address',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'taxNumber',
                name: 'Tax Number',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 200,
                flexGrow: 1,
                draggable: false,
            },
            {
                name: '',
                innerTemplate: this.rowDeleteTemplate,
                sortable: false,
                width: 80,
                canAutoResize: false,
                resizeable: false,
                draggable: false,
            },
        ];
        this.dataTableColumnsObs = new BehaviorSubject<DataTableColumn[]>(ENTERPRISE_TABLE_COLUMNS).asObservable();
        this.dataTableRowsObs = this.dataService.getTableRows();
    }

    onInputChange(row: EnterpriseRow, value: any, column: string) {
        let changedRow = { ...row };
        if (this.updatedData.indexOf(changedRow) === -1) {
            this.updatedData.push(changedRow)
        }

        this.updatedData[(this.updatedData.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: EnterpriseRow, value: any, column: string) {
        let updatedRow = { ...row };
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: EnterpriseRow) {
        this.dataService.confirmRowDeleting(row.id);
    }

    addNewRow() {
        this.dataService.addNewTableRow();
    }

    discardAllChanges() {
        window.location.reload();
    }

    confirmAllUpdates() {
        if (this.updatedData.find(d => Object.values(d).find(v => v.toString() === ''))) {
            alert('There are invalid fields in Table');
            return;
        }
        this.dataService.confirmAllUpdates(this.updatedData);
    }
}

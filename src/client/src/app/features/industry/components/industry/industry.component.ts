import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService } from '../../../../shared/services';
import { DataTableColumn } from '../../../../models';
import { IndustryRow } from 'src/app/models/industry';
import { IndustryCacheService } from 'src/app/shared/services/industry-cache.service';

@Component({
    selector: 'ia-industry',
    templateUrl: 'industry.component.html',
    styleUrls: ['industry.component.scss'],
})
export class IndustryComponent implements OnInit {

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    dataTableColumnsObs: Observable<DataTableColumn[]>;
    dataTableRowsObs: Observable<IndustryRow[] | null>;
    updatedData: IndustryRow[] = []

    userName: string;
    role: string;

    constructor(private titleService: Title,
        private dataService: IndustryCacheService,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Industry - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
        const POSSIBILITY_TABLE_COLUMNS: DataTableColumn[] = [
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
                width: 300,
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
        this.dataTableColumnsObs = new BehaviorSubject<DataTableColumn[]>(POSSIBILITY_TABLE_COLUMNS).asObservable();
        this.dataTableRowsObs = this.dataService.getTableRows();
    }

    onInputChange(row: IndustryRow, value: any, column: string) {
        let changedRow = { ...row };
        if (this.updatedData.indexOf(changedRow) === -1) {
            this.updatedData.push(changedRow)
        }

        this.updatedData[(this.updatedData.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: IndustryRow, value: any, column: string) {
        let updatedRow = { ...row };
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: IndustryRow) {
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

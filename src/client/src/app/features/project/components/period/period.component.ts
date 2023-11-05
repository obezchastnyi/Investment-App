import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService, PeriodCacheService } from '../../../../shared/services';
import { DataTableColumn, PeriodRow } from '../../../../models';
import { Enterprise } from 'src/app/models/enterprise';

@Component({
    selector: 'ia-period',
    templateUrl: 'period.component.html',
    styleUrls: ['period.component.scss'],
})
export class PeriodComponent implements OnInit {

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;
    @ViewChild('dateTemplate', { static: true }) dateTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    dataTableColumnsObs: Observable<DataTableColumn[]>;
    dataTableRowsObs: Observable<PeriodRow[] | null>;
    updatedData: PeriodRow[] = []

    userName: string;
    role: string;

    enterprisesObs: Observable<Enterprise[] | null>;

    constructor(private titleService: Title,
        private dataService: PeriodCacheService,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Period - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
        const PERIOD_TABLE_COLUMNS: DataTableColumn[] = [
            {
                prop: 'internalId',
                name: 'ID',
                sortable: true,
                width: 100,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'startDate',
                name: 'Start Date',
                innerTemplate: this.dateTemplate,
                sortable: true,
                width: 150,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'endDate',
                name: 'End Date',
                innerTemplate: this.dateTemplate,
                sortable: true,
                width: 150,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'discountRate',
                name: 'Discount Rate (DR)',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 100,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'riskFreeDiscountRate',
                name: 'Risk Free DR Rate',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 100,
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
        this.dataTableColumnsObs = new BehaviorSubject<DataTableColumn[]>(PERIOD_TABLE_COLUMNS).asObservable();
        this.dataTableRowsObs = this.dataService.getTableRows();
    }

    onInputChange(row: PeriodRow, value: any, column: string) {
        let changedRow = { ...row };
        if (this.updatedData.indexOf(changedRow) === -1) {
            this.updatedData.push(changedRow)
        }

        this.updatedData[(this.updatedData.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: PeriodRow, value: any, column: string) {
        let updatedRow = { ...row };
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: PeriodRow) {
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

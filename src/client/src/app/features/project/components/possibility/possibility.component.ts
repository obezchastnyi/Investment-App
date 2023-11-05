import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService, PossibilityCacheService } from '../../../../shared/services';
import { DataTableColumn, PossibilityRow } from '../../../../models';

@Component({
    selector: 'ia-possibility',
    templateUrl: 'possibility.component.html',
    styleUrls: ['possibility.component.scss'],
})
export class PossibilityComponent implements OnInit {

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    dataTableColumnsObs: Observable<DataTableColumn[]>;
    dataTableRowsObs: Observable<PossibilityRow[] | null>;
    updatedData: PossibilityRow[] = []

    userName: string;
    role: string;

    constructor(private titleService: Title,
        private dataService: PossibilityCacheService,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Possibility - Investments');

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
                prop: 'rate',
                name: 'Rate',
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

    onInputChange(row: PossibilityRow, value: any, column: string) {
        let changedRow = { ...row };
        if (this.updatedData.indexOf(changedRow) === -1) {
            this.updatedData.push(changedRow)
        }

        this.updatedData[(this.updatedData.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: PossibilityRow, value: any, column: string) {
        let updatedRow = { ...row };
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: PossibilityRow) {
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

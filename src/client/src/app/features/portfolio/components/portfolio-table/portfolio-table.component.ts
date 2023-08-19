import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService, PortfolioCacheService } from '../../../../shared/services';
import { PortfolioRow, DataTableColumn } from '../../../../models';

@Component({
    selector: 'ia-portfolio-table',
    templateUrl: 'portfolio-table.component.html',
    styleUrls: ['portfolio-table.component.scss'],
})
export class PortfolioTableComponent implements OnInit {

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 180;

    portfoliosTableColumnsObs: Observable<DataTableColumn[]>;
    portfoliosTableRowsObs: Observable<PortfolioRow[] | null>;
    updatedPortfolios: PortfolioRow[] = []

    userName: string;
    role: string;

    constructor(private titleService: Title,
                private dataService: PortfolioCacheService,
                private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Portfolio - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
        const PORTFOLIO_TABLE_COLUMNS: DataTableColumn[] = [
            {
                name: '',
                innerTemplate: this.rowDeleteTemplate,
                sortable: false,
                width: 100, // 13 for the checkbox + 28 default left padding + 9 right padding
                canAutoResize: false,
                resizeable: false,
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
                prop: 'sum',
                name: 'Sum',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
        ];
        this.portfoliosTableColumnsObs = new BehaviorSubject<DataTableColumn[]>(PORTFOLIO_TABLE_COLUMNS).asObservable();
        this.portfoliosTableRowsObs = this.dataService.getTableRows();
    }

    onInputChange(row: PortfolioRow, value: any, column: string) {
        let changedRow = {...row};
        if (this.updatedPortfolios.indexOf(changedRow) === -1) {
            this.updatedPortfolios.push(changedRow)
        }

        this.updatedPortfolios[(this.updatedPortfolios.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: PortfolioRow, value: any, column: string) {
        let updatedRow = {...row};
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: PortfolioRow) {
        this.dataService.confirmRowDeleting(row.id);
    }

    addNewRow() {
        this.dataService.addNewTableRow();
    }

    discardAllChanges() {
        window.location.reload();
    }

    confirmAllUpdates() {
        if (this.updatedPortfolios.find(p => p.name == '' || p.sum.toString() == '')) {
            alert('There are invalid fields in Table');
            return;
        }
        this.dataService.confirmAllUpdates(this.updatedPortfolios);
    }

    protected readonly window = window;
}

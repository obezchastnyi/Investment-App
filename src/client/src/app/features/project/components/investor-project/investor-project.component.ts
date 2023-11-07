import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService } from '../../../../shared/services';
import { DataTableColumn, ProjectRow } from '../../../../models';
import { faLink } from '@fortawesome/free-solid-svg-icons';
import { InvestorProjectRow } from 'src/app/models/investor-project';
import { InvestorProjectCacheService } from 'src/app/shared/services/investor-project-cache.service';

@Component({
    selector: 'ia-investor-project',
    templateUrl: 'investor-project.component.html',
    styleUrls: ['investor-project.component.scss'],
})
export class InvestorProjectComponent implements OnInit {

    faLink = faLink;

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;
    @ViewChild('dropdownTemplate', { static: true }) dropdownTemplate: TemplateRef<unknown>;
    @ViewChild('linkTemplate', { static: true }) linkTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    projectsTableColumnsObs: Observable<DataTableColumn[]>;
    projectsTableRowsObs: Observable<InvestorProjectRow[] | null>;
    updatedData: InvestorProjectRow[] = []

    userName: string;
    role: string;

    projectsObs: Observable<ProjectRow[] | null>;

    constructor(private titleService: Title,
        private dataService: InvestorProjectCacheService,
        private authService: AuthenticationService,
    ) {
        this.titleService.setTitle('Investor Project - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
        const PROJECT_TABLE_COLUMNS: DataTableColumn[] = [
            {
                prop: 'internalId',
                name: 'ID',
                sortable: true,
                width: 100,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'minIncomeRate',
                name: 'Min Income Rate',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'maxRiskRate',
                name: 'Max Risk Rate',
                innerTemplate: this.inputTemplate,
                sortable: true,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'project',
                name: 'Project',
                innerTemplate: this.dropdownTemplate,
                sortable: true,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'projectId',
                name: '',
                innerTemplate: this.linkTemplate,
                sortable: true,
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
        this.projectsTableColumnsObs = new BehaviorSubject<DataTableColumn[]>(PROJECT_TABLE_COLUMNS).asObservable();
        this.projectsTableRowsObs = this.dataService.getTableRows();

        this.projectsObs = this.dataService.getProjects();
    }

    onInputChange(row: InvestorProjectRow, value: any, column: string) {
        let changedRow = { ...row };
        if (this.updatedData.indexOf(changedRow) === -1) {
            this.updatedData.push(changedRow)
        }

        this.updatedData[(this.updatedData.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: InvestorProjectRow, value: any, column: string) {
        let updatedRow = { ...row };
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: InvestorProjectRow) {
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

    onProjectChange(row: InvestorProjectRow, value: string) {
        row.project = value;
        this.dataService.updateTableRow(row);
    }

    transformLink(value: string) {
        return value.split('-')[0];
    }
}

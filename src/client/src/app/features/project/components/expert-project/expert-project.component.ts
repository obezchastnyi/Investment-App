import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService, ProjectCacheService } from '../../../../shared/services';
import { ProjectRow, DataTableColumn } from '../../../../models';
import { Enterprise } from 'src/app/models/enterprise';

@Component({
    selector: 'ia-expert-project',
    templateUrl: 'expert-project.component.html',
    styleUrls: ['expert-project.component.scss'],
})
export class ExpertProjectComponent implements OnInit {

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;
    @ViewChild('dropdownTemplate', { static: true }) dropdownTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    projectsTableColumnsObs: Observable<DataTableColumn[]>;
    projectsTableRowsObs: Observable<ProjectRow[] | null>;
    updatedProjects: ProjectRow[] = []

    userName: string;
    role: string;

    enterprisesObs: Observable<Enterprise[] | null>;

    constructor(private titleService: Title,
        private dataService: ProjectCacheService,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Expert Project - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
        const PROJECT_TABLE_COLUMNS: DataTableColumn[] = [
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
                prop: 'startingInvestmentSum',
                name: 'Starting Investment Sum',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'enterprise',
                name: 'Enterprise',
                innerTemplate: this.dropdownTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
        ];
        this.projectsTableColumnsObs = new BehaviorSubject<DataTableColumn[]>(PROJECT_TABLE_COLUMNS).asObservable();
        this.projectsTableRowsObs = this.dataService.getTableRows();

        //this.enterprisesObs = this.dataService.getEnterprises();
    }

    onInputChange(row: ProjectRow, value: any, column: string) {
        let changedRow = { ...row };
        if (this.updatedProjects.indexOf(changedRow) === -1) {
            this.updatedProjects.push(changedRow)
        }

        this.updatedProjects[(this.updatedProjects.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: ProjectRow, value: any, column: string) {
        let updatedRow = { ...row };
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: ProjectRow) {
        this.dataService.confirmRowDeleting(row.id);
    }

    addNewRow() {
        this.dataService.addNewTableRow();
    }

    discardAllChanges() {
        window.location.reload();
    }

    confirmAllUpdates() {
        if (this.updatedProjects.find(p => p.name == '' || p.startingInvestmentSum.toString() == '')) {
            alert('There are invalid fields in Table');
            return;
        }
        this.dataService.confirmAllUpdates(this.updatedProjects);
    }

    onEnterpriseChange(row: ProjectRow, enterprise: string) {
        row.enterprise = enterprise;
        this.dataService.updateTableRow(row);
    }
}

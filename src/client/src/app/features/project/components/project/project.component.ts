import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService, ProjectCacheService } from '../../../../shared/services';
import { ProjectRow, DataTableColumn } from '../../../../models';
import { EnterpriseRow } from 'src/app/models/enterprise';
import { faLink } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'ia-project',
    templateUrl: 'project.component.html',
    styleUrls: ['project.component.scss'],
})
export class ProjectComponent implements OnInit {

    faLink = faLink;

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;
    @ViewChild('dropdownTemplate', { static: true }) dropdownTemplate: TemplateRef<unknown>;
    @ViewChild('linkTemplate', { static: true }) linkTemplate: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    projectsTableColumnsObs: Observable<DataTableColumn[]>;
    projectsTableRowsObs: Observable<ProjectRow[] | null>;
    updatedProjects: ProjectRow[] = []

    userName: string;
    role: string;

    enterprisesObs: Observable<EnterpriseRow[] | null>;

    constructor(private titleService: Title,
        private dataService: ProjectCacheService,
        private authService: AuthenticationService,
    ) {
        this.titleService.setTitle('Project - Investments');

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
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'enterpriseId',
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

        this.enterprisesObs = this.dataService.getEnterprises();
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

    transformLink(value: string) {
        return value.split('-')[0];
    }
}

import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService } from '../../../../shared/services';
import { DataTableColumn, ProjectRow } from '../../../../models';
import { faLink } from '@fortawesome/free-solid-svg-icons';
import { ExpertProjectRow } from 'src/app/models/expert-project';
import { ExpertProjectCacheService } from 'src/app/shared/services/expert-project-cache.service';

@Component({
    selector: 'ia-expert-project',
    templateUrl: 'expert-project.component.html',
    styleUrls: ['expert-project.component.scss'],
})
export class ExpertProjectComponent implements OnInit {

    faLink = faLink;

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;
    @ViewChild('dropdownTemplate', { static: true }) dropdownTemplate: TemplateRef<unknown>;
    @ViewChild('linkTemplate', { static: true }) linkTemplate: TemplateRef<unknown>;
    @ViewChild('link2Template', { static: true }) link2Template: TemplateRef<unknown>;
    @ViewChild('link3Template', { static: true }) link3Template: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    projectsTableColumnsObs: Observable<DataTableColumn[]>;
    projectsTableRowsObs: Observable<ExpertProjectRow[] | null>;
    updatedData: ExpertProjectRow[] = []

    userName: string;
    role: string;

    //projectsObs: Observable<ProjectRow[] | null>;
    //projectsObs: Observable<ProjectRow[] | null>;
    projectsObs: Observable<ProjectRow[] | null>;

    constructor(private titleService: Title,
        private dataService: ExpertProjectCacheService,
        private authService: AuthenticationService,
    ) {
        this.titleService.setTitle('Expert Project - Investments');

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
                prop: 'cashFlowRate',
                name: 'Cash Flow Rate',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 150,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'possibility',
                name: 'Possibility',
                //innerTemplate: this.dropdownTemplate,
                sortable: true,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'possibilityId',
                name: '',
                innerTemplate: this.linkTemplate,
                sortable: true,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'period',
                name: 'Period',
                //innerTemplate: this.dropdownTemplate,
                sortable: true,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'periodId',
                name: '',
                innerTemplate: this.link2Template,
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
                innerTemplate: this.link3Template,
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

    onInputChange(row: ExpertProjectRow, value: any, column: string) {
        let changedRow = { ...row };
        if (this.updatedData.indexOf(changedRow) === -1) {
            this.updatedData.push(changedRow)
        }

        this.updatedData[(this.updatedData.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: ExpertProjectRow, value: any, column: string) {
        let updatedRow = { ...row };
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: ExpertProjectRow) {
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

    onProjectChange(row: ExpertProjectRow, value: string) {
        row.project = value;
        this.dataService.updateTableRow(row);
    }

    transformLink(value: string) {
        return value.split('-')[0];
    }
}

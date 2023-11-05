import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthenticationService } from '../../../../shared/services';
import { DataTableColumn } from '../../../../models';
import { faLink } from '@fortawesome/free-solid-svg-icons';
import { IndustryCriteriaRow } from 'src/app/models/industry-criteria';
import { CriteriaRow } from 'src/app/models/criteria';
import { IndustryRow } from 'src/app/models/industry';
import { IndustryCriteriaCacheService } from 'src/app/shared/services/industry-criteria-cache.service';

@Component({
    selector: 'ia-industry-criteria',
    templateUrl: 'industry-criteria.component.html',
    styleUrls: ['industry-criteria.component.scss'],
})
export class IndustryCriteriaComponent implements OnInit {

    faLink = faLink;

    @ViewChild('inputTemplate', { static: true }) inputTemplate: TemplateRef<unknown>;
    @ViewChild('rowDeleteTemplate', { static: true }) rowDeleteTemplate: TemplateRef<unknown>;
    @ViewChild('dropdownTemplate', { static: true }) dropdownTemplate: TemplateRef<unknown>;
    @ViewChild('dropdown2Template', { static: true }) dropdown2Template: TemplateRef<unknown>;
    @ViewChild('linkTemplate', { static: true }) linkTemplate: TemplateRef<unknown>;
    @ViewChild('link2Template', { static: true }) link2Template: TemplateRef<unknown>;

    tableHeight = window.innerHeight - 150;

    projectsTableColumnsObs: Observable<DataTableColumn[]>;
    projectsTableRowsObs: Observable<IndustryCriteriaRow[] | null>;
    updatedData: IndustryCriteriaRow[] = []

    userName: string;
    role: string;

    criteriasObs: Observable<CriteriaRow[] | null>;
    industriesObs: Observable<IndustryRow[] | null>;

    constructor(private titleService: Title,
        private dataService: IndustryCriteriaCacheService,
        private authService: AuthenticationService,
    ) {
        this.titleService.setTitle('Industry Criteria - Investments');

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
                prop: 'industrySpecificWeight',
                name: 'Industry Specific Weight',
                innerTemplate: this.inputTemplate,
                sortable: true,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'criteria',
                name: 'Criteria',
                innerTemplate: this.dropdownTemplate,
                sortable: true,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'criteriaId',
                name: '',
                innerTemplate: this.linkTemplate,
                sortable: true,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'industry',
                name: 'Industry',
                innerTemplate: this.dropdown2Template,
                sortable: true,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'industryId',
                name: '',
                innerTemplate: this.link2Template,
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

        this.criteriasObs = this.dataService.getCriterias();
        this.industriesObs = this.dataService.getIndustries();
    }

    onInputChange(row: IndustryCriteriaRow, value: any, column: string) {
        let changedRow = { ...row };
        if (this.updatedData.indexOf(changedRow) === -1) {
            this.updatedData.push(changedRow)
        }

        this.updatedData[(this.updatedData.indexOf(changedRow))][column] = value;
    }

    onRowUpdate(row: IndustryCriteriaRow, value: any, column: string) {
        let updatedRow = { ...row };
        updatedRow[column] = value;
        this.dataService.updateTableRow(updatedRow);
    }

    onRowDelete(row: IndustryCriteriaRow) {
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

    onCriteriaChange(row: IndustryCriteriaRow, value: string) {
        row.criteria = value;
        this.dataService.updateTableRow(row);
    }

    onIndustryChange(row: IndustryCriteriaRow, value: string) {
        row.industry = value;
        this.dataService.updateTableRow(row);
    }

    transformLink(value: string) {
        return value.split('-')[0];
    }
}

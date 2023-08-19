import { Component, TemplateRef, ViewChild } from '@angular/core';
import { DataTableRow } from '../../../../models';

@Component({
    selector: 'ia-data-table-column-template-wrapper',
    templateUrl: 'data-table-column-template-wrapper.component.html',
    styleUrls: ['data-table-column-template-wrapper.component.scss'],
})
export class DataTableColumnTemplateWrapperComponent {

    @ViewChild('columnTemplate', { static: true }) columnTemplate: TemplateRef<unknown>;
    innerColumnTemplate: TemplateRef<unknown>;
    first = false;
    getLink: (row: DataTableRow) => string;

    constructor() { }

    getTemplateRef(): TemplateRef<unknown> {
        return this.columnTemplate;
    }

    getMyLink(row: DataTableRow): string {
        return this.getLink ? this.getLink(row) : '';
    }
}

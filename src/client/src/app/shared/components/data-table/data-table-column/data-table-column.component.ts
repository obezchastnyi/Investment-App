import { Component, Input } from '@angular/core';

@Component({
    selector: 'ia-data-table-column',
    templateUrl: 'data-table-column.component.html',
    styleUrls: ['data-table-column.component.scss'],
})
export class DataTableColumnComponent {

    @Input() first = false;
    @Input() linkTo = '';

    constructor() { }
}

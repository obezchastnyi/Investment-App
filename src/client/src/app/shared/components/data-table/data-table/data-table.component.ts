import { Component, ElementRef, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { ColumnMode, SelectionType } from '@swimlane/ngx-datatable';
import { Observable, combineLatest } from 'rxjs';
import { DataTableColumnTemplateWrapperComponent } from '..';
import { DataTableColumn, DataTableRow, DataTableScroll, DataTableSortType } from '../../../../models';

@Component({
    selector: 'ia-data-table',
    templateUrl: 'data-table.component.html',
    styleUrls: ['data-table.component.scss'],
})
export class DataTableComponent implements OnInit {

    // Table inputs -> https://swimlane.gitbook.io/ngx-datatable/api/table/inputs

    @Input() columnsObs: Observable<DataTableColumn[]>;
    @Input() columnMode: ColumnMode = ColumnMode.force;
    columns: DataTableColumn[];

    @Input() rowsObs: Observable<DataTableRow[]>;
    rowsLoaded = 0;

    // Default values for Admin Portal design
    @Input() headerHeight = 37;
    @Input() rowHeight = 46; // height 43 + margin 3
    @Input() tableHeight = 400;
    viewHeight = 0;

    // Default is infinite scroll
    @Input() allowScroll = true;
    needsToScroll = true;
    // Neither pagination nor footer
    @Input() externalPaging = false;
    @Input() pageSize = undefined;
    @Input() currentPageIndex = 0;
    @Input() footerHeight = 0;

    @Input() selectionType = SelectionType.single;
    @Input() selectedRows: DataTableRow[] = [];
    @Output() select = new EventEmitter<DataTableRow[]>();

    @Input() externalSorting = false;
    @Input() defaultSorting: DataTableSortType[] = [];
    @Output() sort = new EventEmitter<DataTableSortType>();

    // only emit scroll past a given ratio of the viewport
    @Input() scrollRatioToEmit = 0;
    @Output() scrollPastRatio = new EventEmitter<DataTableScroll>();

    // https://swimlane.gitbook.io/ngx-datatable/api/table/outputs#activate
    /* eslint-disable  @typescript-eslint/no-explicit-any */
    @Output() activate = new EventEmitter<any>();

    @ViewChild('defaultInnerTemplate', { static: true }) defaultInnerTemplate: TemplateRef<unknown>;

    constructor(
        private myComponentRef: ElementRef,
        private myViewContainerRef: ViewContainerRef,
    ) { }

    ngOnInit(): void {
        combineLatest([
            this.columnsObs,
            this.rowsObs,
        ]).subscribe(([tableColumns, tableRows]) => {
            if (!!tableColumns) {
                this.columns = tableColumns;
                // wrap columns inner templates in column templates
                for (let index = 0; index < this.columns.length; index++) {
                    const wrapperRef = this.myViewContainerRef.createComponent(DataTableColumnTemplateWrapperComponent);
                    if (!!this.columns[index].innerTemplate) {
                        wrapperRef.instance.innerColumnTemplate = this.columns[index].innerTemplate;
                    } else {
                        wrapperRef.instance.innerColumnTemplate = this.defaultInnerTemplate;
                    }
                    wrapperRef.instance.first = index === 0;
                    wrapperRef.instance.getLink = this.columns[index].getLink;
                    this.columns[index].cellTemplate = wrapperRef.instance.getTemplateRef();
                }
            }

            if (!!tableRows) {
                // Total height available to display rows in the viewport
                this.viewHeight = this.myComponentRef.nativeElement.getBoundingClientRect().height - this.headerHeight;
                // Does it need a vertical scroll bar?
                this.rowsLoaded = tableRows?.length ?? 0;
                this.needsToScroll = this.allowScroll && (this.rowsLoaded * this.rowHeight > this.viewHeight);
            }
        });
    }

    onScroll({ offsetX, offsetY }) {
        // Check if we scrolled near to the end of the viewport
        if (this.needsToScroll && offsetY + this.viewHeight >= this.scrollRatioToEmit * this.rowsLoaded * this.rowHeight) {
            this.scrollPastRatio.emit({ offsetX, offsetY } as DataTableScroll);
        }
    }

    onSelect({ selected }): void {
        this.select.emit(selected as DataTableRow[]);
    }

    onSort({ sorts }): void {
        this.sort.emit(sorts[0] as DataTableSortType);
    }

    onActivate(event): void {
        this.activate.emit(event);
    }
}

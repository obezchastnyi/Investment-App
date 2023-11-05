import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { faDownload } from '@fortawesome/free-solid-svg-icons';
import { BehaviorSubject, Observable, of } from "rxjs";
import { ActionRow, DataTableColumn } from 'src/app/models';
import { AuthenticationService } from 'src/app/shared/services';

@Component({
    selector: 'ia-main-view',
    templateUrl: './main-view.component.html',
    styleUrls: ['./main-view.component.scss'],
})
export class MainViewComponent implements OnInit {

    faDownload = faDownload;

    actionsTableColumnsObs: Observable<DataTableColumn[]>;
    actionsTableRowsObs: Observable<ActionRow[] | null>;

    @ViewChild('checkTemplate', { static: true }) checkTemplate: TemplateRef<unknown>;

    userName: string;
    role: string;

    constructor(private authService: AuthenticationService, private titleService: Title) {
        this.userName = this.authService.userName;
        this.role = this.authService.role;

        this.titleService.setTitle('Home - Investments');
    }

    ngOnInit(): void {
        const ACTIONS_TABLE_COLUMNS: DataTableColumn[] = [
            {
                prop: 'allowed',
                name: '',
                innerTemplate: this.checkTemplate,
                sortable: false,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
            {
                prop: 'action',
                name: 'Action',
                sortable: false,
                width: 300,
                flexGrow: 1,
                draggable: false,
            },
        ];

        this.actionsTableColumnsObs = new BehaviorSubject<DataTableColumn[]>(ACTIONS_TABLE_COLUMNS).asObservable();

        switch (this.role) {
            case 'Admin':
                this.actionsTableRowsObs = of([
                    {
                        allowed: 1,
                        action: 'Admin',
                    },
                    {
                        allowed: 1,
                        action: 'Reader',
                    },
                    {
                        allowed: 1,
                        action: 'Writer',
                    },
                    {
                        allowed: 1,
                        action: 'Creator',
                    },
                ]);
                break;
            case 'Reader':
                this.actionsTableRowsObs = of([
                    {
                        allowed: 0,
                        action: 'Admin',
                    },
                    {
                        allowed: 1,
                        action: 'Reader',
                    },
                    {
                        allowed: 0,
                        action: 'Writer',
                    },
                    {
                        allowed: 0,
                        action: 'Creator',
                    },
                ]);
                break;
            case 'Writer':
                this.actionsTableRowsObs = of([
                    {
                        allowed: 0,
                        action: 'Admin',
                    },
                    {
                        allowed: 1,
                        action: 'Reader',
                    },
                    {
                        allowed: 1,
                        action: 'Writer',
                    },
                    {
                        allowed: 0,
                        action: 'Creator',
                    },
                ]);
                break;
            case 'Creator':
                this.actionsTableRowsObs = of([
                    {
                        allowed: 0,
                        action: 'Admin',
                    },
                    {
                        allowed: 1,
                        action: 'Reader',
                    },
                    {
                        allowed: 0,
                        action: 'Writer',
                    },
                    {
                        allowed: 1,
                        action: 'Creator',
                    },
                ]);
                break;
        }
    }
}

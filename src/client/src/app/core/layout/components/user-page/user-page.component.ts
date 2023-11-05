import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { BehaviorSubject, Observable, of } from "rxjs";
import { ActionRow, DataTableColumn } from 'src/app/models';
import { AuthenticationService } from 'src/app/shared/services';

@Component({
    selector: 'ia-user-page',
    templateUrl: './user-page.component.html',
    styleUrls: ['./user-page.component.scss'],
})
export class UserPageComponent implements OnInit {

    actionsTableColumnsObs: Observable<DataTableColumn[]>;
    actionsTableRowsObs: Observable<ActionRow[] | null>;

    @ViewChild('checkTemplate', { static: true }) checkTemplate: TemplateRef<unknown>;

    userName: string;
    role: string;

    constructor(private authService: AuthenticationService, private titleService: Title) {
        this.userName = this.authService.userName;
        this.role = this.authService.role;

        this.titleService.setTitle('User Page - Investments');
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

    logout(): void {
        if (confirm('Do you really want to log out from Investment App?')) {
            this.authService.logout();
        }
    }
}

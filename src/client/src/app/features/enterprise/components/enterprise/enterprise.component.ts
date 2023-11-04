import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';

@Component({
    selector: 'ia-enterprise',
    templateUrl: 'enterprise.component.html',
    styleUrls: ['enterprise.component.scss'],
})
export class EnterpriseComponent implements OnInit {

    userName: string;
    role: string;

    constructor(private titleService: Title,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Enterprise - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
    }
}

import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';

@Component({
    selector: 'ia-industry-criteria',
    templateUrl: 'industry-criteria.component.html',
    styleUrls: ['industry-criteria.component.scss'],
})
export class IndustryCriteriaComponent implements OnInit {

    userName: string;
    role: string;

    constructor(private titleService: Title,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Industry Criteria - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
    }
}

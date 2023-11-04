import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';

@Component({
    selector: 'ia-assessment',
    templateUrl: 'assessment.component.html',
    styleUrls: ['assessment.component.scss'],
})
export class AssessmentComponent implements OnInit {

    userName: string;
    role: string;

    constructor(private titleService: Title,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Assessment - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
    }
}

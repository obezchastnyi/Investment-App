import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';

@Component({
    selector: 'ia-evaluation',
    templateUrl: 'evaluation.component.html',
    styleUrls: ['evaluation.component.scss'],
})
export class EvaluationComponent implements OnInit {

    userName: string;
    role: string;

    constructor(private titleService: Title,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Evaluation - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
    }
}

import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';
import { Observable } from 'rxjs';
import { AssessCacheService } from 'src/app/shared/services/assess-cache.service';

@Component({
    selector: 'ia-assessment',
    templateUrl: 'assessment.component.html',
    styleUrls: ['assessment.component.scss'],
})
export class AssessmentComponent implements OnInit {

    userName: string;
    role: string;

    resultObs: Observable<number | null>;

    constructor(private titleService: Title,
        private authService: AuthenticationService,
        private dataService: AssessCacheService,
    ) {
        this.titleService.setTitle('Assessment - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
        this.resultObs = this.dataService.getTableRows();
    }

    onCalculate() {
        this.dataService.loadDataFromApi();
    }
}

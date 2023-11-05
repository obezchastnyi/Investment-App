import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';
import { Observable } from 'rxjs';
import { EvaluateCacheService } from 'src/app/shared/services/evaluate-cache.service';

@Component({
    selector: 'ia-evaluation',
    templateUrl: 'evaluation.component.html',
    styleUrls: ['evaluation.component.scss'],
})
export class EvaluationComponent implements OnInit {

    userName: string;
    role: string;

    resultObs: Observable<number | null>;

    constructor(private titleService: Title,
        private authService: AuthenticationService,
        private dataService: EvaluateCacheService,
    ) {
        this.titleService.setTitle('Evaluation - Investments');

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

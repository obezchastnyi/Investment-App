import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';
import { CalculateCacheService } from 'src/app/shared/services/calculate-cache.service';
import { Observable } from 'rxjs';

@Component({
    selector: 'ia-calculation',
    templateUrl: 'calculation.component.html',
    styleUrls: ['calculation.component.scss'],
})
export class CalculationComponent implements OnInit {

    userName: string;
    role: string;

    resultObs: Observable<number | null>;

    constructor(private titleService: Title,
        private authService: AuthenticationService,
        private dataService: CalculateCacheService,
    ) {
        this.titleService.setTitle('Calculation - Investments');

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

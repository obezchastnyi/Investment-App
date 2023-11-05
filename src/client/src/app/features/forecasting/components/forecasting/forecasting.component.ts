import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';
import { Observable } from 'rxjs';
import { ForecastCacheService } from 'src/app/shared/services/forecast-cache.service';

@Component({
    selector: 'ia-forecasting',
    templateUrl: 'forecasting.component.html',
    styleUrls: ['forecasting.component.scss'],
})
export class ForecastingComponent implements OnInit {

    userName: string;
    role: string;

    resultObs: Observable<number | null>;

    constructor(private titleService: Title,
        private authService: AuthenticationService,
        private dataService: ForecastCacheService,
    ) {
        this.titleService.setTitle('Forecasting - Investments');

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

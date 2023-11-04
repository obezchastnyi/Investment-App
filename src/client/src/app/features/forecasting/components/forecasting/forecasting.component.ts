import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';

@Component({
    selector: 'ia-forecasting',
    templateUrl: 'forecasting.component.html',
    styleUrls: ['forecasting.component.scss'],
})
export class ForecastingComponent implements OnInit {

    userName: string;
    role: string;

    constructor(private titleService: Title,
        private authService: AuthenticationService
    ) {
        this.titleService.setTitle('Forecasting - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
    }
}

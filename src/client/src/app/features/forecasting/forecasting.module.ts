import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ForecastingComponent } from '.';
import { ForecastingRoutingModule } from './forecasting-routing.module';

@NgModule({
    declarations: [
        ForecastingComponent,
    ],
    imports: [
        CommonModule,
        ForecastingRoutingModule,
        SharedModule,
    ],
})
export class ForecastingModule { }

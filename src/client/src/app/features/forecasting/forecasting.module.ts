import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ForecastingComponent } from '.';

@NgModule({
    declarations: [
        ForecastingComponent,
    ],
    imports: [
        CommonModule,
        ForecastingComponent,
        SharedModule,
    ],
})
export class ForecastingModule { }

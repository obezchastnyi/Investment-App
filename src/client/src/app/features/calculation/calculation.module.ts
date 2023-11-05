import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { CalculationComponent } from '.';
import { CalculationRoutingModule } from './calculation-routing.module';

@NgModule({
    declarations: [
        CalculationComponent,
    ],
    imports: [
        CommonModule,
        CalculationRoutingModule,
        SharedModule,
    ],
})
export class CalculationModule { }

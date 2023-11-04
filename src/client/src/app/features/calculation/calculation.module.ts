import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { CalculationComponent } from '.';

@NgModule({
    declarations: [
        CalculationComponent,
    ],
    imports: [
        CommonModule,
        CalculationComponent,
        SharedModule,
    ],
})
export class CalculationModule { }

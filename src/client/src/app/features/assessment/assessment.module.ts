import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { AssessmentComponent } from '.';

@NgModule({
    declarations: [
        AssessmentComponent,
    ],
    imports: [
        CommonModule,
        AssessmentComponent,
        SharedModule,
    ],
})
export class ForecastingModule { }

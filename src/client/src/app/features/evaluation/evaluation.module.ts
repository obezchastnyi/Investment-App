import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { EvaluationComponent } from '.';

@NgModule({
    declarations: [
        EvaluationComponent,
    ],
    imports: [
        CommonModule,
        EvaluationComponent,
        SharedModule,
    ],
})
export class EvaluationModule { }

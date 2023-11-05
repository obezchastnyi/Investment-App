import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { EvaluationComponent } from '.';
import { EvaluationRoutingModule } from './evaluation-routing.module';

@NgModule({
    declarations: [
        EvaluationComponent,
    ],
    imports: [
        CommonModule,
        EvaluationRoutingModule,
        SharedModule,
    ],
})
export class EvaluationModule { }

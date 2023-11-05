import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { AssessmentComponent } from '.';
import { AssessmentRoutingModule } from './assessment-routing.module';

@NgModule({
    declarations: [
        AssessmentComponent,
    ],
    imports: [
        CommonModule,
        AssessmentRoutingModule,
        SharedModule,
    ],
})
export class AssessmentModule { }

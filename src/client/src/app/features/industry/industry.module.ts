import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ExpertIndustryComponent, IndustryComponent } from '.';
import { IndustryRoutingModule } from './industry-routing.module';

@NgModule({
    declarations: [
        IndustryComponent,
        ExpertIndustryComponent
    ],
    imports: [
        CommonModule,
        IndustryRoutingModule,
        SharedModule,
    ],
})
export class IndustryModule { }

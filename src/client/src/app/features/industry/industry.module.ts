import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ExpertIndustryComponent, IndustryComponent } from '.';

@NgModule({
    declarations: [
        IndustryComponent,
        ExpertIndustryComponent
    ],
    imports: [
        CommonModule,
        IndustryComponent,
        ExpertIndustryComponent,
        SharedModule,
    ],
})
export class IndustryModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { CriteriaComponent, IndustryCriteriaComponent } from '.';

@NgModule({
    declarations: [
        CriteriaComponent,
        IndustryCriteriaComponent
    ],
    imports: [
        CommonModule,
        CriteriaComponent,
        IndustryCriteriaComponent,
        SharedModule,
    ],
})
export class CriteriaModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { CriteriaComponent, IndustryCriteriaComponent } from '.';
import { CriteriaRoutingModule } from './criteria-routing.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
    declarations: [
        CriteriaComponent,
        IndustryCriteriaComponent
    ],
    imports: [
        CommonModule,
        CriteriaRoutingModule,
        FontAwesomeModule,
        SharedModule,
    ],
})
export class CriteriaModule { }

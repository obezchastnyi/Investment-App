import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ProjectRoutingModule } from './project-routing.module';
import { ExpertProjectComponent, InvestorProjectComponent, PeriodComponent, PossibilityComponent, ProjectComponent } from '.';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
    declarations: [
        ProjectComponent,
        ExpertProjectComponent,
        InvestorProjectComponent,
        PeriodComponent,
        PossibilityComponent,
    ],
    imports: [
        CommonModule,
        ProjectRoutingModule,
        SharedModule,
        FontAwesomeModule,
    ],
})
export class ProjectModule { }

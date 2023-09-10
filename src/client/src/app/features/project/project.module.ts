import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ProjectRoutingModule } from './project-routing.module';
import { ProjectTableComponent } from '.';

@NgModule({
    declarations: [
        ProjectTableComponent,
    ],
    imports: [
        CommonModule,
        ProjectRoutingModule,
        SharedModule,
    ],
})
export class ProjectModule { }

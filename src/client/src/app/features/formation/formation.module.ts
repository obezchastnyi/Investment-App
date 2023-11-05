import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { FormationComponent } from '.';
import { FormationRoutingModule } from './formation-routing.module';

@NgModule({
    declarations: [
        FormationComponent,
    ],
    imports: [
        CommonModule,
        FormationRoutingModule,
        SharedModule,
    ],
})
export class FormationModule { }

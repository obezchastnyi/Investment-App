import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { FormationComponent } from '.';

@NgModule({
    declarations: [
        FormationComponent,
    ],
    imports: [
        CommonModule,
        FormationComponent,
        SharedModule,
    ],
})
export class FormationModule { }

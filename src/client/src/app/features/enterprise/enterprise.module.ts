import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { EnterpriseComponent } from '.';

@NgModule({
    declarations: [
        EnterpriseComponent,
    ],
    imports: [
        CommonModule,
        EnterpriseComponent,
        SharedModule,
    ],
})
export class EnterpriseModule { }

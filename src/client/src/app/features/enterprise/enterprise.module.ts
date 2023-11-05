import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { EnterpriseComponent } from '.';
import { EnterpriseRoutingModule } from './enterprise-routing.module';

@NgModule({
    declarations: [
        EnterpriseComponent,
    ],
    imports: [
        CommonModule,
        EnterpriseRoutingModule,
        SharedModule,
    ],
})
export class EnterpriseModule { }

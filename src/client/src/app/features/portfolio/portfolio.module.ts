import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { PortfolioRoutingModule } from './portfolio-routing.module';
import { PortfolioTableComponent } from '.';

@NgModule({
    declarations: [
        PortfolioTableComponent,
    ],
    imports: [
        CommonModule,
        PortfolioRoutingModule,
        SharedModule,
    ],
})
export class PortfolioModule { }

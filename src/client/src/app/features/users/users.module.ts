import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { UsersRoutingModule } from './users-routing.module';
import { UsersTableComponent } from '.';

@NgModule({
    declarations: [
        UsersTableComponent,
    ],
    imports: [
        CommonModule,
        UsersRoutingModule,
        SharedModule,
    ],
})
export class UsersModule { }

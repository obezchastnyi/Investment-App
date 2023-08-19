import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { DataTableComponent, DataTableColumnComponent, DataTableColumnTemplateWrapperComponent, CustomCheckboxComponent } from './components';

@NgModule({
    declarations: [
        CustomCheckboxComponent,
        DataTableComponent,
        DataTableColumnComponent,
        DataTableColumnTemplateWrapperComponent,
    ],
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        NgxDatatableModule,
        RouterModule,
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule,
        DataTableComponent,
        CustomCheckboxComponent
    ],
})
export class SharedModule {}

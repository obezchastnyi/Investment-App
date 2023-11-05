import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { DataTableComponent, DataTableColumnComponent, DataTableColumnTemplateWrapperComponent } from './components';
import { EnterpriseCacheService } from './services/enterprise-cache.service';

@NgModule({
    declarations: [
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
    providers: [
        EnterpriseCacheService,
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule,
        DataTableComponent,
    ],
})
export class SharedModule {}

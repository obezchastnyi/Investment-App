import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectTableComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: ProjectTableComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ProjectRoutingModule {
}

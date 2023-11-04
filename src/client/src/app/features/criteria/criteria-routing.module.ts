import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CriteriaComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: CriteriaComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class CriteriaRoutingModule {
}

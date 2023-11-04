import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndustryComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: IndustryComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class IndustryRoutingModule {
}

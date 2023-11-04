import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EvaluationComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: EvaluationComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EvaluationRoutingModule {
}

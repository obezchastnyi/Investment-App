import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssessmentComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: AssessmentComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ForecastingRoutingModule {
}

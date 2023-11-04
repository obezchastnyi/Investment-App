import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForecastingComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: ForecastingComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ForecastingRoutingModule {
}

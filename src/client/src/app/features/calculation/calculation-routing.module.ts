import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalculationComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: CalculationComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class CalculationRoutingModule {
}

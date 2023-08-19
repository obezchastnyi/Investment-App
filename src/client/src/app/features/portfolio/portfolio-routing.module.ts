import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PortfolioTableComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: PortfolioTableComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class PortfolioRoutingModule {
}

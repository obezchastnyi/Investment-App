import { NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersTableComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: UsersTableComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UsersRoutingModule {
}

import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { MainViewComponent } from './core/layout/components/main-view/main-view.component';
import { AuthenticationComponent } from './core/authentication';
import { AuthenticatedGuard, NotAuthenticatedGuard } from './core/guards';
import { PortfolioTableComponent } from './features/portfolio';
import { UsersTableComponent } from './features/users';
import { PortfolioModule } from './features/portfolio/portfolio.module';
import { UsersModule } from './features/users/users.module';

export function getPortfolioModule(): any {
    return PortfolioModule;
}

export function getUsersModule(): any {
    return UsersModule;
}

export const appRoutes: Routes = [
    {
        path: 'login',
        component: AuthenticationComponent,
        canActivate: [NotAuthenticatedGuard],
    },
    {
        path: '',
        component: MainViewComponent,
        canActivate: [AuthenticatedGuard],
        data : {
            redirectUrl : `/login`,
        },
    },
    {
        path: 'portfolio',
        component: PortfolioTableComponent,
        canActivate: [AuthenticatedGuard],
        data : {
            redirectUrl : `/login`,
        },
    },
    {
        path: 'users',
        component: UsersTableComponent,
        canActivate: [AuthenticatedGuard],
        data : {
            redirectUrl : `/login`,
        },
    },
    {
        path: '**',
        redirectTo: '',
    },
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes, {
        enableTracing: false,
        preloadingStrategy: PreloadAllModules,
    })],
    exports: [RouterModule],
})
export class AppRoutingModule { }

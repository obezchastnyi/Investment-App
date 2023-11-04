import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { MainViewComponent } from './core/layout/components/main-view/main-view.component';
import { AuthenticationComponent } from './core/authentication';
import { AuthenticatedGuard, NotAuthenticatedGuard } from './core/guards';
import { ExpertProjectComponent, InvestorProjectComponent, PeriodComponent, PossibilityComponent, ProjectComponent } from './features/project';
import { UsersTableComponent } from './features/users';
import { ProjectModule } from './features/project/project.module';
import { UsersModule } from './features/users/users.module';
import { UserPageComponent } from './core/layout';
import { CalculationComponent } from './features/calculation';
import { ForecastingComponent } from './features/forecasting';
import { AssessmentComponent } from './features/assessment';
import { EvaluationComponent } from './features/evaluation';
import { FormationComponent } from './features/formation';
import { EnterpriseComponent } from './features/enterprise';
import { CriteriaComponent, IndustryCriteriaComponent } from './features/criteria';
import { ExpertIndustryComponent, IndustryComponent } from './features/industry';

export function getProjectModule(): any {
    return ProjectModule;
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
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'project',
        component: ProjectComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'expert-project',
        component: ExpertProjectComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'investor-project',
        component: InvestorProjectComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'period',
        component: PeriodComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'possibility',
        component: PossibilityComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'enterprise',
        component: EnterpriseComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'criteria',
        component: CriteriaComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'industry-criteria',
        component: IndustryCriteriaComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'industry',
        component: IndustryComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'expert-industry',
        component: ExpertIndustryComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'calculation',
        component: CalculationComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'forecasting',
        component: ForecastingComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'assessment',
        component: AssessmentComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'evaluation',
        component: EvaluationComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'formation',
        component: FormationComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'user-page',
        component: UserPageComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
        },
    },
    {
        path: 'users',
        component: UsersTableComponent,
        canActivate: [AuthenticatedGuard],
        data: {
            redirectUrl: `/login`,
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

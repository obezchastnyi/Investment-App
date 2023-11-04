import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from '../app-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AuthorizationInterceptor } from './interceptors';
import { AuthenticationComponent } from './authentication';
import { HeaderComponent, MainViewComponent, UserPageComponent } from './layout';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
    declarations: [
        UserPageComponent,
        MainViewComponent,
        HeaderComponent,
        AuthenticationComponent,
    ],
    imports: [
        FontAwesomeModule,
        RouterModule,
        AppRoutingModule,
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        SharedModule,
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthorizationInterceptor,
            multi: true,
        },
    ],
    exports: [
        UserPageComponent,
        MainViewComponent,
        HeaderComponent,
        AuthenticationComponent,
        RouterModule,
        AppRoutingModule,
        SharedModule,
    ],
})
export class CoreModule {}

import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/services';

@Injectable({
    providedIn: 'root',
})
export class AuthorizationInterceptor implements HttpInterceptor {

    constructor(
        private authService: AuthenticationService,
    ) {}

    intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const modifiedReq = req.clone({
            //body:
        });

        return next.handle(modifiedReq);
    }
}

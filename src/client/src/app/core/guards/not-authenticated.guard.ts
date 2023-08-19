import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/shared/services';

@Injectable({
    providedIn: 'root',
})
export class NotAuthenticatedGuard  {

    constructor(private router: Router, private authService: AuthenticationService){ }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
        return this.authService.isAuthenticated()
            ? this.router.parseUrl('')
            : true;
    }
}

import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Router } from "@angular/router";
import { APP_SETTINGS } from "../utils";

@Injectable({
    providedIn: 'root',
})
export class AuthenticationService {

    private authenticated = false;
    userName: string;
    role: string;

    constructor(private http: HttpClient, private router: Router) {
        this.authenticated = localStorage.getItem('authenticated') == 'true';
        this.userName = localStorage.getItem('userName');
        this.role = localStorage.getItem('role');
    }

    isAuthenticated(): boolean {
        return this.authenticated;
    }

    authenticate(userName: string, password: string): void {
        let params = new HttpParams()
            .append('UserName', userName)
            .append('Password', password);

        this.http.get(`${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}user/auth`, { responseType:'text', params:params, observe: 'response' })
            .subscribe((response: any)  => {
               if (response.status === 200) {
                   this.authenticated = true;
                   this.userName = userName;
                   this.role = response.body;

                   this.setLocalStorage();

                   this.router.navigate([''])
                       .then(() => {
                           window.location.reload();
                       });
                   return;
               }
            },
                err => {
                    alert('Login Failed! UserName or Password are incorrect.');
                    /*setTimeout(function() {
                    }, 1000);*/
                });
    }

    logout(): void {
        this.authenticated = false;
        this.userName = undefined;

        localStorage.clear();

        this.router.navigate([''])
            .then(() => {
                window.location.reload();
            });
    }

    private setLocalStorage() {
        localStorage.setItem('authenticated', String(this.authenticated));
        localStorage.setItem('userName', this.userName);
        localStorage.setItem('role', this.role);
    }
}

import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { AuthenticationService } from 'src/app/shared/services';

@Component({
    selector: 'ia-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {

    faUser = faUser;

    isAuthenticated: boolean;
    userName: string;
    role: string;

    currentUrl: string;

    constructor(private authService: AuthenticationService, router: Router) {
        this.isAuthenticated = this.authService.isAuthenticated();
        this.userName = this.authService.userName;
        this.role = this.authService.role;

        router.events
            .subscribe(event => {
                if (event instanceof NavigationEnd) {
                    this.currentUrl = event.url;
                }
            });
    }

    logout(): void {
        if (confirm('Do you really want to log out from Investment App?')) {
            this.authService.logout();
        }
    }

    onToggle() {
        let menu = document.getElementById("wrapper");
        if (menu.classList.contains('toggled')) {
            menu.classList.remove('toggled');
        } else {
            menu.classList.add('toggled');
        }
    }
}

import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services';

@Component({
    selector: 'ia-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {

    isAuthenticated: boolean;
    userName: string;
    role: string;

    constructor(private authService: AuthenticationService) {
        this.isAuthenticated = this.authService.isAuthenticated();
        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    logout(): void {
        if (confirm('Do you really want to log out from Investment App?')) {
            this.authService.logout();
        }
    }
}

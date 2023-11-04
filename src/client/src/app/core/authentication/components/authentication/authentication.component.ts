import { Component } from '@angular/core';
import { faCoffee, faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { AuthenticationService } from 'src/app/shared/services';

@Component({
    templateUrl: './authentication.component.html',
    styleUrls: ['./authentication.component.scss'],
})
export class AuthenticationComponent {

    showPassword = false;
    faEye = faEye;
    faEyeSlash = faEyeSlash;
    
    isAuthenticated: boolean;

    userName = '';
    userNameValidationError = '';

    password = '';
    passwordValidationError = '';

    constructor(private authService: AuthenticationService) {
        this.isAuthenticated = this.authService.isAuthenticated();
    }

    auth(): void {
        if (this.validateInputs()) {
            this.authService.authenticate(this.userName, this.password);
        }
    }

    private validateInputs(): boolean {
        let valid = true;

        if (this.userName === '') {
            this.userNameValidationError = 'UserName is empty';
            valid = false;
        } else if (this.userName.length < 3) {
            this.userNameValidationError = 'UserName is short';
            valid = false;
        }

        if (this.password === '') {
            this.passwordValidationError = 'Password is empty';
            valid = false;
        } else if (this.password.length < 3) {
            this.passwordValidationError = 'Password is short';
            valid = false;
        }

        return valid;
    }

    onShowPassword() {
        let input = document.getElementById("inputPassword");

        if (input.getAttribute('type') === "password") {
            input.setAttribute('type', 'text');
            this.showPassword = true;
        } else {
            input.setAttribute('type', 'password');
            this.showPassword = false;
        }
    }
}

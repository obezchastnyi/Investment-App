import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../../../shared/services';
import { Observable } from 'rxjs';
import { FormCacheService } from 'src/app/shared/services/form-cache.service';

@Component({
    selector: 'ia-formation',
    templateUrl: 'formation.component.html',
    styleUrls: ['formation.component.scss'],
})
export class FormationComponent implements OnInit {

    userName: string;
    role: string;

    resultObs: Observable<number | null>;

    constructor(private titleService: Title,
        private authService: AuthenticationService,
        private dataService: FormCacheService,
    ) {
        this.titleService.setTitle('Formation - Investments');

        this.userName = this.authService.userName;
        this.role = this.authService.role;
    }

    ngOnInit(): void {
        this.resultObs = this.dataService.getTableRows();
    }

    onCalculate() {
        this.dataService.loadDataFromApi();
    }
}

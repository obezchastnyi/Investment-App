import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { Project, ProjectRow } from 'src/app/models';
import { APP_SETTINGS } from '../utils';
import { Enterprise, EnterpriseRow } from 'src/app/models/enterprise';
import { EnterpriseCacheService } from './enterprise-cache.service';

@Injectable({
    providedIn: 'root',
})
export class ProjectCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    public loadedProjectsSub = new BehaviorSubject<ProjectRow[] | null>(null);
    private loadedEnterprisesSub = new BehaviorSubject<EnterpriseRow[] | null>(null);

    constructor(private http: HttpClient, enterpriseCacheService: EnterpriseCacheService) {
        this.loadedEnterprisesSub = enterpriseCacheService.loadedEnterprisesSub;
        this.loadDataFromApi();
    }

    getTableRows(): Observable<ProjectRow[]> {
        return this.loadedProjectsSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    getEnterprises(): Observable<EnterpriseRow[]> {
        return this.loadedEnterprisesSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    addNewTableRow() {
        this.addNewDataToApi({
            id: '',
            name: `New Item ${this.loadedProjectsSub.value.length + 1}`,
            startingInvestmentSum: 0,
            enterprise: this.loadedEnterprisesSub.getValue()[0].name,
            enterpriseId: this.loadedEnterprisesSub.getValue()[0].id,
            internalId: ''
        });
    }

    updateTableRow(project: ProjectRow) {
        let row = this.loadedProjectsSub.value
            .find(p => p.id == project.id);

        row.startingInvestmentSum = project.startingInvestmentSum;
        row.name = project.name;
        row.enterprise = project.enterprise;

        this.updateDataByApi(project);
    }

    confirmAllUpdates(projects: ProjectRow[]): void {
        this.updateCollectionOfDataByApi(projects);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(project: ProjectRow): void {
        this.http.put(`${this.serverUrl}project`, {
            id: project.id,
            name: project.name,
            startingInvestmentSum: project.startingInvestmentSum,
            enterpriseId: this.loadedEnterprisesSub.value.find(e => e.name === project.enterprise).id,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(projects: ProjectRow[]): void {
        let body: any[] = [];
        projects.forEach(p => {
            body.push({
                id: p.id,
                name: p.name,
                startingInvestmentSum: p.startingInvestmentSum,
                enterpriseId: this.loadedEnterprisesSub.value.find(e => e.name === p.enterprise).id,
            })
        })

        this.http.put(`${this.serverUrl}project/all-update`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}project/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(project: ProjectRow) {
        const body = {
            id: project.id,
            name: project.name,
            startingInvestmentSum: project.startingInvestmentSum,
        };
        this.http.post(`${this.serverUrl}project`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
            err => {
                alert('[addNewDataToApi] failed');
            });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}project/all`) as Observable<Project[]>,
        ]).subscribe(([projects]) => {
            let data: ProjectRow[] = projects.map(p => ({
                id: p.id.toString(),
                internalId: `${p.id.toString().split('-')[0]}***`,
                name: p.name,
                startingInvestmentSum: p.startingInvestmentSum,
                enterprise: p.enterprise.name,
                enterpriseId: p.enterpriseId.toString(),
            }));

            this.loadedProjectsSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

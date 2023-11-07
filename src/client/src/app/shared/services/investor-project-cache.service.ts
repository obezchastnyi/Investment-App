import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { ProjectRow } from 'src/app/models';
import { APP_SETTINGS } from '../utils';
import { ProjectCacheService } from './project-cache.service';
import { InvestorProject, InvestorProjectRow } from 'src/app/models/investor-project';

@Injectable({
    providedIn: 'root',
})
export class InvestorProjectCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    private loadedInvestorProjectsSub = new BehaviorSubject<InvestorProjectRow[] | null>(null);
    private loadedProjectsSub = new BehaviorSubject<ProjectRow[] | null>(null);

    constructor(private http: HttpClient, projectCacheService: ProjectCacheService) {
        this.loadedProjectsSub = projectCacheService.loadedProjectsSub;
        this.loadDataFromApi();
    }

    getTableRows(): Observable<InvestorProjectRow[]> {
        return this.loadedInvestorProjectsSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    getProjects(): Observable<ProjectRow[]> {
        return this.loadedProjectsSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    addNewTableRow() {
        this.addNewDataToApi({
            id: '',
            minIncomeRate: 0,
            maxRiskRate: 0,
            project: this.loadedProjectsSub.getValue()[0].name,
            projectId: this.loadedProjectsSub.getValue()[0].id,
            internalId: ''
        });
    }

    updateTableRow(project: InvestorProjectRow) {
        let row = this.loadedInvestorProjectsSub.value
            .find(p => p.id == project.id);

        row.minIncomeRate = project.minIncomeRate;
        row.maxRiskRate = project.maxRiskRate;
        row.project = project.project;

        this.updateDataByApi(project);
    }

    confirmAllUpdates(projects: InvestorProjectRow[]): void {
        this.updateCollectionOfDataByApi(projects);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(project: InvestorProjectRow): void {
        this.http.put(`${this.serverUrl}investor/project`, {
            id: project.id,
            minIncomeRate: project.minIncomeRate,
            maxRiskRate: project.maxRiskRate,
            projectId: this.loadedProjectsSub.value.find(e => e.name === project.projectId).id,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(projects: InvestorProjectRow[]): void {
        let body: any[] = [];
        projects.forEach(p => {
            body.push({
                id: p.id,
                minIncomeRate: p.minIncomeRate,
                maxRiskRate: p.maxRiskRate,
                projectId: this.loadedProjectsSub.value.find(e => e.name === p.projectId).id,
            })
        })

        this.http.put(`${this.serverUrl}investor/project/all-update`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}investor/project/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(project: InvestorProjectRow) {
        const body = {
            id: project.id,
            minIncomeRate: project.minIncomeRate,
            maxRiskRate: project.maxRiskRate,
        };
        this.http.post(`${this.serverUrl}investor/project`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[addNewDataToApi] failed');
                });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}investor/project/all`) as Observable<InvestorProject[]>,
        ]).subscribe(([projects]) => {
            let data: InvestorProjectRow[] = projects.map(p => ({
                id: p.id.toString(),
                internalId: `${p.id.toString().split('-')[0]}***`,
                minIncomeRate: p.minIncomeRate,
                maxRiskRate: p.maxRiskRate,
                project: p.project.name,
                projectId: p.projectId.toString(),
            }));

            this.loadedInvestorProjectsSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

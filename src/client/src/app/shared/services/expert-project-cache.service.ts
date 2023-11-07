import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { PeriodRow, PossibilityRow, ProjectRow } from 'src/app/models';
import { APP_SETTINGS } from '../utils';
import { ProjectCacheService } from './project-cache.service';
import { ExpertProject, ExpertProjectRow } from 'src/app/models/expert-project';
import { PeriodCacheService } from './period-cache.service';
import { PossibilityCacheService } from './possibility-cache.service';
import { DatePipe } from '@angular/common';

@Injectable({
    providedIn: 'root',
})
export class ExpertProjectCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    private loadedExpertProjectsSub = new BehaviorSubject<ExpertProjectRow[] | null>(null);

    private loadedProjectsSub = new BehaviorSubject<ProjectRow[] | null>(null);
    private loadedPeriodsSub = new BehaviorSubject<PeriodRow[] | null>(null);
    private loadedPossibilitiesSub = new BehaviorSubject<PossibilityRow[] | null>(null);

    constructor(
        private http: HttpClient,
        projectCacheService: ProjectCacheService,
        periodCacheService: PeriodCacheService,
        possibilityCacheService: PossibilityCacheService,
        private datePipe: DatePipe,
    ) {
        this.loadedProjectsSub = projectCacheService.loadedProjectsSub;
        this.loadedPeriodsSub = periodCacheService.loadedPeriodsSub;
        this.loadedPossibilitiesSub = possibilityCacheService.loadedPossibilitiesSub;

        this.loadDataFromApi();
    }

    getTableRows(): Observable<ExpertProjectRow[]> {
        return this.loadedExpertProjectsSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    getProjects(): Observable<ProjectRow[]> {
        return this.loadedProjectsSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    getPeriods(): Observable<PeriodRow[]> {
        return this.loadedPeriodsSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    getPossibilities(): Observable<PossibilityRow[]> {
        return this.loadedPossibilitiesSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    addNewTableRow() {
        this.addNewDataToApi({
            id: '',
            cashFlowRate: 0,
            period: `${this.datePipe.transform(this.loadedPeriodsSub.getValue()[0].startDate)} - ${this.datePipe.transform(this.loadedPeriodsSub.getValue()[0].endDate)}`,
            periodId: this.loadedPeriodsSub.getValue()[0].id,
            possibility: this.loadedPossibilitiesSub.getValue()[0].rate.toString(),
            possibilityId: this.loadedPossibilitiesSub.getValue()[0].id,
            project: this.loadedProjectsSub.getValue()[0].name,
            projectId: this.loadedProjectsSub.getValue()[0].id,
            internalId: ''
        });
    }

    updateTableRow(project: ExpertProjectRow) {
        let row = this.loadedExpertProjectsSub.value
            .find(p => p.id == project.id);

        row.cashFlowRate = project.cashFlowRate;
        //row.period = project.period;
        row.possibility = project.possibility;
        row.project = project.project;

        this.updateDataByApi(project);
    }

    confirmAllUpdates(projects: ExpertProjectRow[]): void {
        this.updateCollectionOfDataByApi(projects);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(project: ExpertProjectRow): void {
        this.http.put(`${this.serverUrl}expert/project`, {
            id: project.id,
            cashFlowRate: project.cashFlowRate,
            //periodId: ,
            possibilityId: this.loadedPossibilitiesSub.value.find(e => e.rate.toString() === project.possibility).id,
            projectId: this.loadedProjectsSub.value.find(e => e.name === project.projectId).id,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(projects: ExpertProjectRow[]): void {
        let body: any[] = [];
        projects.forEach(p => {
            body.push({
                id: p.id,
                cashFlowRate: p.cashFlowRate,
                //periodId: ,
                possibilityId: this.loadedPossibilitiesSub.value.find(e => e.rate.toString() === p.possibility).id,
                projectId: this.loadedProjectsSub.value.find(e => e.name === p.projectId).id,
            })
        })

        this.http.put(`${this.serverUrl}expert/project/all-update`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}expert/project/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(project: ExpertProjectRow) {
        const body = {
            id: project.id,
            cashFlowRate: project.cashFlowRate,
        };
        this.http.post(`${this.serverUrl}expert/project`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[addNewDataToApi] failed');
                });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}expert/project/all`) as Observable<ExpertProject[]>,
        ]).subscribe(([projects]) => {
            let data: ExpertProjectRow[] = projects.map(p => ({
                id: p.id.toString(),
                internalId: `${p.id.toString().split('-')[0]}***`,
                cashFlowRate: p.cashFlowRate,
                project: p.project.name,
                projectId: p.projectId.toString(),
                period: `${this.datePipe.transform(p.period?.startDate ?? Date.now())} - ${this.datePipe.transform(p.period?.endDate ?? Date.now())}`,
                periodId: p.periodId.toString(),
                possibility: p.possibility?.rate.toString(),
                possibilityId: p.possibilityId.toString(),
            }));

            this.loadedExpertProjectsSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { Project, ProjectRow } from 'src/app/models';
import { APP_SETTINGS } from '../utils';

@Injectable({
    providedIn: 'root',
})
export class ProjectCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    private loadedProjectsSub = new BehaviorSubject<ProjectRow[] | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<ProjectRow[]> {
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
            name: `New Item ${this.loadedProjectsSub.value.length + 1}`,
            sum: 0
        });
    }

    updateTableRow(project: ProjectRow) {
        let row = this.loadedProjectsSub.value
            .find(p => p.id == project.id);

        row.sum = project.sum;
        row.name = project.name;

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
            sum: project.sum,
        }).subscribe(()  => {},
        err => {
            alert('[updateDataByApi] failed');
        });
    }

    private updateCollectionOfDataByApi(projects: ProjectRow[]): void {
        this.http.put(`${this.serverUrl}project/all-update`, projects)
            .subscribe(()  => {
                this.loadDataFromApi();
            },
            err => {
                alert('Failed update');
            });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}project/${id}`)
            .subscribe(()  => {
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
            sum: project.sum,
        };
        this.http.post(`${this.serverUrl}project`, {
            id: project.id,
            name: project.name,
            sum: project.sum,
        }).subscribe(()  => {
            this.loadDataFromApi();
        },
        err => {
            alert('[addNewDataToApi] failed');
        });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}project/all`) as Observable<Project[]>
        ]).subscribe(([projects]) => {
            let data: ProjectRow[] = projects.map(p => ({
                id: p.id.toString(),
                name: p.name,
                sum: p.sum,
            }));
            this.loadedProjectsSub.next(data);
        },
        err => {
            alert('[getDataFromApi] failed');
        });
    }
}

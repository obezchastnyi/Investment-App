import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { APP_SETTINGS } from '../utils';
import { Criteria, CriteriaRow } from 'src/app/models/criteria';

@Injectable({
    providedIn: 'root',
})
export class CriteriaCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    public loadedCriteriasSub = new BehaviorSubject<CriteriaRow[] | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<CriteriaRow[]> {
        return this.loadedCriteriasSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    addNewTableRow() {
        this.addNewDataToApi({
            id: '',
            name: `New Item ${this.loadedCriteriasSub.value.length + 1}`,
            internalId: ''
        });
    }

    updateTableRow(criteria: CriteriaRow) {
        let row = this.loadedCriteriasSub.value
            .find(p => p.id == criteria.id);

        row.name = criteria.name;

        this.updateDataByApi(criteria);
    }

    confirmAllUpdates(possibilities: CriteriaRow[]): void {
        this.updateCollectionOfDataByApi(possibilities);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(criteria: CriteriaRow): void {
        this.http.put(`${this.serverUrl}criteria`, {
            id: criteria.id,
            name: criteria.name,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(criterias: CriteriaRow[]): void {
        let body: any[] = [];
        criterias.forEach(p => {
            body.push({
                id: p.id,
                name: p.name,
            })
        })

        this.http.put(`${this.serverUrl}criteria/all-update`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}criteria/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(possibility: CriteriaRow) {
        const body = {
            id: possibility.id,
            name: possibility.name,
        };
        this.http.post(`${this.serverUrl}criteria`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[addNewDataToApi] failed');
                });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}criteria/all`) as Observable<Criteria[]>
        ]).subscribe(([possibilities]) => {
            let data: CriteriaRow[] = possibilities.map(p => ({
                id: p.id.toString(),
                internalId: `${p.id.toString().split('-')[0]}***`,
                name: p.name,
            }));

            this.loadedCriteriasSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

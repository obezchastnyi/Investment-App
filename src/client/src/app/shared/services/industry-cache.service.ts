import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { APP_SETTINGS } from '../utils';
import { Industry, IndustryRow } from 'src/app/models/industry';

@Injectable({
    providedIn: 'root',
})
export class IndustryCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    public loadedIndustriesSub = new BehaviorSubject<IndustryRow[] | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<IndustryRow[]> {
        return this.loadedIndustriesSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    addNewTableRow() {
        this.addNewDataToApi({
            id: '',
            name: `New Item ${this.loadedIndustriesSub.value.length + 1}`,
            internalId: ''
        });
    }

    updateTableRow(criteria: IndustryRow) {
        let row = this.loadedIndustriesSub.value
            .find(p => p.id == criteria.id);

        row.name = criteria.name;

        this.updateDataByApi(criteria);
    }

    confirmAllUpdates(possibilities: IndustryRow[]): void {
        this.updateCollectionOfDataByApi(possibilities);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(criteria: IndustryRow): void {
        this.http.put(`${this.serverUrl}industry`, {
            id: criteria.id,
            name: criteria.name,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(criterias: IndustryRow[]): void {
        let body: any[] = [];
        criterias.forEach(p => {
            body.push({
                id: p.id,
                name: p.name,
            })
        })

        this.http.put(`${this.serverUrl}industry/all-update`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}industry/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(possibility: IndustryRow) {
        const body = {
            id: possibility.id,
            name: possibility.name,
        };
        this.http.post(`${this.serverUrl}industry`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[addNewDataToApi] failed');
                });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}industry/all`) as Observable<Industry[]>
        ]).subscribe(([possibilities]) => {
            let data: IndustryRow[] = possibilities.map(p => ({
                id: p.id.toString(),
                internalId: `${p.id.toString().split('-')[0]}***`,
                name: p.name,
            }));

            this.loadedIndustriesSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

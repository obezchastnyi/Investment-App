import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { APP_SETTINGS } from '../utils';
import { Possibility, PossibilityRow } from 'src/app/models';

@Injectable({
    providedIn: 'root',
})
export class PossibilityCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    public loadedPossibilitiesSub = new BehaviorSubject<PossibilityRow[] | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<PossibilityRow[]> {
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
            rate: 0,
            internalId: ''
        });
    }

    updateTableRow(possibility: PossibilityRow) {
        let row = this.loadedPossibilitiesSub.value
            .find(p => p.id == possibility.id);

        row.rate = possibility.rate;

        this.updateDataByApi(possibility);
    }

    confirmAllUpdates(possibilities: PossibilityRow[]): void {
        this.updateCollectionOfDataByApi(possibilities);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(possibility: PossibilityRow): void {
        this.http.put(`${this.serverUrl}possibility`, {
            id: possibility.id,
            rate: possibility.rate,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(possibilities: PossibilityRow[]): void {
        let body: any[] = [];
        possibilities.forEach(p => {
            body.push({
                id: p.id,
                rate: p.rate,
            })
        })

        this.http.put(`${this.serverUrl}possibility/all-update`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}possibility/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(possibility: PossibilityRow) {
        const body = {
            id: possibility.id,
            rate: possibility.rate,
        };
        this.http.post(`${this.serverUrl}possibility`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[addNewDataToApi] failed');
                });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}possibility/all`) as Observable<Possibility[]>
        ]).subscribe(([possibilities]) => {
            let data: PossibilityRow[] = possibilities.map(p => ({
                id: p.id.toString(),
                internalId: `${p.id.toString().split('-')[0]}***`,
                rate: p.rate,
            }));

            this.loadedPossibilitiesSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

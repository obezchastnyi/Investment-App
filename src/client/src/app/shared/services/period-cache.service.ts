import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { APP_SETTINGS } from '../utils';
import { Period, PeriodRow } from 'src/app/models';

@Injectable({
    providedIn: 'root',
})
export class PeriodCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    public loadedPeriodsSub = new BehaviorSubject<PeriodRow[] | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<PeriodRow[]> {
        return this.loadedPeriodsSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    addNewTableRow() {
        this.addNewDataToApi({
            id: '',
            startDate: new Date().toISOString(),
            endDate: new Date().toISOString(),
            discountRate: 0,
            riskFreeDiscountRate: 0,
            internalId: ''
        });
    }

    updateTableRow(period: PeriodRow) {
        let row = this.loadedPeriodsSub.value
            .find(p => p.id == period.id);

        //row.startDate = period.startDate;
        //row.endDate = period.endDate;
        row.discountRate = period.discountRate;
        row.riskFreeDiscountRate = period.riskFreeDiscountRate;

        this.updateDataByApi(period);
    }

    confirmAllUpdates(periods: PeriodRow[]): void {
        this.updateCollectionOfDataByApi(periods);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(period: PeriodRow): void {
        this.http.put(`${this.serverUrl}enterprise`, {
            id: period.id,
            //startDate: period.startDate,
            //endDate: period.endDate,
            discountRate: period.discountRate,
            riskFreeDiscountRate: period.riskFreeDiscountRate,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(periods: PeriodRow[]): void {
        let body: any[] = [];
        periods.forEach(p => {
            body.push({
                id: p.id,
                //startDate: p.startDate,
                //endDate: p.endDate,
                discountRate: p.discountRate,
                riskFreeDiscountRate: p.riskFreeDiscountRate,
            })
        })

        this.http.put(`${this.serverUrl}period/all-update`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}period/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(period: PeriodRow) {
        const body = {
            id: period.id,
            startDate: period.startDate,
            endDate: period.endDate,
            discountRate: period.discountRate,
            riskFreeDiscountRate: period.riskFreeDiscountRate,
        };
        this.http.post(`${this.serverUrl}period`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[addNewDataToApi] failed');
                });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}period/all`) as Observable<Period[]>
        ]).subscribe(([enterprises]) => {
            let data: PeriodRow[] = enterprises.map(e => ({
                id: e.id.toString(),
                internalId: `${e.id.toString().split('-')[0]}***`,
                startDate: e.startDate.toString(),
                endDate: e.endDate?.toString() ?? 'Indefinitely',
                discountRate: e.discountRate,
                riskFreeDiscountRate: e.riskFreeDiscountRate,
            }));

            this.loadedPeriodsSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

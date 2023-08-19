import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { Portfolio, PortfolioRow } from 'src/app/models';
import { APP_SETTINGS } from '../utils';

@Injectable({
    providedIn: 'root',
})
export class PortfolioCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    private loadedPortfoliosSub = new BehaviorSubject<PortfolioRow[] | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<PortfolioRow[]> {
        return this.loadedPortfoliosSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    addNewTableRow() {
        this.addNewDataToApi({
            id: '',
            name: `New Item ${this.loadedPortfoliosSub.value.length + 1}`,
            sum: 0
        });
    }

    updateTableRow(portfolio: PortfolioRow) {
        let row = this.loadedPortfoliosSub.value
            .find(p => p.id == portfolio.id);

        row.sum = portfolio.sum;
        row.name = portfolio.name;

        this.updateDataByApi(portfolio);
    }

    confirmAllUpdates(portfolios: PortfolioRow[]): void {
        this.updateCollectionOfDataByApi(portfolios);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(portfolio: PortfolioRow): void {
        this.http.put(`${this.serverUrl}portfolio`, {
            id: portfolio.id,
            name: portfolio.name,
            sum: portfolio.sum,
        }).subscribe(()  => {},
        err => {
            alert('[updateDataByApi] failed');
        });
    }

    private updateCollectionOfDataByApi(portfolios: PortfolioRow[]): void {
        this.http.put(`${this.serverUrl}portfolio/all-update`, portfolios)
            .subscribe(()  => {
                this.loadDataFromApi();
            },
            err => {
                alert('Failed update');
            });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}portfolio/${id}`)
            .subscribe(()  => {
                this.loadDataFromApi();
            },
            err => {
                alert('[deleteRow] failed');
            });
    }

    private addNewDataToApi(portfolio: PortfolioRow) {
        const body = {
            id: portfolio.id,
            name: portfolio.name,
            sum: portfolio.sum,
        };
        this.http.post(`${this.serverUrl}portfolio`, {
            id: portfolio.id,
            name: portfolio.name,
            sum: portfolio.sum,
        }).subscribe(()  => {
            this.loadDataFromApi();
        },
        err => {
            alert('[addNewDataToApi] failed');
        });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}portfolio/all`) as Observable<Portfolio[]>
        ]).subscribe(([portfolios]) => {
            let data: PortfolioRow[] = portfolios.map(p => ({
                id: p.id.toString(),
                name: p.name,
                sum: p.sum,
            }));
            this.loadedPortfoliosSub.next(data);
        },
        err => {
            alert('[getDataFromApi] failed');
        });
    }
}

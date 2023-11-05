import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { APP_SETTINGS } from '../utils';
import { Enterprise, EnterpriseRow } from 'src/app/models/enterprise';

@Injectable({
    providedIn: 'root',
})
export class EnterpriseCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    public loadedEnterprisesSub = new BehaviorSubject<EnterpriseRow[] | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<EnterpriseRow[]> {
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
            name: `New Item ${this.loadedEnterprisesSub.value.length + 1}`,
            address: `New Item ${this.loadedEnterprisesSub.value.length + 1}`,
            bankAccount: `New Item ${this.loadedEnterprisesSub.value.length + 1}`,
            taxNumber: 0,
            internalId: ''
        });
    }

    updateTableRow(enterprise: EnterpriseRow) {
        let row = this.loadedEnterprisesSub.value
            .find(p => p.id == enterprise.id);

        row.name = enterprise.name;
        row.bankAccount = enterprise.bankAccount;
        row.address = enterprise.address;
        row.taxNumber = enterprise.taxNumber;

        this.updateDataByApi(enterprise);
    }

    confirmAllUpdates(enterprises: EnterpriseRow[]): void {
        this.updateCollectionOfDataByApi(enterprises);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(enterprise: EnterpriseRow): void {
        this.http.put(`${this.serverUrl}enterprise`, {
            id: enterprise.id,
            name: enterprise.name,
            address: enterprise.address,
            taxNumber: enterprise.taxNumber,
            bankAccount: enterprise.bankAccount,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(enterprises: EnterpriseRow[]): void {
        let body: any[] = [];
        enterprises.forEach(e => {
            body.push({
                id: e.id,
                name: e.name,
                address: e.address,
                taxNumber: e.taxNumber,
                bankAccount: e.bankAccount,
            })
        })

        this.http.put(`${this.serverUrl}enterprise/all-update`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}enterprise/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(enterprise: EnterpriseRow) {
        const body = {
            id: enterprise.id,
            name: enterprise.name,
            address: enterprise.address,
            taxNumber: enterprise.taxNumber,
            bankAccount: enterprise.bankAccount,
        };
        this.http.post(`${this.serverUrl}enterprise`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
            err => {
                alert('[addNewDataToApi] failed');
            });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}enterprise/all`) as Observable<Enterprise[]>
        ]).subscribe(([enterprises]) => {
            let data: EnterpriseRow[] = enterprises.map(e => ({
                id: e.id.toString(),
                internalId: `${e.id.toString().split('-')[0]}***`,
                name: e.name,
                address: e.address,
                taxNumber: e.taxNumber,
                bankAccount: e.bankAccount,
            }));

            this.loadedEnterprisesSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

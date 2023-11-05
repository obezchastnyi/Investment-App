import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { APP_SETTINGS } from '../utils';

@Injectable({
    providedIn: 'root',
})
export class FormCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    public loadedResultSub = new BehaviorSubject<number | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<number> {
        return this.loadedResultSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}formation`) as Observable<number>
        ]).subscribe(([result]) => {
            this.loadedResultSub.next(result);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

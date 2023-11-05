import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { Role, User, UserRow } from 'src/app/models';
import { APP_SETTINGS } from '../utils';

@Injectable({
    providedIn: 'root',
})
export class UsersCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    private loadedUsersSub = new BehaviorSubject<UserRow[] | null>(null);
    private loadedRolesSub = new BehaviorSubject<Role[] | null>(null);

    constructor(private http: HttpClient) {
        this.loadDataFromApi();
    }

    getTableRows(): Observable<UserRow[]> {
        return this.loadedUsersSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    getUserRoles(): Observable<Role[]> {
        return this.loadedRolesSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    loadDataFromApi(): void {
        this.getDataFromApi();
    }

    updateTableRow(user: UserRow) {
        let row = this.loadedUsersSub.value
            .find(u => u.id == user.id);

        row.userName = user.userName;
        row.role = user.role;

        this.updateDataByApi(user);
    }

    private updateDataByApi(user: UserRow): void {
        this.http.put(`${this.serverUrl}user`, {
            id: user.id,
            userName: user.userName,
            roleId: this.loadedRolesSub.value.find(r => r.code === user.role).id,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}user/all`) as Observable<User[]>,
            this.http.get(`${this.serverUrl}userRole/all`) as Observable<Role[]>
        ]).subscribe(([users, roles]) => {
            let data: UserRow[] = users.map(u => ({
                id: u.id.toString(),
                internalId: `${u.id.toString().split('-')[0]}***`,
                userName: u.userName,
                role: u.userRole.code,
            }));

            this.loadedUsersSub.next(data);
            this.loadedRolesSub.next(roles);
        });
    }
}

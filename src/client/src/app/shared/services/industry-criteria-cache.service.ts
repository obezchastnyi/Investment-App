import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, shareReplay } from 'rxjs';
import { APP_SETTINGS } from '../utils';
import { IndustryCriteria, IndustryCriteriaRow } from 'src/app/models/industry-criteria';
import { CriteriaRow } from 'src/app/models/criteria';
import { IndustryRow } from 'src/app/models/industry';
import { CriteriaCacheService } from './criteria-cache.service';
import { IndustryCacheService } from './industry-cache.service';

@Injectable({
    providedIn: 'root',
})
export class IndustryCriteriaCacheService {

    serverUrl = `${APP_SETTINGS.SERVER_BASE_URL}${APP_SETTINGS.SERVER_CURRENT_VERSION}`;

    public loadedIndustryCriteriasSub = new BehaviorSubject<IndustryCriteriaRow[] | null>(null);
    private loadedCriteriasSub = new BehaviorSubject<CriteriaRow[] | null>(null);
    private loadedIndustriesSub = new BehaviorSubject<IndustryRow[] | null>(null);

    constructor(private http: HttpClient, criteriaCacheService: CriteriaCacheService, industryCacheService: IndustryCacheService) {
        this.loadedCriteriasSub = criteriaCacheService.loadedCriteriasSub;
        this.loadedIndustriesSub = industryCacheService.loadedIndustriesSub;
        this.loadDataFromApi();
    }

    getTableRows(): Observable<IndustryCriteriaRow[]> {
        return this.loadedIndustryCriteriasSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    getCriterias(): Observable<CriteriaRow[]> {
        return this.loadedCriteriasSub.asObservable().pipe(
            shareReplay(1),
        );
    }

    getIndustries(): Observable<IndustryRow[]> {
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
            industrySpecificWeight: 0,
            industry: this.loadedIndustriesSub.getValue()[0].name,
            industryId: this.loadedIndustriesSub.getValue()[0].id,
            criteria: this.loadedCriteriasSub.getValue()[0].name,
            criteriaId: this.loadedCriteriasSub.getValue()[0].id,
            internalId: ''
        });
    }

    updateTableRow(criteria: IndustryCriteriaRow) {
        let row = this.loadedIndustryCriteriasSub.value
            .find(p => p.id == criteria.id);

        row.industrySpecificWeight = criteria.industrySpecificWeight;
        row.industry = criteria.industry;
        row.criteria = criteria.criteria;

        this.updateDataByApi(criteria);
    }

    confirmAllUpdates(possibilities: IndustryCriteriaRow[]): void {
        this.updateCollectionOfDataByApi(possibilities);
    }

    confirmRowDeleting(id: string) {
        this.deleteRow(id);
    }

    private updateDataByApi(criteria: IndustryCriteriaRow): void {
        this.http.put(`${this.serverUrl}criteria/industry`, {
            id: criteria.id,
            industrySpecificWeight: criteria.industrySpecificWeight,
            industryId: this.loadedIndustriesSub.value.find(e => e.name === criteria.industry).id,
            criteriaId: this.loadedCriteriasSub.value.find(e => e.name === criteria.criteria).id,
        }).subscribe(() => { },
            err => {
                alert('[updateDataByApi] failed');
            });
    }

    private updateCollectionOfDataByApi(criterias: IndustryCriteriaRow[]): void {
        let body: any[] = [];
        criterias.forEach(p => {
            body.push({
                id: p.id,
                industrySpecificWeight: p.industrySpecificWeight,
                industryId: this.loadedIndustriesSub.value.find(e => e.name === p.industry).id,
                criteriaId: this.loadedCriteriasSub.value.find(e => e.name === p.criteria).id,
            })
        })

        this.http.put(`${this.serverUrl}criteria/all-update/industry`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('Failed update');
                });
    }

    private deleteRow(id: string): void {
        this.http.delete(`${this.serverUrl}criteria/industry/${id}`)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[deleteRow] failed');
                });
    }

    private addNewDataToApi(criteria: IndustryCriteriaRow) {
        const body = {
            id: criteria.id,
            industrySpecificWeight: criteria.industrySpecificWeight,
        };
        this.http.post(`${this.serverUrl}criteria/industry`, body)
            .subscribe(() => {
                this.loadDataFromApi();
            },
                err => {
                    alert('[addNewDataToApi] failed');
                });
    }

    private getDataFromApi(): void {
        combineLatest([
            this.http.get(`${this.serverUrl}criteria/all/industry`) as Observable<IndustryCriteria[]>
        ]).subscribe(([possibilities]) => {
            let data: IndustryCriteriaRow[] = possibilities.map(p => ({
                id: p.id.toString(),
                internalId: `${p.id.toString().split('-')[0]}***`,
                industrySpecificWeight: p.industrySpecificWeight,
                industry: p.industry.name,
                industryId: p.industryId.toString(),
                criteria: p.criteria.name,
                criteriaId: p.criteriaId.toString(),
            }));

            this.loadedIndustryCriteriasSub.next(data);
        },
            err => {
                alert('[getDataFromApi] failed');
            });
    }
}

import { SortDirection } from '@swimlane/ngx-datatable';

// targets that sorting can be applied to
export type SortingTarget = 'users' | 'companies' | 'companyUsers';

export type GenericSortType<T extends string> = {
    prop: T;
    dir: SortDirection;
};

export type DataTableSortType = GenericSortType<string>;

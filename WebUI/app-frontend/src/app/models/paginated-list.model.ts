export class PaginatedList<T> {
    public items: T[];
    public pageIndex: number;
    public totalPages: number
    public totalCount: number;
    public hasPreviousPage: boolean;
    public hasNextPage: boolean;
}

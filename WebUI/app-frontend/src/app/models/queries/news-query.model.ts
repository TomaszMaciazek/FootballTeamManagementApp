export class NewsQuery {
    pageNumber: number;
    pageSize: number;
    orderByColumnName: string;
    orderByDirection: string;

    constructor(data: {
        pageNumber: number,
        pageSize: number,
        orderByColumnName: string,
        orderByDirection: string
    }){
        Object.assign(this, data);
    }
}

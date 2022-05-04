export class UserQuery {
    public pageNumber: number;
    public pageSize: number;
    public orderByColumnName: string;
    public orderByDirection: string;
    public queryString: string;
    public isActive : boolean;

    constructor(data: {
        pageNumber: number,
        pageSize: number,
        orderByColumnName: string,
        orderByDirection: string,
        queryString: string,
        isActive : boolean
    }){
        Object.assign(this, data);
    }
}

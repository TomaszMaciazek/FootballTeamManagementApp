export class TeamQuery {
    pageNumber: number;
    pageSize: number;
    orderByColumnName: string;
    orderByDirection: string;
    name : string;
    coachId : string;

    constructor(data: {
        pageNumber: number,
        pageSize: number,
        orderByColumnName: string,
        orderByDirection: string,
        name : string,
        coachId : string
    }){
        Object.assign(this, data);
    }
}

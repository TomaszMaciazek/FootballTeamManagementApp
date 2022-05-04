export class TestResultsQuery {
    public pageNumber: number;
    public pageSize: number;
    public orderByColumnName: string;
    public orderByDirection: string;
    public testId : string;

    constructor(data: {
        pageNumber: number,
        pageSize: number,
        orderByColumnName: string,
        orderByDirection: string,
        testId : string
    }){
        Object.assign(this,data);
    }
}

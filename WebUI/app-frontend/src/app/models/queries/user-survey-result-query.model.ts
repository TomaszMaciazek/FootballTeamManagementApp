export class UserSurveyResultQuery {
    public pageNumber: number;
    public pageSize: number;
    public orderByColumnName: string;
    public orderByDirection: string;
    public userId : string;
    public isCompleated : boolean;
    public title: string;

    constructor(data: {
        pageNumber: number,
        pageSize: number,
        orderByColumnName: string,
        orderByDirection: string,
        title: string,
        userId : string,
        isCompleated : boolean
    }){
        Object.assign(this,data);
    }
}

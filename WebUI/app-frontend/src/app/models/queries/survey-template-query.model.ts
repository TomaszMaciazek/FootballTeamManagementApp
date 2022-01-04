export class SurveyTemplateQuery {
    public pageNumber: number;
    public pageSize: number;
    public orderByColumnName: string;
    public orderByDirection: string;
    public authorId : string;
    public title : string;
    public isAnonymous : boolean;

    constructor(data: {
        pageNumber: number,
        pageSize: number,
        orderByColumnName: string,
        orderByDirection: string,
        authorId : string,
        title : string,
        isAnonymous : boolean
    }){
        Object.assign(this, data);
    }
}

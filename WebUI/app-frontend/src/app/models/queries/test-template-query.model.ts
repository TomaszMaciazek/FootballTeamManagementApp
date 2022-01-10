export class TestTemplateQuery {
    public pageNumber: number;
    public pageSize: number;
    public orderByColumnName: string;
    public orderByDirection: string;
    public authorId : string;
    public title : string;

    constructor(data: {
        pageNumber: number,
        pageSize: number,
        orderByColumnName: string,
        orderByDirection: string,
        authorId : string,
        title : string
    }){
        Object.assign(this, data);
    }
}

export class TrainingQuery {
    pageNumber: number;
    pageSize: number;
    orderByColumnName: string;
    orderByDirection: string;
    isActive: boolean;
    title: string;
    minDate : Date;
    maxDate: Date;

    constructor(data: {
        pageNumber: number,
        pageSize: number,
        orderByColumnName: string,
        orderByDirection: string,
        isActive: boolean,
        title: string,
        minDate : Date,
        maxDate: Date,
    }){
        Object.assign(this, data);
    }
}

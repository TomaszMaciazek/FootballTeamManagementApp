import { Gender } from "src/app/enums/gender";

export class CoachQuery {
    public pageNumber : number;
    public pageSize : number;
    public orderByColumnName : string;
    public orderByDirection : string;
    public queryString : string;
    public isActive : boolean;
    public countriesIds: Array<number>;
    public gender: Gender;
    public teamsIds : Array<string>;
    public isStillWorking : boolean;

    constructor(data: {
        pageNumber : number,
        pageSize : number,
        orderByColumnName : string,
        orderByDirection : string,
        queryString : string,
        isActive : boolean,
        countriesIds: Array<number>,
        gender: Gender,
        teamsIds : Array<string>,
        isStillWorking : boolean
    }){
        Object.assign(this, data)
    }
}

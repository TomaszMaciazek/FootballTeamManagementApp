import { Gender } from "src/app/enums/gender";

export class PlayerQuery {
    public pageNumber : number;
    public pageSize : number;
    public orderByColumnName : string;
    public orderByDirection : string;
    public queryString : string;
    public isActive : boolean;
    public countryId: number;
    public gender: Gender;
    public teamId : string;
    public isStillPlaying : boolean;

    constructor(data: {
        pageNumber : number,
        pageSize : number,
        orderByColumnName : string,
        orderByDirection : string,
        queryString : string,
        isActive : boolean,
        countryId: number,
        gender: Gender,
        teamId : string,
        isStillPlaying : boolean
    }){
        Object.assign(this, data)
    }
}

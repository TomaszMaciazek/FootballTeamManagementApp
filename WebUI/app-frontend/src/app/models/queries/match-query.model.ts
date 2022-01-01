import { MatchType } from "src/app/enums/match-type";
import { PlayersGender } from "src/app/enums/players-gender";

export class MatchQuery {
    public pageNumber : number;
    public pageSize : number;
    public orderByColumnName : string;
    public orderByDirection : string;
    public queryString : string;
    public isActive: boolean;
    public playersGenders : Array<PlayersGender>;
    public matchTypes : Array<MatchType>;
    public teamsIds : Array<string>;
    public minDate : string;
    public maxDate : string;

    constructor(data: {
        pageNumber : number,
        pageSize : number,
        orderByColumnName : string,
        orderByDirection : string,
        queryString : string,
        isActive : boolean,
        playersGenders : Array<PlayersGender>,
        matchTypes : Array<MatchType>,
        teamsIds : Array<string>,
        minDate : string,
        maxDate : string
    }){
        Object.assign(this, data)
    }
}

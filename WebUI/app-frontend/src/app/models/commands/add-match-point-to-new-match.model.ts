import { MatchPointType } from "src/app/enums/match-point-type";

export class AddMatchPointToNewMatch {
    public playerId : string;
    public point : MatchPointType;
    public minuteOfMatch : number;

    constructor(data: {
        playerId : string,
        point : MatchPointType,
        minuteOfMatch : number
    }){
        Object.assign(this, data);
    }
}

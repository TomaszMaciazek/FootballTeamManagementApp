import { MatchPointType } from "src/app/enums/match-point-type";

export class MatchPoint {
    public id : string;
    public isActive : boolean;
    public matchId : string;
    public playerId : string;
    public point : MatchPointType;
    public minuteOfMatch : number;
}

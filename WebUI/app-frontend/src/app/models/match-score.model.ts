import { MatchScoreType } from "../enums/match-score-type";

export class MatchScore {
    public id: string;
    public playerId: string;
    public value: number;
    public scoreType: MatchScoreType;  
    public isActive : boolean;
}

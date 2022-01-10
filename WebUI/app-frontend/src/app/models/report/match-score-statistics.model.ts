import { MatchScoreType } from "src/app/enums/match-score-type";

export class MatchScoreStatistics {
    public scoreType : MatchScoreType;
    public avg: number;
    public min : number;
    public max : number;
    public median : number;
}

import { MatchScoreStatistics } from "./match-score-statistics.model";

export class MonthlyMatchScoreStatistics {
    public year : number;
    public month : number;
    public scores : Array<MatchScoreStatistics>;
}

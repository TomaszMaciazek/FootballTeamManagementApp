import { MonthlyMatchScoreStatistics } from "./monthly-match-score-statistics.model";

export class PlayerMonthlyMatchScoreStatistics {
    public playerId : string;
    public scoreStatistics : Array<MonthlyMatchScoreStatistics>;
}

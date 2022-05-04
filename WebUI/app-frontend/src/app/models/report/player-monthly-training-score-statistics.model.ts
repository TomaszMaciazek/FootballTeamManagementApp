import { MonthlyTrainingScoreStatistics } from "./monthly-training-score-statistics.model";

export class PlayerMonthlyTrainingScoreStatistics {
    public playerId : string;
    public scoreStatistics : Array<MonthlyTrainingScoreStatistics>;
}

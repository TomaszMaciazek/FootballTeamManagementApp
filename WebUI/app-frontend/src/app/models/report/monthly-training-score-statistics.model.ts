import { TrainingScoreStatistics } from "./training-score-statistics.model";

export class MonthlyTrainingScoreStatistics {
    public year : number;
    public month : number;
    public scores : Array<TrainingScoreStatistics>;
}

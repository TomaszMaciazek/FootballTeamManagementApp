import { TrainingScoreStatistics } from "./training-score-statistics.model";

export class PlayerTrainingScoreStatistics {
    public playerId : string;
    public scores : Array<TrainingScoreStatistics>;
}

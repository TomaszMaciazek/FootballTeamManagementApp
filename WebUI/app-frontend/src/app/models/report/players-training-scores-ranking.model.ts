import { TrainingScoreType } from "src/app/enums/training-score-type";
import { PlayerRankingScoreValue } from "./player-ranking-score-value.model";

export class PlayersTrainingScoresRanking {
    public scoreType : TrainingScoreType;
    public playerScores : Array<PlayerRankingScoreValue>;
}

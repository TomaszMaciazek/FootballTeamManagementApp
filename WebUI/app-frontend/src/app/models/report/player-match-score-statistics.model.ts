import { MatchScoreStatistics } from "./match-score-statistics.model";

export class PlayerMatchScoreStatistics {
    public playerId : string;
    public scores : Array<MatchScoreStatistics>;
}

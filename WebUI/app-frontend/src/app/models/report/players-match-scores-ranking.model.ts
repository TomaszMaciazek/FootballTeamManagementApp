import { MatchScoreType } from "src/app/enums/match-score-type";
import { PlayerRankingScoreValue } from "./player-ranking-score-value.model";

export class PlayersMatchScoresRanking {
    public scoreType : MatchScoreType;
    public playerScores : Array<PlayerRankingScoreValue>;
}

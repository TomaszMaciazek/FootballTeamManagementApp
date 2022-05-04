import { MatchScoreType } from "src/app/enums/match-score-type";

export class MatchScoreRankingQuery {
    public scoreType : MatchScoreType;
    public startDate : string;
    public endDate : string
    public onlyStillPlaying : boolean;
    public count : number;
    public teamsIds : Array<string>;
    public getPlayersWithoutTeam: boolean;
}

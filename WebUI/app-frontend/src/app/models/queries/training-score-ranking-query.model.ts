import { TrainingScoreType } from "src/app/enums/training-score-type";

export class TrainingScoreRankingQuery {
    public scoreType : TrainingScoreType;
    public startDate : string;
    public endDate : string
    public onlyStillPlaying : boolean;
    public count : number;
    public teamsIds : Array<string>;
    public getPlayersWithoutTeam: boolean;
}

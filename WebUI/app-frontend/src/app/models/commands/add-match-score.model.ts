import { MatchScoreType } from "src/app/enums/match-score-type";

export class AddMatchScoreCommand {
    public playerId: string;
    public matchId: string;
    public scoreType: MatchScoreType;
    public value: number;

    constructor(data: {
        playerId: string,
        matchId: string,
        scoreType: MatchScoreType,
        value: number
    }){
        Object.assign(this, data);
    }
}

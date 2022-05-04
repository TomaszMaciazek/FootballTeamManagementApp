import { MatchPointType } from "src/app/enums/match-point-type";
import { SimpleSelectPlayer } from "./simple-select-player.model";

export class PlayerPointAssignment {
    public player: SimpleSelectPlayer;
    public pointType : MatchPointType;
    public minuteOfMatch: number;

    constructor(data: {
        player: SimpleSelectPlayer,
        pointType : MatchPointType,
        minuteOfMatch: number
    }){
        Object.assign(this.minuteOfMatch, data);
    }
}

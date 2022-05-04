import { PlayerPosition } from "src/app/enums/player-position";
import { SimpleTeam } from "../team/simple-team.model";
import { SimpleSelectPlayer } from "./simple-select-player.model";

export class PlayerPositionAssignment {
    public player: SimpleSelectPlayer;
    public position : PlayerPosition;

    constructor(data: {
        player: SimpleSelectPlayer,
        position : PlayerPosition
    }){
        Object.assign(this,data);
    }
}

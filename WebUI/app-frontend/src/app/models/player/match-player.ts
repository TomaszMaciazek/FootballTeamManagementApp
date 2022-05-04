import { PlayerPosition } from "src/app/enums/player-position";
import { SimpleTeam } from "../team/simple-team.model";

export class MatchPlayer {
    public id : string;
    public playerId : string;
    public isActive : boolean;
    public team : SimpleTeam;
    public name : string;
    public middleName : string;
    public surname : string;
    public playerPosition : PlayerPosition;
}

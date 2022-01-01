import { Gender } from "src/app/enums/gender";
import { PlayerPosition } from "src/app/enums/player-position";
import { SimpleTeam } from "../team/simple-team.model";

export class AccountPlayer {
    public id : string;
    public startedPlaying : Date;
    public finishedPlaying : Date;
    public gender : Gender;
    public prefferedPosition : PlayerPosition;
    public birthDate : Date;
    public country : string;
    public team : SimpleTeam;
    public matchCount : number;
}

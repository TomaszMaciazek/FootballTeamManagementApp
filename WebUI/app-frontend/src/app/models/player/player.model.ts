import { Gender } from "src/app/enums/gender";
import { PlayerPosition } from "src/app/enums/player-position";
import { SimpleTeam } from "../team/simple-team.model";

export class Player {
    public id : string;
    public startedPlaying : Date;
    public finishedPlaying : Date;
    public email : string;
    public gender : Gender;
    public prefferedPosition : PlayerPosition;
    public birthDate : Date;
    public country : string;
    public team : SimpleTeam;
    public name : string;
    public middleName : string;
    public surname : string;
    public isActive : boolean;
    public matchCount : number;
}

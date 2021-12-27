import { Gender } from "src/app/enums/gender";
import { PlayerPosition } from "src/app/enums/player-position";
import { SimpleTeam } from "../team/simple-team.model";

export class PlayerItem {
    public id : string;
    public team : SimpleTeam;
    public name: string;
    public middleName : string;
    public surname : string;
    public gender : Gender;
    public prefferedPosition : PlayerPosition;
    public isStillPlaying : boolean;
    public country : string;
    public isActive : boolean;
    public yearOfBirth: number;
}

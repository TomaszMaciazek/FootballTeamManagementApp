import { SimplePlayerName } from "../player/simple-player-name.model";

export class PlayerLeftTeamEvent {
    public id : string;
    public player : SimplePlayerName;
    public date : Date;
}

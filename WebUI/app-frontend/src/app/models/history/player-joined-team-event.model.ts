import { SimplePlayerName } from "../player/simple-player-name.model";

export class TeamHistoryPlayerJoinedTeamEvent {
    public id : string;
    public player : SimplePlayerName;
    public date : Date;
}

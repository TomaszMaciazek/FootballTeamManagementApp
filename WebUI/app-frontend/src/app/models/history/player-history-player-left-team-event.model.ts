import { SimpleTeam } from "../team/simple-team.model";

export class PlayerHistoryPlayerLeftTeamEvent {
    public id : string;
    public team : SimpleTeam;
    public date : Date;
}

import { SimpleTeam } from "../team/simple-team.model";

export class PlayerHistoryPlayerJoinedTeamEvent {
    public id : string;
    public team : SimpleTeam;
    public date : Date;
}

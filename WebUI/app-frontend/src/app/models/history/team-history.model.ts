import { CoachAssignmentEvent } from "./coach-assignment-event.model";
import { TeamHistoryPlayerJoinedTeamEvent } from "./player-joined-team-event.model";
import { PlayerLeftTeamEvent } from "./player-left-team-event.model";
import { TeamMatchEvent } from "./team-match-event.model";

export class TeamHistory {
    public id: string;
    public created: Date;
    public coachAssignmentEvents : Array<CoachAssignmentEvent>;
    public matchEvents: Array<TeamMatchEvent>;
    public playerJoinedTeamEvents: Array<TeamHistoryPlayerJoinedTeamEvent>;
    public playerLeftTeamEvents: Array<PlayerLeftTeamEvent>;
}

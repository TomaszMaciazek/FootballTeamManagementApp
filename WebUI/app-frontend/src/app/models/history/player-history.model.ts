import { PlayerHistoryPlayerJoinedTeamEvent } from "./player-history-player-joined-team-event.model";
import { PlayerHistoryPlayerLeftTeamEvent } from "./player-history-player-left-team-event.model";
import { PlayerMatchEvent } from "./player-match-event.model";

export class PlayerHistory {
    public id : string;
    public created : Date;
    public playerJoinedTeamEvents : Array<PlayerHistoryPlayerJoinedTeamEvent>
    public playerLeftTeamEvents : Array<PlayerHistoryPlayerLeftTeamEvent>
    public matchEvents : Array<PlayerMatchEvent>
}

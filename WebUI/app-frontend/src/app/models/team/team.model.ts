import { SimpleCoach } from "../coach/simple-coach.model";
import { TeamHistoryEvent } from "../history/team-history-event.model";
import { SimplePlayer } from "../player/simple-player.model";

export class Team {
    public id : string;
    public isActive : string;
    public name : string;
    public players : Array<SimplePlayer>;
    public mainCoach : SimpleCoach;
}

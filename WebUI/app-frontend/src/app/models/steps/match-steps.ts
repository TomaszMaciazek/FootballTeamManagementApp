import { MatchType } from "src/app/enums/match-type";
import { PlayersGender } from "src/app/enums/players-gender";
import { CoachCardAssignment } from "../coach/coach-card-assignment.model";
import { PlayerPositionAssignment } from "../player/player-assignment.model";
import { PlayerCardAssignment } from "../player/player-card-assignment.model";
import { PlayerPointAssignment } from "../player/player-point-assignment.model";
import { SimpleSelectPlayer } from "../player/simple-select-player.model";

export class MatchSteps {
    public date : Date;
    public opponentsClubName : string;
    public opponentsScore : number;
    public clubScore : number;
    public location : string;
    public playersGender : PlayersGender;
    public matchType : MatchType;
    public description: string;
    public players: SimpleSelectPlayer[];
    public assignedPlayers: PlayerPositionAssignment[];
    public playersPointsAssignments: PlayerPointAssignment[];
    public playersCardsAssignments: PlayerCardAssignment[];
    public coachCardsAssignments: CoachCardAssignment[];

    constructor(){
        this.assignedPlayers = [];
        this.playersPointsAssignments = [];
        this.playersCardsAssignments = [];
        this.coachCardsAssignments = [];
        this.clubScore = 0;
        this.opponentsScore = 0;
        this.date = null;
        this.opponentsClubName = null,
        this.matchType = null;
        this.location = null;
        this.description = null;
        this.playersGender = null;
        this.players = [];
    }
}

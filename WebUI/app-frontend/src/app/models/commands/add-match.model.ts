import { MatchType } from "src/app/enums/match-type";
import { PlayersGender } from "src/app/enums/players-gender";
import { AddCoachCardToNewMatch } from "./add-coach-card-to-new-match.model";
import { AddMatchPointToNewMatch } from "./add-match-point-to-new-match.model";
import { AddPlayerCardToNewMatch } from "./add-player-card-to-new-match.model";
import { AddPlayerPerformanceToNewMatch } from "./add-player-performance-to-new-match.model";

export class AddMatchCommand {
    public date : Date;
    public opponentsClubName : string;
    public opponentsScore : number;
    public clubScore : number;
    public location : string;
    public playersGender : PlayersGender;
    public matchType : MatchType;
    public description: string;
    public playerPerformances : Array<AddPlayerPerformanceToNewMatch>;
    public matchPoints : Array<AddMatchPointToNewMatch>;
    public playersCards : Array<AddPlayerCardToNewMatch>;
    public coachesCards : Array<AddCoachCardToNewMatch>;


    constructor(data: {
        date : Date,
        opponentsClubName : string,
        opponentsScore : number,
        clubScore : number,
        location : string,
        playersGender : PlayersGender,
        matchType : MatchType,
        description: string,
        playerPerformances : Array<AddPlayerPerformanceToNewMatch>,
        matchPoints : Array<AddMatchPointToNewMatch>,
        playersCards : Array<AddPlayerCardToNewMatch>,
        coachesCards : Array<AddCoachCardToNewMatch>
    }){
        Object.assign(this, data);
    }
}
